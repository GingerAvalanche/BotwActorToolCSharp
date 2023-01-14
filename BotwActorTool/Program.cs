global using ReactiveUI;
global using static BotwActorTool.App;
global using static BotwActorTool.Lib.BatConfig;
using Avalonia;
using Avalonia.Generics.Dialogs;
using Avalonia.Generics.Extensions;
using Avalonia.ReactiveUI;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using Projektanker.Icons.Avalonia.MaterialDesign;

namespace BotwActorTool
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            try {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex) {
                MessageBox.ShowDialog($"{ex.Message}\n\n(Writting stack trace to '{Path.GetFullPath("./error.log")}')", "Unhandled Exception").Await();
                File.WriteAllText("./error.log", ex.ToString());
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .WithIcons(container => container
                    .Register<FontAwesomeIconProvider>()
                    .Register<MaterialDesignIconProvider>())
                .UseReactiveUI();
    }
}
