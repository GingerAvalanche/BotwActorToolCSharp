using Avalonia.Controls;
using Avalonia.Input;
using BotwActorTool.Models;
using BotwActorTool.ViewModels.Tools;

namespace BotwActorTool.Views.Tools
{
    public partial class ModBrowserView : UserControl
    {
        public ModBrowserView()
        {
            InitializeComponent();

            SearchField.AttachedToVisualTree += (s, e) => SearchField.Focus();

            ItemsTreeView.KeyDown += async (s, e) => {
                if (((TreeView)s!).SelectedItem is ActorNodeModel) {
                    if (e.KeyModifiers == KeyModifiers.Alt && e.Key == Key.Enter) {
                        await (DataContext as ModBrowserViewModel)!.ActorInfo();
                    }
                    else if (e.KeyModifiers == KeyModifiers.Control && e.Key == Key.Enter) {
                        await (DataContext as ModBrowserViewModel)!.CreateCopy();
                    }
                    else if (e.Key == Key.Enter) {
                        await (DataContext as ModBrowserViewModel)!.Open();
                    }
                }
            };
        }

        public async void OpenEvent(object? sender, TappedEventArgs e) => await (DataContext as ModBrowserViewModel)!.CloseMod();
    }
}
