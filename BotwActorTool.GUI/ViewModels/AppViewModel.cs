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
using BotwActorTool.GUI.Extensions;
using System;

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

        public async void OpenModActor(string actorpack) => await OpenActor(actorpack, ModContext);
        public async void OpenGameActor(string actorpack) => await OpenActor(actorpack, Config.GetDir(Dir.Update));
        public async Task OpenActor(string actorpack, string modRoot)
        {
            SetStatus("Unpacking Actor", MaterialIconKind.SemanticWeb);

            try {

                var actorDoc = new ActorViewModel($"{modRoot}/{Util.GetActorRelPath(actorpack)}".ToSystemPath()) {
                    Title = actorpack.Length >= 20  ? actorpack[0..14] + "..." + actorpack[(actorpack.Length-6)..actorpack.Length] : actorpack,
                    Id = actorpack,
                };

                if (DocumentDock == null) {
                    Factory.CreateLayout();
                    Factory.InitLayout(Layout);
                }

                if (DocumentDock!.VisibleDockables == null) {
                    DocumentDock!.VisibleDockables = Factory.CreateList<IDockable>(actorDoc);
                }
                else {
                    foreach (var doc in DocumentDock.VisibleDockables) {
                        if (doc.Id != actorpack || await View.ShowMessageBox(
                                $"An actor with the name '{actorpack}' is already open. Are you sure you want to open it again?", "Warning", MessageBoxButtons.YesNo
                            ) == MessageBoxResult.Yes) {
                            DocumentDock.VisibleDockables.Add(actorDoc);
                            break;
                        }
                    }
                }

                DocumentDock.ActiveDockable = actorDoc;
            }
            catch (Exception ex) {
                await View.ShowMessageBox(ex.ToString(), actorpack);
            }

            SetStatus();
        }

        public async void OpenActorFile(string file)
        {

        }

        //
        // Properties

        private BrowserViewModel currentMod;
        public BrowserViewModel CurrentMod {
            get => currentMod;
            set => this.RaiseAndSetIfChanged(ref currentMod, value);
        }

        private string modContext;
        public string ModContext {
            get => modContext;
            set => this.RaiseAndSetIfChanged(ref modContext, value);
        }
        
        //
        // Dock References

        public IDock layoutRoot;
        public ToolDock ToolDock => (ToolLayout.VisibleDockables![0] as ToolDock)!;
        public DocumentDock? DocumentDock => Layout.VisibleDockables!.Count > 0 ? Layout.VisibleDockables[0] as DocumentDock : null;
        public ActorViewModel? CurrentActor => DocumentDock != null ? DocumentDock.ActiveDockable as ActorViewModel : null;

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
