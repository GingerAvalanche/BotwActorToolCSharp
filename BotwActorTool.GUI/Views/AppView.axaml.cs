using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using BotwActorTool.GUI.ViewModels;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.Views
{
    public partial class AppView : Window
    {
        public AppViewModel CastDataContent { get; set; }
        public AppView()
        {
            InitializeComponent();
            DataContext = new AppViewModel(this);

            #if DEBUG
            this.AttachDevTools();
            #endif
        }

        protected override void HandleWindowStateChanged(WindowState state)
        {
            if (state == WindowState.Maximized) {
                Padding = new Thickness(7);
                ExtendClientAreaTitleBarHeightHint = 44;
                ((AppViewModel)DataContext).IsMaximized = true;
            }
            else {
                Padding = new Thickness(0);
                ExtendClientAreaTitleBarHeightHint = 30;
                ((AppViewModel)DataContext).IsMaximized = false;
            }

            base.HandleWindowStateChanged(state);
        }

        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
