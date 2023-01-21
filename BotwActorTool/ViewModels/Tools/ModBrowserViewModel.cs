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

        public async Task Open()
        {
            // INVOKE OPEN
            // - Report UI as working
            // - Build View (async)
            // - Inject into DocumentDock
            // - End working
            await MessageBox.ShowDialog("INVOKE OPEN");
        }

        public async Task CreateCopy()
        {
            // INPUT DIALOG
            var name = (await InputDialog.ShowDialog(new Dictionary<string, string>() {
                 { "Name", (SelectedItem as ActorNodeModel)!.Name }
             }, $"Copy {(SelectedItem as ActorNodeModel)!.Name}"))?["Name"];

            if (name != null) {
                // INVOKE OPEN
                // RENAME
            }
        }

        public async Task ActorInfo()
        {
            await MessageBox.ShowDialog((SelectedItem as ActorNodeModel)!.Meta.SerializeNode(), $"Actor Info");
        }
    }
}
