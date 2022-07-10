using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using BotwActorTool.GUI.Extensions;
using Dock.Model.ReactiveUI.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.ViewModels
{
    public delegate void OnSelectionExecute(string field);
    public class BrowserViewModel : Tool
    {
        private readonly List<string> baseRoot = new();
        public BrowserViewModel(object obj, OnSelectionExecute _onSelectionExecuted)
        {
            onSelectionExecuted = _onSelectionExecuted;

            if (obj is string str) {
                foreach (var file in Directory.GetFiles(str)) {
                    var fname = Path.GetFileNameWithoutExtension(file);
                    baseRoot.Add(fname);
                }
            }
            else if (obj is List<string> list) {
                baseRoot = new(list);
            }

            Root = baseRoot;
        }

        private List<string> root = new();
        public List<string> Root {
            get => root;
            set => this.RaiseAndSetIfChanged(ref root, value);
        }

        private string selected = "";
        public string Selected {
            get => selected;
            set => this.RaiseAndSetIfChanged(ref selected, value);
        }

        private string searchField = "";
        public string SearchField {
            get => searchField;
            set {
                this.RaiseAndSetIfChanged(ref searchField, value);

                if (string.IsNullOrEmpty(searchField)) {
                    Root = baseRoot;
                }
                else {
                    Root = new(baseRoot.Where(s => s.ToLower().Contains(searchField.ToLower())));
                }
            }
        }

        private string browseMode = "Vanilla";
        public string BrowseMode {
            get => browseMode;
            set => this.RaiseAndSetIfChanged(ref browseMode, value);
        }

        private List<string> browsingModes = new() {
            "Vanilla",
            "TitleBG"
        };
        public List<string> BrowsingModes {
            get => browsingModes;
            set => this.RaiseAndSetIfChanged(ref browsingModes, value);
        }

        private readonly OnSelectionExecute onSelectionExecuted;
        public void ExecuteSelectionChanged(object _) => onSelectionExecuted(Selected);
    }
}
