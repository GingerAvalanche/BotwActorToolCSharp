#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Avalonia.Data;
using BotwActorTool.GUI.ViewModels;
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
    public class AppDockFactory : Factory
    {
        private IDocumentDock documentDock;
        private readonly AppViewModel context;

        public AppDockFactory(AppViewModel _context) => context = _context;

        public override IRootDock CreateLayout()
        {
            DocumentDock documentDock = new() {
                Id = "ActorsDock",
                Title = "ActorsDock",
                Proportion = double.NaN,
                CanCreateDocument = false,
            };

            IRootDock root = CreateRootDock();

            root.Id = "RootDock";
            root.Title = "RootDock";
            root.VisibleDockables = CreateList<IDockable>(documentDock);

            this.documentDock = documentDock;

            context.Factory = this;
            context.Layout = root;

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

            SetActiveDockable(documentDock);
            SetFocusedDockable(documentDock, documentDock.VisibleDockables?.FirstOrDefault());
        }
    }
}
