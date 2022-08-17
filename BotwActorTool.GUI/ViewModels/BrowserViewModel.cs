using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using BotwActorTool.GUI.Extensions;
using BotwActorTool.GUI.Views;
using BotwActorTool.Lib;
using Dock.Model.ReactiveUI.Controls;
using Nintendo.Byml;
using Nintendo.Yaz0;
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
        private Dictionary<string, string> baseRoot = new();
        public Dictionary<string, string> BaseRoot {
            get => baseRoot;
            set {
                baseRoot = value;
                Root = new(value);
            }
        }

        public void SetRoot(object? obj = null, bool isMod = false)
        {
            if (obj is string str) {

                BymlFile actorInfo = new(Yaz0.DecompressFast(Util.GetFileAnywhere(str, "/Actor/ActorInfo.product.sbyml")));

                foreach (var actor in actorInfo.RootNode.Hash["Actors"].Array) {
                    if (actor.Hash["name"].String is string name) {

                        if (isMod && !File.Exists($"{Util.GetModRoot(str)}\\Actor\\Pack\\{name}.sbactorpack".ToSystemPath())) {
                            continue;
                        }

                        string desc = string.Join("\n",
                            "Name: " + "WIP",
                            "Bfres: " + (actor.Hash.ContainsKey("bfres") ? actor.Hash["bfres"].String : "∅"),
                            "Model: " + (actor.Hash.ContainsKey("mainModel") ? actor.Hash["mainModel"].String : "∅"),
                            "Profile: " + (actor.Hash.ContainsKey("profile") ? actor.Hash["profile"].String : "∅")
                        );

                        if (!name.EndsWith("_Far")) {
                            baseRoot[name] = desc;
                        }
                    }
                }
            }
            else if (obj is Dictionary<string, string> dict) {
                baseRoot = dict;
            }

            BaseRoot = baseRoot;
        }

        public BrowserViewModel(OnSelectionExecute onSelectionExecuted, object? obj = null, bool isMod = false)
        {
            this.onSelectionExecuted = onSelectionExecuted;
            SetRoot(obj, isMod);
        }

        private SortedDictionary<string, string> root = new();
        public SortedDictionary<string, string> Root {
            get => root;
            set => this.RaiseAndSetIfChanged(ref root, value);
        }

        private KeyValuePair<string, string> selected = new();
        public KeyValuePair<string, string> Selected {
            get => selected;
            set => this.RaiseAndSetIfChanged(ref selected, value);
        }

        private string searchField = "";
        public string SearchField {
            get => searchField;
            set {
                this.RaiseAndSetIfChanged(ref searchField, value);

                if (string.IsNullOrEmpty(searchField)) {
                    Root = new(BaseRoot);
                }
                else {
                    Root = new(BaseRoot.Where(
                        s => s.Key.ToLower().Contains(searchField.ToLower())
                    ).OrderBy(
                        entry => entry.Value).ToDictionary(entry => entry.Key, entry => entry.Value)
                    );
                }
            }
        }

        private readonly OnSelectionExecute onSelectionExecuted;
        public void ExecuteSelectionChanged(object _) => onSelectionExecuted(Selected.Key);
    }
}
