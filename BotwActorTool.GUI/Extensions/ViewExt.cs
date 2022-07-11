using Avalonia.Controls;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.GUI.Views;
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

    public static class ViewExt
    {
        public static Task<MessageBoxResult> ShowMessageBox(this Window parent, string text, string title = "Warning", MessageBoxButtons buttons = MessageBoxButtons.Ok)
            => Show(text, title, buttons, parent);
    }
}
