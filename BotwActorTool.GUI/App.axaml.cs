using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Fluent;
using BotwActorTool.GUI.Helpers;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.GUI.Views;
using Dock.Model.Core;
using Material.Icons;
using System;
using System.Threading.Tasks;

namespace BotwActorTool.GUI
{
    public partial class App : Application
    {
        public static AppView View { get; set; } = null!;
        public static AppViewModel ViewModel { get; set; } = null!;
        public static FluentTheme Fluent { get; set; } = new(new Uri("avares://BotwActorTool.GUI/Styles"));

        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override async void OnFrameworkInitializationCompleted()
        {
            // Load the user config
            LoadConfig();

            Fluent.Mode = Config.IsDarkTheme ? FluentThemeMode.Dark : FluentThemeMode.Light;
            Current!.Styles[0] = Fluent;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {

                // Create desktop instance
                View = new();
                desktop.MainWindow = View;

                // Create data context
                ViewModel = new();
                View.DataContext = ViewModel;

                // Build the menu
                View.FindControl<Menu>("MainMenu")!.Items = MenuFactory.Generate(ViewModel);

                // Make sure settings are always set
                if (Config.Lang == "NULL") {
                    ViewModel.SettingsView = new(View, canClose: false);
                    ViewModel.SetStatus("Waiting for settings input", MaterialIconKind.BoxVariant);

                    await Task.Run(() => {
                        while (Config.Lang == "NULL")
                            Task.Delay(100);
                    });

                    ViewModel.SetStatus();
                }

                // Create actor dock
                var factory = new AppDockFactory(ViewModel);
                var layout = factory.CreateLayout();
                layout.VisibleDockables!.Clear();

                // Create tools dock
                var toolFactory = new ToolDockFactory(ViewModel);
                var toolLayout = toolFactory.CreateLayout();
                toolFactory.InitLayout(toolLayout);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
