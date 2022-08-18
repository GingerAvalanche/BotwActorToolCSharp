using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using BotwActorTool.GUI.ViewModels;
using ReactiveUI;

namespace BotwActorTool.GUI.Views
{
    public partial class BrowserView : UserControl
    {
        private ListBox list;
        public BrowserView()
        {
            AvaloniaXamlLoader.Load(this);

            // Kinda stupid work-around for event
            // binding, but it works for now.
            list = this.FindControl<ListBox>("Root");
            list.KeyDown += ListKeyDown;
            list.DoubleTapped += (s, e) => {
                if (((ListBox)s!).SelectedItem != null) {
                    (DataContext as BrowserViewModel)!.ExecuteSelectionChanged(null!);
                }
            };

            var searchField = this.FindControl<TextBox>("SearchField");
            searchField.AttachedToVisualTree += (s, e) => searchField.Focus();
        }

        private void ListKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                if (list.SelectedItem != null) {
                    (DataContext as BrowserViewModel)!.ExecuteSelectionChanged(null!);
                }
            }
        }
    }
}
