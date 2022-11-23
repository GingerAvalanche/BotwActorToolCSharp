#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Generics;
using Avalonia.Generics.Builders;
using Avalonia.Generics.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using BotwActorTool.Builders;
using BotwActorTool.Models;
using BotwActorTool.ViewModels;
using BotwActorTool.Views;
using Material.Icons;
using System;
using System.Threading.Tasks;

namespace BotwActorTool
{
    public partial class App : Application
    {
        public static ShellViewModel ShellViewModel { get; set; }
        public static AppView View { get; set; }
        public static AppViewModel ViewModel { get; set; }
        public static FluentTheme Theme { get; set; } = new(new Uri("avares://BotwActorTool/Styles"));

        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override async void OnFrameworkInitializationCompleted()
        {
            // Load the user config
            LoadConfig();

            Theme.Mode = Config.Theme == "Dark" ? FluentThemeMode.Dark : FluentThemeMode.Light;
            Current!.Styles[0] = Theme;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                GenericWindow mainWindow = WindowBuilder.Initialize(new ShellView())
                    .WithMenu(new AppMenuModel())
                    .WithWindowColors("SystemChromeLowColor", "SystemChromeLowColor")
                    .Build();

#if DEBUG
                mainWindow.AttachDevTools();
#endif
                desktop.MainWindow = mainWindow;

                // Create default view
                View = new();
                ShellViewModel.Content = View;

                ViewModel = new();
                View.DataContext = ViewModel;

                // Create dock layout
                var factory = new DockFactory(ViewModel);
                var layout = factory.CreateLayout();
                factory.InitLayout(layout);

                ApplicationLoader.Attach(this);

                // Make sure settings are always set
                SettingsView settingsView = new(false);
                if (Config.RequiresInput || settingsView.ValidateSave() != null) {
                    ShellViewModel.Content = settingsView;
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
