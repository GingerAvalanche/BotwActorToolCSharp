using Avalonia.Controls;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.GUI.Views;
using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;
using Dock.Model.ReactiveUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotwActorTool.GUI.Views.MessageBox;

namespace BotwActorTool.GUI.Extensions
{
    public enum MessageBoxButtons { Ok, OkCancel, YesNo, YesNoCancel }
    public enum MessageBoxResult { Cancel, No, Ok, Yes }
    public enum Formatting { None, Markdown }
    public static class ViewExt
    {
        public static Task<MessageBoxResult> ShowMessageBox(this Window parent, string text, string title = "Warning", MessageBoxButtons buttons = MessageBoxButtons.Ok,
            Formatting formatting = Formatting.None) => Show(text, title, buttons, parent, formatting);

        public static void SetActive(this DockBase dock, string id)
        {
            foreach (var doc in dock.VisibleDockables!) {
                if (doc.Id == id) {
                    dock.ActiveDockable = doc;
                    return;
                }
            }
        }

        public static IDockable? Get(this DockBase dock, string id)
        {
            foreach (var doc in dock.VisibleDockables!) {
                if (doc.Id == id) {
                    return doc;
                }
            }

            return null;
        }
    }
}
