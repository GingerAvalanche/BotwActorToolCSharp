global using static BotwActorTool.GUI.App;
global using static BotwActorTool.Lib.BatConfig;
global using ReactiveUI;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using System;
using System.IO;
using BotwActorTool.GUI.Dialogs;
using System.Threading.Tasks;
using System.Threading;
using Avalonia.Threading;

namespace BotwActorTool.GUI
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
                using (var source = new CancellationTokenSource()) {
                    MessageBox.Show($"{ex.Message}\n\n(Writting stack to 'error.log')", "Unhandled Exception").ContinueWith(t => source.Cancel(), TaskScheduler.FromCurrentSynchronizationContext());
                    Dispatcher.UIThread.MainLoop(source.Token);
                }
                File.WriteAllText(".\\error.log", ex.ToString());
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        public static async void Throw(Exception ex) => await MessageBox.Show($"{ex.Message}", "Unhandled Exception");
    }
}
