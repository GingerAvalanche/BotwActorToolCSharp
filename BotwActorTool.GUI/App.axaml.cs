using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Fluent;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.GUI.Views;
using Dock.Model.Core;
using System;

namespace BotwActorTool.GUI
{
    public partial class App : Application
    {
        public static FluentTheme Fluent = new(new Uri("avares://BotwActorTool.GUI/Styles"));
        public override void Initialize() => AvaloniaXamlLoader.Load(this);
        public override void OnFrameworkInitializationCompleted()
        {
            // Load the user config
            LoadConfig();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {

                // Create desktop instance
                AppView appView = new();
                desktop.MainWindow = appView;

                // Create data context
                AppViewModel dataContext = new(appView);
                appView.DataContext = dataContext;

                // Create actor dock
                var factory = new AppDockFactory(dataContext);
                var layout = factory.CreateLayout();
                factory.InitLayout(layout);

                // Create tools dock
                var toolFactory = new ToolDockFactory(dataContext);
                var toolLayout = toolFactory.CreateLayout();
                toolFactory.InitLayout(toolLayout);
            }

            Fluent.Mode = Config.IsDarkTheme ? FluentThemeMode.Dark : FluentThemeMode.Dark;
            if (Current != null) {
                Current.Styles[0] = Fluent;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
