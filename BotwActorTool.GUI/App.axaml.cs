using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Fluent;
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
        public static FluentTheme Fluent = new(new Uri("avares://BotwActorTool.GUI/Styles"));
        public override void Initialize() => AvaloniaXamlLoader.Load(this);
        public override async void OnFrameworkInitializationCompleted()
        {
            // Load the user config
            LoadConfig();

            Fluent.Mode = Config.IsDarkTheme ? FluentThemeMode.Dark : FluentThemeMode.Dark;
            if (Current != null) {
                Current.Styles[0] = Fluent;
            }

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {

                // Create desktop instance
                AppView appView = new();
                desktop.MainWindow = appView;

                // Create data context
                AppViewModel dataContext = new(appView);
                appView.DataContext = dataContext;

                // Make sure settings are always set
                if (Config.Lang == "NULL") {
                    ((AppViewModel)appView.DataContext).SettingsView = new(appView, canClose: false);
                    ((AppViewModel)appView.DataContext).SetStatus("Waiting for settings input", MaterialIconKind.BoxVariant);

                    await Task.Run(() => {
                        while (Config.Lang == "NULL")
                            Task.Delay(100);
                    });

                    ((AppViewModel)appView.DataContext).SetStatus();
                }

                // Create actor dock
                var factory = new AppDockFactory(dataContext);
                var layout = factory.CreateLayout();
                layout.VisibleDockables!.Clear();

                // Create tools dock
                var toolFactory = new ToolDockFactory(dataContext);
                var toolLayout = toolFactory.CreateLayout();
                toolFactory.InitLayout(toolLayout);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
