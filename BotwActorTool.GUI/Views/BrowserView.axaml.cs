using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BotwActorTool.GUI.ViewModels;

namespace BotwActorTool.GUI.Views
{
    public partial class BrowserView : UserControl
    {
        public BrowserView()
        {
            AvaloniaXamlLoader.Load(this);

            // Kinda stupid work-around for event
            // binding, but it works for now.
            this.FindControl<ListBox>("Root").DoubleTapped += (s, e) => {
                (DataContext as BrowserViewModel)!.ExecuteSelectionChanged(null!);
            };

            var searchField = this.FindControl<TextBox>("SearchField");
            searchField.AttachedToVisualTree += (s, e) => searchField.Focus();
        }
    }
}
