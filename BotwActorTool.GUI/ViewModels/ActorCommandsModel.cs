using AvaloniaEdit;
using BotwActorTool.GUI.Views.Editors;
using Dock.Model.ReactiveUI.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.ViewModels
{
    public partial class ActorViewModel : Document
    {
        public void Undo()
        {
            if (Editor.Value?.editor != null) {
                (Editor.Value.editor as TextEditor)!.Undo();
            }
        }

        public void Redo()
        {
            if (Editor.Value?.editor != null) {
                (Editor.Value.editor as TextEditor)!.Redo();
            }
        }

        public void Copy()
        {
            if (Editor.Value?.editor != null) {
                (Editor.Value.editor as TextEditor)!.Copy();
            }
        }

        public void Cut()
        {
            if (Editor.Value?.editor != null) {
                (Editor.Value.editor as TextEditor)!.Cut();
            }
        }

        public void Paste()
        {
            if (Editor.Value?.editor != null) {
                (Editor.Value.editor as TextEditor)!.Paste();
            }
        }

        public void SelectAll()
        {
            if (Editor.Value?.editor != null) {
                (Editor.Value.editor as TextEditor)!.SelectAll();
            }
        }

        public void Rename()
        {

        }

        public void ToggleFastTextEditor()
        {
            IsFastMode = isFastMode == _isFastMode ? !isFastMode : isFastMode;
            _isFastMode = isFastMode;

            if (Editor.Value?.editor != null) {
                ((YamlEditor)Editor.Value).SetEditorMode(IsFastMode);
                var editor = Editor.Value.editor as TextEditor;
                editor!.ScrollToLine(editor.Document.GetLocation(0).Line);
            }
        }

        public void Save()
        {
            if (Editor.Value != null)
                Editor.Value.Save(this);
            App.SetStatus($"Saved {ActorFile}", Material.Icons.MaterialIconKind.ContentSaveCheck, false);
        }

        public void Cancel()
        {
            if (Editor.Value != null) {
                Editors = AllEditors[actorFile];
                Editor = Editors.First();
            }
        }

        //
        // Props
        #region Expand

        private bool _isFastMode = false;
        private bool isFastMode = false;
        public bool IsFastMode {
            get => isFastMode;
            set => this.RaiseAndSetIfChanged(ref isFastMode, value);
        }

        #endregion
    }
}
