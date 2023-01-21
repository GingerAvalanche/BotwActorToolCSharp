using BotwActorTool.Attributes;
using BotwActorTool.Builders;
using BotwActorTool.Extensions;
using BotwActorTool.Lib;
using BotwActorTool.ViewModels.Tools;

namespace BotwActorTool.Models
{
    internal class ToolDockModel
    {
        [ToolDock(Title = "Actor")]
        public static ActorFilesViewModel Actor()
        {
            // Query build tool dock for this VM and return, else build the VM
            return ToolDockFactory.QueryTool(out ActorFilesViewModel? obj) != null ? obj! : new();
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

        [ToolDock(Title = "Mods")]
        public static ModBrowserViewModel Mods()
        {
            // Query build tool dock for this VM and return, else build the VM
            return ToolDockFactory.QueryTool(out ModBrowserViewModel? obj) != null ? obj! : new();
        }
    }
}
