#pragma warning disable CS8602 // Dereference of a possibly null reference.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using BotwActorTool.GUI.ViewModels;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.Views
{
    public partial class AppView : Window
    {
        private readonly DispatcherTimer loader = new();

        public AppView()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
            #endif

            loader.Interval = new TimeSpan(0, 0, 0, 0, 400);
            loader.Tick += (_, _) => {
                if (ViewModel.IsLoading == true) {
                    ViewModel.Status += " .";
                    ViewModel.Status = ViewModel.Status.Replace(" . . . .", " .");
                }
            };

            loader.Start();
        }

        protected override void HandleWindowStateChanged(WindowState state)
        {
            if (state == WindowState.Maximized) {
                Padding = new Thickness(7);
                ExtendClientAreaTitleBarHeightHint = 44;
                (DataContext as AppViewModel).IsMaximized = true;
            }
            else {
                Padding = new Thickness(0);
                ExtendClientAreaTitleBarHeightHint = 30;
                (DataContext as AppViewModel).IsMaximized = false;
            }

            base.HandleWindowStateChanged(state);
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
