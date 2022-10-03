#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BotwActorTool.GUI.Helpers;
using BotwActorTool.GUI.ViewModels;

namespace BotwActorTool.GUI.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView() => AvaloniaXamlLoader.Load(this);
        public SettingsView(bool canClose = true)
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new SettingsViewModel(canClose);

            // Very much unnecessary, but not having this bothers me.
            // Allows you to focus seemingly nothing.
            Grid focusDelegate = this.FindControl<Grid>("FocusDelegate")!;
            focusDelegate.PointerPressed += (_, _) => focusDelegate.Focus();
            Grid focusDelegate2 = this.FindControl<Grid>("FocusDelegate2")!;
            focusDelegate2.PointerPressed += (_, _) => focusDelegate.Focus();

            SettingsFactory.Generate(this);
        }
    }
}
