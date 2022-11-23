using Avalonia.Controls;
using Avalonia.Input;
using BotwActorTool.ViewModels.Tools;

namespace BotwActorTool.Views.Tools
{
    public partial class FileBrowserView : UserControl
    {
        public FileBrowserView()
        {
            InitializeComponent();

            SearchField.AttachedToVisualTree += (s, e) => SearchField.Focus();
            SearchField.KeyDown += (s, e) => {
                if (e.Key == Key.Enter) {
                    ItemsListRoot.Focus();
                    ItemsListRoot.SelectedIndex = 0;
                }
            };

            ItemsListRoot.KeyDown += (s, e) => {
                if (e.Key == Key.Down) {
                    ItemsListRoot.Focus();
                    ItemsListRoot.SelectedIndex += 1;
                }
                else if (e.Key == Key.Up) {
                    ItemsListRoot.SelectedIndex -= 1;
                }
                else if (e.KeyModifiers == KeyModifiers.Alt && e.Key == Key.Enter) {
                    (DataContext as FileBrowserViewModel)!.ActorInfo();
                }
                else if (e.KeyModifiers == KeyModifiers.Control && e.Key == Key.Enter) {
                    (DataContext as FileBrowserViewModel)!.CreateCopy();
                }
                else if (e.Key == Key.Enter) {
                    (DataContext as FileBrowserViewModel)!.Open();
                }
            };
        }

        public void OpenEvent(object? sender, TappedEventArgs e) => (DataContext as FileBrowserViewModel)!.Open();
    }
}
