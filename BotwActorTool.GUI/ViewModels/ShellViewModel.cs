using Avalonia.Controls;
using BotwActorTool.GUI.Models;
using Material.Icons;
using System.Diagnostics;

namespace BotwActorTool.GUI.ViewModels
{
    /// <summary>
    /// Embedded Root ViewModel
    /// </summary>
    public class ShellViewModel : ReactiveObject
    {
        private UserControl? defaultContent;
        private UserControl? content;
        public UserControl? Content
        {
            get => content;
            set
            {
                defaultContent ??= value;
                if (value != null)
                {
                    this.RaiseAndSetIfChanged(ref content, value);
                }
                else
                {
                    this.RaiseAndSetIfChanged(ref content, defaultContent);
                }
            }
        }

        private string modContext = " ";
        public string ModContext {
            get => modContext;
            set => TruncatedModContext = value;
        }

        public string TruncatedModContext {
            get {
                if (!string.IsNullOrEmpty(modContext) && modContext.Length > 3) {
                    var truncated = modContext[3..][(modContext.Length > 100 ? modContext.Length - 100 : 0)..];
                    return $"{modContext[0..3]}{(truncated.Length < modContext.Length - 3 ? " ... " : "")}{truncated}";    
                }

                return modContext;
            }
            private set => this.RaiseAndSetIfChanged(ref modContext, value);
        }

        public void OpenModFolder()
        {
            Process.Start("explorer.exe", Meta.IsWindows ? ModContext.Replace("/", "\\") : ModContext);
        }

        public void ChangeState() => Shell.WindowState = Shell.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        public void Minimize() => Shell.WindowState = WindowState.Minimized;
        public void Quit() => AppMenuModel.Quit();

        private bool isMaximized = false;
        public bool IsMaximized
        {
            get => isMaximized;
            set => this.RaiseAndSetIfChanged(ref isMaximized, value);
        }

        private bool isEdited = false;
        public bool IsEdited
        {
            get => isEdited;
            set => this.RaiseAndSetIfChanged(ref isEdited, value);
        }

        private bool isLoading = false;
        public bool IsLoading
        {
            get => isLoading;
            set => this.RaiseAndSetIfChanged(ref isLoading, value);
        }

        private MaterialIconKind statusIcon = MaterialIconKind.CardsOutline;
        public MaterialIconKind StatusIcon
        {
            get => statusIcon;
            set => this.RaiseAndSetIfChanged(ref statusIcon, value);
        }

        private string status = "Ready";
        public string Status
        {
            get => status;
            set => this.RaiseAndSetIfChanged(ref status, value);
        }
    }
}
