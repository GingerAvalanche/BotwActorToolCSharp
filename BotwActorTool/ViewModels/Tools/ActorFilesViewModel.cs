using Avalonia.Generics.Dialogs;
using BotwActorTool.Models;
using Dock.Model.ReactiveUI.Controls;
using System.Collections.ObjectModel;

namespace BotwActorTool.ViewModels.Tools
{
    public class ActorFilesViewModel : Tool
    {
        public ObservableCollection<ActorNodeModel> ItemsBase = new();
        public bool Unloaded => ItemsBase.Count.Equals(0);

        private ActorNodeModel? selectedItem;
        public ActorNodeModel? SelectedItem {
            get => selectedItem;
            set {
                this.RaiseAndSetIfChanged(ref selectedItem, value);
            }
        }

        private ObservableCollection<ActorNodeModel>? items;
        public ObservableCollection<ActorNodeModel> Items {
            get {
                items ??= new(ItemsBase);
                return items;
            }
            set => this.RaiseAndSetIfChanged(ref items, value);
        }

        private string searchField = "";
        public string SearchField {
            get => searchField;
            set {
                this.RaiseAndSetIfChanged(ref searchField, value);
                Items = new(string.IsNullOrEmpty(searchField) ? ItemsBase : ItemsBase.Where(x => x.Name.ToLower().Contains(searchField.ToLower())).OrderBy(x => x.Name));
            }
        }

        public async Task Open()
        {
            // INVOKE OPEN
            // - Report UI as working
            // - Build View (async)
            // - Inject into DocumentDock
            // - End working
            await MessageBox.ShowDialog("INVOKE OPEN");
        }
    }
}
