using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using Material.Icons.Avalonia;
using System;

namespace BotwActorTool.GUI.Views
{
    public partial class ShellView : Window
    {
        private readonly DispatcherTimer Updater = new();

        public ShellView()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif

            Chrome_Fullscreen = this.FindControl<MaterialIcon>("Chrome_Fullscreen")!;
            Chrome_Restore = this.FindControl<MaterialIcon>("Chrome_Restore")!;

            Chrome_Fullscreen.IsVisible = WindowState != WindowState.Maximized;
            Chrome_Restore.IsVisible = WindowState == WindowState.Maximized;

            Updater.Interval = new TimeSpan(0, 0, 0, 0, 400);
            Updater.Tick += (_, _) => {
                if (ShellViewModel.IsLoading == true) {
                    ShellViewModel.Status += " .";
                    ShellViewModel.Status = ShellViewModel.Status.Replace(" . . . . .", " .");
                }
            };

            Updater.Start();
        }

        protected override void HandleWindowStateChanged(WindowState state)
        {
            if (state == WindowState.Maximized) {
                Padding = new Thickness(7);
                ExtendClientAreaTitleBarHeightHint = 44;
                ShellViewModel.IsMaximized = true;
            }
            else {
                Padding = new Thickness(0);
                ExtendClientAreaTitleBarHeightHint = 30;
                ShellViewModel.IsMaximized = false;
            }

            base.HandleWindowStateChanged(state);
        }
    }
}
