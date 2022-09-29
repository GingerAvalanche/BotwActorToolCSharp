using Avalonia.Controls;
using BotwActorTool.GUI.Dialogs;
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

namespace BotwActorTool.GUI.Extensions
{
    public enum MessageBoxButtons { Ok, OkCancel, YesNo, YesNoCancel }
    public enum MessageBoxResult { Cancel, No, Ok, Yes }
    public enum Formatting { None, Markdown }
    public static class ViewExt
    {
        public static Task<MessageBoxResult> ShowMessageBox(this Window _, string text, string title = "Warning", MessageBoxButtons buttons = MessageBoxButtons.Ok,
            Formatting formatting = Formatting.None) => MessageBox.Show(text, title, buttons, formatting);

        public static void SetActive(this DockBase dock, string id)
        {
            var doc = dock.VisibleDockables?.Where(x => x.Id == id).FirstOrDefault();
            dock.ActiveDockable = doc;
        }

        public static IDockable? Get(this DockBase dock, string id)
        {
            return dock.VisibleDockables?.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
