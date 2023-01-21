using Avalonia.Generics.Dialogs;
using BotwActorTool.Models;
using Dock.Model.ReactiveUI.Controls;
using System.Collections.ObjectModel;

namespace BotwActorTool.ViewModels.Tools
{
    public class ModBrowserViewModel : Tool
    {
        public ModBrowserViewModel()
        {
            ModNodeModel mod = new("ModName", "ModDescription");
            mod.Actors.Add(new("ActorName", "ActorDescription"));
            Items.Add(mod);
        }

        private ObservableCollection<ModNodeModel> _items = new();
        public ObservableCollection<ModNodeModel> Items {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        private object? _selectedItem;
        public object? SelectedItem {
            get => _selectedItem;
            set {
                this.RaiseAndSetIfChanged(ref _selectedItem, value);
            }
        }

        private string _searchField = string.Empty;
        public string SearchField {
            get => _searchField;
            set {
                this.RaiseAndSetIfChanged(ref _searchField, value);
                Search(value.ToLower());
            }
        }

        public void Search(string str)
        {
            foreach (var mod in Items) {
                mod.Visible = mod.Search(str);
            }
        }

        public async Task CloseMod()
        {
            // INVOKE OPEN
            // - Report UI as working
            // - Build View (async)
            // - Inject into DocumentDock
            // - End working
            await MessageBox.ShowDialog("INVOKE CLOSE");
        }
    }
}
