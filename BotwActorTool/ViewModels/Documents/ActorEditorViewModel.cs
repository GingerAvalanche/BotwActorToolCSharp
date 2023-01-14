using BotwActorTool.Attributes;
using Dock.Model.ReactiveUI.Controls;
using System.Diagnostics;

namespace BotwActorTool.ViewModels.Documents
{
    public class ActorEditorViewModel : Document
    {
        public ActorEditorViewModel()
        {
            Title = "DEBUG";
        }

        public Task ToCustomMode()
        {
            // Switch to custom/properties mode
            Debug.WriteLine("ToCustom()");
            return Task.CompletedTask;
        }

        public Task ToEditorMode()
        {
            // Switch to yaml/editor mode
            Debug.WriteLine("ToEditor()");
            return Task.CompletedTask;
        }

        [ToolbarItem("Rename the current file", "fa-solid fa-pen-to-square", GroupId = 1)]
        public Task Rename()
        {
            // Rename
            Debug.WriteLine("Rename()");
            return Task.CompletedTask;
        }

        [ToolbarItem("Save the current document", "fa-solid fa-floppy-disk", GroupId = 1)]
        public Task Save()
        {
            // Save
            Debug.WriteLine("Save()");
            return Task.CompletedTask;
        }

        [ToolbarItem("Cancel editing this document", "fa-solid fa-ban", GroupId = 1)]
        public Task Cancel()
        {
            // Cancel?
            Debug.WriteLine("Cancel()");
            return Task.CompletedTask;
        }
    }
}
