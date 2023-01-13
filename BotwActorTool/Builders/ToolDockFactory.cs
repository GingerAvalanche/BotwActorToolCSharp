using BotwActorTool.Attributes;
using BotwActorTool.Extensions;
using Dock.Model.Core;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BotwActorTool.Builders
{
    public class ToolDockFactory
    {
        public static List<string> ToolDocks { get; set; } = new();

        public static string AddDock(string id)
        {
            ToolDocks.Add(id);
            return id;
        }

        public static bool? SetDock(string id, IDockable dock)
        {
            return DockFactory.Root?.Get<IDock>("Tools")?.Set(id, dock);
        }

        public static void SetActive(string id)
        {
            var rootDock = DockFactory.Root?.Get<IDock>("Tools");
            if (rootDock != null) {
                rootDock.ActiveDockable = QueryTool(out IDockable? _, id);
            }
        }

        public static List<IDockable> Build<T>()
        {
            List<IDockable> docks = new();
            object? obj = typeof(T).GetConstructor(Array.Empty<Type>())?.Invoke(Array.Empty<object>());

            if (obj == null) {
                throw new Exception($"Object of type '{nameof(T)}' must have a constructor that takes zero parameters to be used as a ToolDockFactory target.");
            }
            else {
                foreach (var func in typeof(T).GetMethods().Where(x => x.GetCustomAttributes<ToolDockAttribute>().Any())) {
                    IDockable dock = (IDockable)func.Invoke(obj, Array.Empty<object>())!;
                    dock.Title = func.GetCustomAttribute<ToolDockAttribute>()?.Title ?? func.Name;
                    dock.Id = AddDock(func.Name);
                    dock.CanClose = false;
                    docks.Add(dock);
                }
            }

            return docks;
        }

        public static T? QueryTool<T>(out T? obj, [CallerMemberName] string id = "") where T : IDockable
        {
            var exDock = DockFactory.Root?.Get<IDock>("Tools")?.Get(id);
            obj = exDock != null ? (T)exDock : default;
            return obj;
        }
    }
}
