#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BotwActorTool.GUI.ViewModels;

namespace BotwActorTool.GUI.Views
{
    public partial class SettingsView : UserControl
    {
        public Window Window { get; set; }
        public SettingsView() => AvaloniaXamlLoader.Load(this);
        public SettingsView(Window view)
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new SettingsViewModel(this);
            Window = view;

            Grid root = this.FindControl<Grid>("Root");
            root.PointerPressed += (_, _) => root.Focus();

            (view.DataContext as AppViewModel).IsSettingsOpen = true;
        }
    }
}
