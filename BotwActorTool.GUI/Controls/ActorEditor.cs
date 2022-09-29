using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.TextMate;
using BotwActorTool.GUI.Dialogs;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.GUI.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMateSharp.Grammars;

namespace BotwActorTool.GUI.Controls
{
    public class ActorEditor : UserControl
    {
        public dynamic? editor;
        public virtual void Init(ActorViewModel doc)
        {
            if (editor == null) {
                return;
            }

            if (editor is TextEditor) {
                try {
                    editor!.Document = new TextDocument(doc.Actor.GetLinkData(doc.LinkKeys[doc.ActorFile]));
                }
                catch (Exception ex) {
                    editor!.Document = new TextDocument(ex.ToString());
                }
            }
        }

        public virtual async void Save(ActorViewModel doc)
        {
            if (editor is TextEditor) {
                try {
                    doc.Actor.SetLinkData(doc.LinkKeys[doc.ActorFile], (editor as TextEditor)!.Text);
                }
                catch (Exception ex) {
                    await MessageBox.Show(ex.ToString(), "Error Saving Editor");
                }
            }
        }
    }
}
