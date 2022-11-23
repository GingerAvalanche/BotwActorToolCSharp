using Avalonia.Controls;
using Avalonia.Data;
using BotwActorTool.Extensions;
using BotwActorTool.Models;
using BotwActorTool.ViewModels;
using BotwActorTool.Lib;
using Dock.Avalonia.Controls;
using Dock.Model;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI;
using Dock.Model.ReactiveUI.Controls;
using Dock.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.Builders
{
    public class DockFactory : Factory
    {
        private readonly AppViewModel Context;

        public static IDock? Root => (ViewModel.Layout?.VisibleDockables?[0] as IDock)?.VisibleDockables?[0] as IDock;
        public DockFactory(AppViewModel context) => Context = context;

        public override IRootDock CreateLayout()
        {
            IDockable[] dockables = ToolDockFactory.Build<ToolDockModel>().ToArray();
            Context.Factory = this;

            var dockLayout = new ProportionalDock {
                Id = "DockLayout",
                Title = "Dock Layout",
                Proportion = double.NaN,
                Orientation = Orientation.Horizontal,
                VisibleDockables = CreateList<IDockable>(
                    new ToolDock() {
                        Id = "Tools",
                        Title = "Tools",
                        ActiveDockable = dockables[0],
                        VisibleDockables = CreateList(dockables)
                    },
                    new ProportionalDockSplitter() {
                        Id = "Splitter",
                        Title = "Splitter"
                    },
                    new DocumentDock() {
                        Id = "ActorDocuments",
                        Title = "Actor Documents",
                        Proportion = 0.75
                    }
                )
            };

            RootDock rootDock = new() {
                Id = "RootLayout",
                Title = "RootLayout",
                ActiveDockable = dockLayout,
                VisibleDockables = CreateList<IDockable>(dockLayout)
            };

            IRootDock root = CreateRootDock();
            root.Id = "RootDock";
            root.Title = "RootDock";
            root.ActiveDockable = rootDock;
            root.DefaultDockable = rootDock;
            root.VisibleDockables = CreateList<IDockable>(rootDock);

            // Update static layout
            Context.Layout = root;

            // Load saved layout
            if (File.Exists($"{Config.DataFolder}/layout.idock")) {
                Context.Layout = root.Load($"{Config.DataFolder}/layout.idock");
            }

            return (IRootDock)Context.Layout;
        }

        public override void InitLayout(IDockable layout)
        {
            HostWindowLocator = new Dictionary<string, Func<IHostWindow>> {
                [nameof(IDockWindow)] = () => {
                    var hostWindow = new HostWindow() {
                        [!Window.TitleProperty] = new Binding("ActiveDockable.Title")
                    };

                    return hostWindow;
                }
            };

            base.InitLayout(layout);
        }
    }
}
