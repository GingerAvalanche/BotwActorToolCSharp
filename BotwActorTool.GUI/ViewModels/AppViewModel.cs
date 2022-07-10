﻿#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Avalonia.Controls;
using Avalonia.Themes.Fluent;
using BotwActorTool.GUI.Views;
using Dock.Model.Core;
using ReactiveUI;
using System.Runtime.InteropServices;
using Material.Icons;
using BotwActorTool.Lib;
using Dock.Model.ReactiveUI.Controls;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.ViewModels
{
    public partial class AppViewModel : ReactiveObject
    {
        public void SetStatus(string msg = "Ready", MaterialIconKind icon = MaterialIconKind.CardsOutline, bool? isLoading = null)
        {
            IsLoading = isLoading == null ? !IsLoading : (bool)isLoading;
            Status = msg;
            StatusIcon = icon;
        }

        public void OpenActor(string actorpack, string name)
        {
            SetStatus("Unpacking Actor", MaterialIconKind.SemanticWeb);
            DocumentDock.VisibleDockables.Add(new ActorViewModel(new(PathExtensions.ToAltPathSeparator(actorpack))) { Title = name });
            SetStatus();
        }
        
        //
        // Dock References
        public DocumentDock DocumentDock => Layout.VisibleDockables[0] as DocumentDock;
        public ActorViewModel CurrentActor => DocumentDock.ActiveDockable as ActorViewModel;


        //
        // App References

        AppView View { get; set; }
        public bool IsWindows { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public void ChangeState(string parameter) => View.WindowState = View.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        public void Minimize(string parameter) => View.WindowState = WindowState.Minimized;

        private IFactory factory;
        public IFactory Factory {
            get => factory;
            set => this.RaiseAndSetIfChanged(ref factory, value);
        }

        private IDock layout;
        public IDock Layout {
            get => layout;
            set => this.RaiseAndSetIfChanged(ref layout, value);
        }

        private IFactory toolFactory;
        public IFactory ToolFactory {
            get => toolFactory;
            set => this.RaiseAndSetIfChanged(ref toolFactory, value);
        }

        private IDock toolLayout;
        public IDock ToolLayout {
            get => toolLayout;
            set => this.RaiseAndSetIfChanged(ref toolLayout, value);
        }

        private bool isMaximized = false;
        public bool IsMaximized {
            get => isMaximized;
            set => this.RaiseAndSetIfChanged(ref isMaximized, value);
        }

        private bool isEdited = false;
        public bool IsEdited {
            get => isEdited;
            set => this.RaiseAndSetIfChanged(ref isEdited, value);
        }

        private bool isSettingsOpen = false;
        public bool IsSettingsOpen {
            get => isSettingsOpen;
            set => this.RaiseAndSetIfChanged(ref isSettingsOpen, value);
        }

        private bool isLoading = false;
        public bool IsLoading {
            get => isLoading;
            set => this.RaiseAndSetIfChanged(ref isLoading, value);
        }

        private MaterialIconKind statusIcon = MaterialIconKind.CardsOutline;
        public MaterialIconKind StatusIcon {
            get => statusIcon;
            set => this.RaiseAndSetIfChanged(ref statusIcon, value);
        }

        private string status = "Ready";
        public string Status {
            get => status;
            set => this.RaiseAndSetIfChanged(ref status, value);
        }

        private SettingsView? settingsView;
        public SettingsView? SettingsView {
            get => settingsView;
            set => this.RaiseAndSetIfChanged(ref settingsView, value);
        }

        public AppViewModel(AppView view)
        {
            View = view;
        }
    }
}