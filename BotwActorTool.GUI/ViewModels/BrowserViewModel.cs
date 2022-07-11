using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using BotwActorTool.GUI.Extensions;
using BotwActorTool.Lib;
using Dock.Model.ReactiveUI.Controls;
using Nintendo.Byml;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaz0Library;

namespace BotwActorTool.GUI.ViewModels
{
    public delegate void OnSelectionExecute(string field, string modRoot);
    public class BrowserViewModel : Tool
    {
        private readonly Dictionary<string, Dictionary<string, string>> baseRoot = new();
        public BrowserViewModel(OnSelectionExecute _onSelectionExecuted, object? obj = null)
        {
            onSelectionExecuted = _onSelectionExecuted;

            if (obj == null) {

                baseRoot["Normal"] = new();
                baseRoot["Far"] = new();

                BymlFile actorInfo = new(Yaz0.DecompressFast(Util.GetFileAnywhere(Config.GetDir(Dir.Update), "/Actor/ActorInfo.product.sbyml")));

                foreach (var actor in actorInfo.RootNode.Hash["Actors"].Array) {
                    if (actor.Hash["name"].String is string name) {

                        string desc = string.Join("\n",
                            "Name: " + "WIP",
                            "Bfres: " + (actor.Hash.ContainsKey("bfres") ? actor.Hash["bfres"].String : "???"),
                            "Model: " + (actor.Hash.ContainsKey("mainModel") ? actor.Hash["mainModel"].String : "???"),
                            "Profile: " + (actor.Hash.ContainsKey("profile") ? actor.Hash["profile"].String : "???")
                        );

                        if (name.EndsWith("_Far")) {
                            baseRoot["Far"][name] = desc;
                        }
                        else {
                            baseRoot["Normal"][name] = desc;
                        }
                    }
                }
            }
            else if (obj is List<string> list) {
                // baseRoot = new(list);
            }

            BrowseMode = BrowsingModes.First();
            Root =new(baseRoot[BrowseMode]);
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
                    Root = new(baseRoot[BrowseMode]);
                }
                else {
                    Root = new(baseRoot[BrowseMode].Where(
                        s => s.Key.ToLower().Contains(searchField.ToLower())
                    ).OrderBy(
                        entry => entry.Value).ToDictionary(entry => entry.Key, entry => entry.Value)
                    );
                }
            }
        }

        public List<string> BrowsingModes => baseRoot.Keys.ToList();

        private string browseMode = "Vanilla";
        public string BrowseMode {
            get => browseMode;
            set {
                this.RaiseAndSetIfChanged(ref browseMode, value);
                SearchField = searchField;
            }
        }

        private readonly OnSelectionExecute onSelectionExecuted;
        public void ExecuteSelectionChanged(object _) => onSelectionExecuted(Selected.Key, BrowseMode);
    }
}
