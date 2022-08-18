using Avalonia.Controls;
using BotwActorTool.GUI.Controls;
using BotwActorTool.GUI.Views;
using BotwActorTool.Lib;
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
        private string actorFile = "ActorLink";
        public string ActorFile {
            get => actorFile;
            set {
                this.RaiseAndSetIfChanged(ref actorFile, value);
                Editors = AllEditors[actorFile];
                Editor = Editors.First();
                App.SetStatus($"Loaded {actorFile}", Material.Icons.MaterialIconKind.FileCheck, false);
            }
        }

        private List<string> actorFiles = new();
        public List<string> ActorFiles {
            get => actorFiles;
            set => this.RaiseAndSetIfChanged(ref actorFiles, value);
        }

        private KeyValuePair<string, ActorEditor> editor = new();
        public KeyValuePair<string, ActorEditor> Editor {
            get => editor;
            set {
                this.RaiseAndSetIfChanged(ref editor, value);
                if (editor.Value != null) {
                    editor.Value.Init(this);
                }
            }
        }

        private Dictionary<string, ActorEditor> editors = new();
        public Dictionary<string, ActorEditor> Editors {
            get => editors;
            set => this.RaiseAndSetIfChanged(ref editors, value);
        }
        public Dictionary<string, Dictionary<string, ActorEditor>> AllEditors { get; set; } = new();

        public Dictionary<string, string> LinkKeys { get; set; } = new();
        public AppView View { get; }
        public AppViewModel App { get; }
        public Actor Actor { get; set; }
        public ActorViewModel(AppView view, Actor actorpack)
        {
            Actor = actorpack;
            App = (view.DataContext as AppViewModel)!;
            View = view;
        }
    }
}
