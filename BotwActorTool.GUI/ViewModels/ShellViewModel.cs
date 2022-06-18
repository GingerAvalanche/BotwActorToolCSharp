using BotwActorTool.GUI.ViewResources.Helpers;
using Stylet;
using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace BotwActorTool.GUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        /// 
        /// App parameters
        /// 
        public static int MinHeight { get; set; } = 400;
        public static int MinWidth { get; set; } = 600;
        public static bool CanResize { get; set; } = true;
        public string HelpLink { get; set; } = "https://github.com/ArchLeaders/stylet-wpf-templates";
        public string Title { get; set; } = "BotwActorTool.GUI";

        // Error report settings
        public static bool UseGitHubUpload { get; set; } = false;
        public string GitHubRepo { get; set; } = "stylet-wpf-templates";
        public string DiscordInvite { get; set; } = "https://discord.gg/vPzgy5S";
        public ulong DiscordReportChannel { get; set; } = 961334714633965569;

        ///
        /// Actions
        ///
        #region Actions

        public void GitHub() => System.Operations.Execute.Explorer(HelpLink);
        public void About() => WindowManager.Show(File.ReadAllText($"README.md"), "About", width: 400);
        public void LoadSettings() => SettingsViewModel = new(this);

        #endregion

        ///
        /// Properties
        ///
        #region Properties



        #endregion

        ///
        /// Bindings
        ///
        #region Bindings

        #endregion

        ///
        /// DataContext
        ///
        #region DataContext

        // Views
        private HandledExceptionViewModel? _handledExceptionViewModel = null;
        public HandledExceptionViewModel? HandledExceptionViewModel
        {
            get => _handledExceptionViewModel;
            set => SetAndNotify(ref _handledExceptionViewModel, value);
        }

        private SettingsViewModel? _settingsViewModel = null;
        public SettingsViewModel? SettingsViewModel {
            get => _settingsViewModel;
            set => SetAndNotify(ref _settingsViewModel, value);
        }

        // App
        public IWindowManager WindowManager { get; set; }
        public bool CanFullscreen { get; set; } = CanResize;
        public ResizeMode ResizeMode { get; set; } = CanResize ? ResizeMode.CanResize : ResizeMode.CanMinimize;
        public WindowStyle WindowStyle { get; set; } = CanResize ? WindowStyle.None : WindowStyle.SingleBorderWindow;

        public void ThrowException(HandledExceptionViewModel ex)
        {
            WindowManager.Error(ex.Message, ex.StackText, ex.Title);
            HandledExceptionViewModel = ex;
        }

        public ShellViewModel(IWindowManager windowManager) => WindowManager = windowManager;

        ///
        /// Root Error handling
        /// 
        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception as Exception ?? new();
            File.WriteAllText("error.log.txt", $"[{DateTime.Now}]\n- {ex.Message}\n[Stack Trace]\n{ex.StackTrace}\n- - - - - - - - - - - - - - -\n\n");
            ThrowException(new(this, "Unhandled Exception", ex.Message, ex.StackTrace ?? "", true));
            Environment.Exit(0);
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception ?? new();
            File.WriteAllText("error.log.txt", $"[{DateTime.Now}]\n- {ex.Message}\n[Stack Trace]\n{ex.StackTrace}\n- - - - - - - - - - - - - - -\n\n");
            ThrowException(new(this, "Unhandled Exception", ex.Message, ex.StackTrace ?? "", true));
            Environment.Exit(0);
        }

        #endregion
    }
}
