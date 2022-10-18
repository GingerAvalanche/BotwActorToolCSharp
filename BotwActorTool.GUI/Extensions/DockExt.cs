using BotwActorTool.GUI.Builders;
using BotwActorTool.GUI.Models;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;
using Dock.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.Extensions
{
    public static class DockExt
    {
        public static IRootDock Load(this IRootDock layout, string path)
        {
            try {
                IRootDock rootDock = new DockSerializer(typeof(ObservableCollection<>)).Load<IRootDock>(path);
                rootDock.QueryDockables();

                return rootDock;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
                return layout;
            }
        }

        private static void QueryDockables(this IDock rootDock)
        {
            var docks = rootDock.VisibleDockables!;
            for (int i = 0; i < docks.Count; i++) {
                if (ToolDockFactory.ToolDocks.Contains(docks[i].Id)) {
                    docks[i] = ToolDockFactory.QueryTool(out IDockable? _, docks[i].Id) ?? docks[i];
                }
                if (docks[i] is IDock dock) {

                    if (dock.VisibleDockables != null) {
                        dock.QueryDockables();
                    }
                }
            }
        }

        public static bool Set(this IDock parent, string id, IDockable newDock)
        {
            var docks = parent.VisibleDockables!;
            for (int i = 0; i < docks.Count; i++) {
                if (docks[i].Id == id) {
                    newDock.Id = docks[i].Id;
                    newDock.Title = docks[i].Title;
                    docks[i] = newDock;
                    return true;
                }
            }

            return false;
        }

        public static IDockable? Get(this IDock parentDock, string id)
        {
            return parentDock.VisibleDockables?.Where(x => x.Id == id).FirstOrDefault();
        }

        public static T? Get<T>(this IDock parentDock, string id) where T : IDock
        {
            return (T)parentDock.VisibleDockables?.Where(x => x.Id == id).FirstOrDefault()!;
        }
    }
}
