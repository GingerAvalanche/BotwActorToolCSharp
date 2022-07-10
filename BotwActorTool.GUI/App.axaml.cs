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

            // Create actor dock
            var factory = new AppDockFactory(null);
            var layout = factory.CreateLayout();
            factory.InitLayout(layout);

            // Create tools dock
            var toolFactory = new ToolDockFactory(null);
            var toolLayout = toolFactory.CreateLayout();
            toolFactory.InitLayout(toolLayout);

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {

                AppView appView = new();
                appView.DataContext = new AppViewModel(appView, factory, layout, toolFactory, toolLayout);
                desktop.MainWindow = appView;
            }

            Fluent.Mode = Config.IsDarkTheme ? FluentThemeMode.Dark : FluentThemeMode.Dark;
            if (Current != null) {
                Current.Styles[0] = Fluent;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
