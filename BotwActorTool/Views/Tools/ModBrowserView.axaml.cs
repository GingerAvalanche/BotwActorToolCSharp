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
                if (((TreeView)s!).SelectedItem is ActorNodeModel actorNode) {
                    if (e.KeyModifiers == KeyModifiers.Alt && e.Key == Key.Enter) {
                        await actorNode.ActorInfo();
                    }
                    else if (e.KeyModifiers == KeyModifiers.Control && e.Key == Key.Enter) {
                        await actorNode.CreateCopy();
                    }
                    else if (e.Key == Key.Enter) {
                        await actorNode.Open();
                    }
                }
            };
        }

        public async void OpenEvent(object? sender, TappedEventArgs e)
        {
            await ((DataContext as ModBrowserViewModel)!.SelectedItem as ActorNodeModel)!.Open();
        }
    }
}
