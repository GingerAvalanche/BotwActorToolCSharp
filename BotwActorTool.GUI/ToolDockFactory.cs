#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Avalonia.Data;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.Lib;
using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI;
using Dock.Model.ReactiveUI.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI
{
    public class ToolDockFactory : Factory
    {
        private readonly AppViewModel context;

        public ToolDockFactory(AppViewModel _context) => context = _context;

        public override IRootDock CreateLayout()
        {
            var dockLayout = new ToolDock {
                Id = "FileBrowsers",
                Title = "FileBrowsers",
                Proportion = double.NaN,
                ActiveDockable = null,
                VisibleDockables = CreateList<IDockable>(
                    new BrowserViewModel((string field) => context.OpenActorFile(field)) {
                        Title = "Actor",
                        CanClose = false,
                        CanPin = false,
                        CanFloat = false,
                        Id = "Actor"
                    },
                    new BrowserViewModel((string field) => context.OpenGameActor(field), Config.GetDir(BotwDir.Update)) {
                        Title = "Vanilla Files",
                        CanClose = false,
                        CanPin = false,
                        CanFloat = false,
                        Id = "VanillaFiles"
                    },
                    new BrowserViewModel((string field) => context.OpenModActor(field)) {
                        Title = "Mod Files",
                        CanClose = false,
                        CanPin = false,
                        CanFloat = false,
                        Id = "ModFiles"
                    }
                )
            };

            IRootDock root = CreateRootDock();

            root.Id = "RootDock";
            root.Title = "RootDock";
            root.ActiveDockable = dockLayout;
            root.DefaultDockable = dockLayout;
            root.VisibleDockables = CreateList<IDockable>(dockLayout);

            context.ToolFactory = this;
            context.ToolLayout = root;

            return root;
        }

        public override void InitLayout(IDockable layout)
        {
            HostWindowLocator = new Dictionary<string, Func<IHostWindow>> {
                [nameof(IDockWindow)] = () => {
                    var hostWindow = new HostWindow() {
                        [!HostWindow.TitleProperty] = new Binding("ActiveDockable.Title")
                    };

                    return hostWindow;
                }
            };

            base.InitLayout(layout);
        }
    }
}
