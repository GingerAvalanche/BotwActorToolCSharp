using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using BotwActorTool.Attributes;
using Dock.Model.ReactiveUI.Controls;
using System.Reflection;

namespace BotwActorTool.Builders
{
    public class ToolbarFactory
    {
        private readonly Document _source;
        private readonly StackPanel _target;

        public ToolbarFactory(Document source, StackPanel target)
        {
            _source = source;
            _target = target;
        }

        public void Initialize()
        {
            IEnumerable<MethodInfo> methods = _source.GetType().GetMethods().Where(x => x.GetCustomAttributes<ToolbarItemAttribute>().Any());
            ToolbarItemAttribute[] items = methods.Select(x => x.GetCustomAttribute<ToolbarItemAttribute>()!).ToArray();
            if (items.Length == 0) {
                return;
            }

            int index = 0;
            List<List<(MethodInfo Method, ToolbarItemAttribute Info)>> groups = new() { new() };

            for (int i = 0; i < items.Length; i++) {
                ToolbarItemAttribute item = items[i];
                groups[index].Add((methods.ElementAt(i), item));

                if (i + 1 < items.Length && item.GroupId != items[i + 1].GroupId) {
                    groups.Add(new());
                    index++;
                }
            }

            foreach (var group in groups) {
                _target.Children.Add(CreateGroupElement(group));
            }
        }

        private StackPanel CreateGroupElement(List<(MethodInfo Method, ToolbarItemAttribute Info)> group)
        {
            StackPanel element = new() {
                Orientation = Orientation.Horizontal
            };

            for (int i = 0; i < group.Count; i++) {
                element.Children.Add(
                    group[i].Method.GetParameters().Length > 0 ? CreateToggleElement(group, i) : CreateButtonElement(group, i)
                );
            }

            return element;
        }

        private Button CreateButtonElement(List<(MethodInfo Method, ToolbarItemAttribute Info)> group, int i)
        {
            Button element = new() {
                CornerRadius = i == 0 ? new(3, 0, 0, 3) : i == group.Count - 1 ? new(0, 3, 3, 0) : new(0),
                Content = new Projektanker.Icons.Avalonia.Icon() {
                    Value = group[i].Info.Icon
                },
                Command = ReactiveCommand.Create(() => group[i].Method.Invoke(_source, Array.Empty<object?>()) as Task)
            };

            ToolTip.SetTip(element, group[i].Info.Description);
            return element;
        }

        private ToggleButton CreateToggleElement(List<(MethodInfo Method, ToolbarItemAttribute Info)> group, int i)
        {
            ToggleButton element = new() {
                CornerRadius = i == 0 ? new(3, 0, 0, 3) : i == group.Count ? new(0, 3, 3, 0) : new(0),
                Content = new Projektanker.Icons.Avalonia.Icon() {
                    Value = group[i].Info.Icon
                },
            };

            ToolTip.SetTip(element, group[i].Info.Description);
            element.Command = ReactiveCommand.Create(() => group[i].Method.Invoke(_source, new object?[] { element.IsChecked }) as Task);
            return element;
        }
    }
}
