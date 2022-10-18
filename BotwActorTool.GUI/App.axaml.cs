#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using BotwActorTool.GUI.Builders;
using BotwActorTool.GUI.Models;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.GUI.Views;
using Material.Icons;
using System;
using System.Threading.Tasks;

namespace BotwActorTool.GUI
{
    public partial class App : Application
    {
        public static ShellView Shell { get; set; }
        public static ShellViewModel ShellViewModel { get; set; }
        public static AppView View { get; set; }
        public static AppViewModel ViewModel { get; set; }
        public static FluentTheme Theme { get; set; } = new(new Uri("avares://BotwActorTool.GUI/Styles"));

        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override async void OnFrameworkInitializationCompleted()
        {
            // Load the user config
            LoadConfig();

            Theme.Mode = Config.Theme == "Dark" ? FluentThemeMode.Dark : FluentThemeMode.Light;
            Current!.Styles[0] = Theme;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {

                // Create desktop shell
                Shell = new();
                desktop.MainWindow = Shell;

                ShellViewModel = new();
                Shell.DataContext = ShellViewModel;

                // Create default view
                View = new();
                ShellViewModel.Content = View;

                ViewModel = new();
                View.DataContext = ViewModel;

                // Create dock layout
                var factory = new DockFactory(ViewModel);
                var layout = factory.CreateLayout();
                factory.InitLayout(layout);

                // Build the menu
                Shell.FindControl<Menu>("MenuRoot")!.Items = MenuFactory.Generate(new AppMenuModel());

                // Make sure settings are always set
                SettingsView view = new(false);
                if (Config.RequiresInput || view.ValidateSave() != null) {
                    ShellViewModel.Content = view;
                    SetStatus("Waiting for settings input", MaterialIconKind.BoxVariant);

                    await Task.Run(() => {
                        while (Config.RequiresInput)
                            Task.Delay(100);
                    });

                    SetStatus("Ready");
                }
            }

            base.OnFrameworkInitializationCompleted();
        }

        public static void SetStatus(string status, MaterialIconKind icon = MaterialIconKind.CardsOutline, bool? isLoading = null)
        {
            ShellViewModel.IsLoading = isLoading == null ? !ShellViewModel.IsLoading : (bool)isLoading;
            ShellViewModel.Status = status;
            ShellViewModel.StatusIcon = icon;
        }
    }
}
