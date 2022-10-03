using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Layout;
using Avalonia.Media;
using BotwActorTool.GUI.Controls;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.Lib;
using System.Collections.Generic;
using System.Linq;
using Material.Icons.Avalonia;
using ReactiveUI;

namespace BotwActorTool.GUI.Views.Editors
{
    public partial class ActorLink : ActorEditor
    {
        private readonly StackPanel root;
        private ActorViewModel? doc;
        public ActorLink()
        {
            AvaloniaXamlLoader.Load(this);
            root = this.FindControl<StackPanel>("ROOT")!;
        }

        public override void Init(ActorViewModel doc)
        {
            if (root.Children.Count > 0) {
                return;
            }

            this.doc = doc;
            var linkInfo = Resource.GetDynamic<Dictionary<string, Dictionary<string, dynamic>>>("LinkInfo")!;

            root.Children.Add(CreateEntry("Name", "Name of the actor.", doc.Actor.Name));

            foreach ((var link, var data) in linkInfo) {
                string tip = data["Tip"].ToString();

                bool? isNotDummy = null;
                try {
                    isNotDummy = doc.Actor.GetLink(link) != "Dummy";
                }
                catch { }

                if (isNotDummy != null) {
                    root.Children.Add(CreateEntry(data["Name"].ToString(), string.IsNullOrEmpty(tip) ? link : tip, doc!.Actor.GetLink(link)));
                }
            }
        }

        public Grid CreateEntry(string name, string tip, object defaultValue)
        {
            Grid grid = new() {
                ColumnDefinitions = new("37,37,42,*"),
                Margin = new(0, 2.5)
            };

            TextBox textBox = new() {
                Text = defaultValue.ToString(),
                Watermark = string.IsNullOrEmpty(name) ? "???" : name,
                UseFloatingWatermark = true,
            };
            textBox.Classes.Add("inline");

            // Apply/Update
            Button btnApply = new() {
                HorizontalAlignment = HorizontalAlignment.Left,
                Content = new MaterialIcon() {
                    Kind = Material.Icons.MaterialIconKind.AlertCircleCheckOutline
                },
                Command = ReactiveCommand.Create(() => {
                    textBox.Text = doc!.Actor.Name;
                })
            };

            // Set as Dummy (Clear)
            Button btnReset = new() {
                HorizontalAlignment = HorizontalAlignment.Left,
                Content = new MaterialIcon() {
                    Kind = Material.Icons.MaterialIconKind.RefreshCircle
                },
                Command = ReactiveCommand.Create(() => {
                    textBox.Text = "Dummy";
                })
            };

            // Set as Default
            Button btnSetDefault = new() {
                HorizontalAlignment = HorizontalAlignment.Left,
                Content = new MaterialIcon() {
                    Kind = Material.Icons.MaterialIconKind.SetNull
                },
                Command = ReactiveCommand.Create(() => {
                    textBox.Text = doc!.Actor.Name;
                })
            };

            Grid.SetColumn(btnApply, 0);
            Grid.SetColumn(btnReset, 1);
            Grid.SetColumn(btnSetDefault, 2);
            Grid.SetColumn(textBox, 3);

            ToolTip.SetTip(btnApply, "Save/apply the modified value.");
            ToolTip.SetTip(btnReset, "Set the value to 'Dummy'.");
            ToolTip.SetTip(btnSetDefault, "Use the default value (actor name).");
            ToolTip.SetTip(textBox, tip);

            grid.Children.Add(btnApply);
            grid.Children.Add(btnReset);
            grid.Children.Add(btnSetDefault);
            grid.Children.Add(textBox);

            return grid;
        }
    }
}
