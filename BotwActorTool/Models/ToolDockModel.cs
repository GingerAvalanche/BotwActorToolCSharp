using BotwActorTool.Attributes;
using BotwActorTool.Builders;
using BotwActorTool.Extensions;
using BotwActorTool.ViewModels.Tools;
using BotwActorTool.Lib;

namespace BotwActorTool.Models
{
    internal class ToolDockModel
    {
        [ToolDock(Title = "Actor")]
        public static FileBrowserViewModel Actor()
        {
            // Query build tool dock for this VM and return, else build the VM
            return ToolDockFactory.QueryTool(out FileBrowserViewModel? obj) != null ? obj! : new();
        }

        [ToolDock(Title = "Vanilla Actors")]
        public static FileBrowserViewModel VanillaActors()
        {
            // Query build tool dock for this VM and return, else build the VM
            if (ToolDockFactory.QueryTool(out FileBrowserViewModel? obj) != null) return obj!;

            FileBrowserViewModel browser = new() {
                ItemsBase = ActorInfoExtension.LoadActorInfoNodes(Config.GetDir(BotwDir.Update))
            };

            return browser;
        }

        [ToolDock(Title = "Mod Actors")]
        public static FileBrowserViewModel ModActors()
        {
            // Query build tool dock for this VM and return, else build the VM
            return ToolDockFactory.QueryTool(out FileBrowserViewModel? obj) != null ? obj! : new();
        }
    }
}
