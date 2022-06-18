using BotwActorTool.GUI.ViewResources.Helpers;
using BotwActorTool.GUI.ViewThemes.App;
using BotwActorTool.Lib;
using Stylet;
using System.IO;

namespace BotwActorTool.GUI.ViewModels
{
    public class SettingsViewModel : Screen
    {
        private ShellViewModel ShellViewModel { get; set; }

        private string _baseGame = "";
        public string BaseGame {
            get => _baseGame;
            set => SetAndNotify(ref _baseGame, value);
        }

        private string _update = "";
        public string Update {
            get => _update;
            set => SetAndNotify(ref _update, value);
        }

        private string _dlc = "";
        public string Dlc {
            get => _dlc;
            set => SetAndNotify(ref _dlc, value);
        }

        private string _lang = "";
        public string Lang {
            get => _lang;
            set => SetAndNotify(ref _lang, value);
        }

        private BindableCollection<string> _themes = new();
        public BindableCollection<string> Themes {
            get => _themes;
            set => SetAndNotify(ref _themes, value);
        }

        private string _theme = SysTheme.Name;
        public string Theme {
            get => _theme;
            set {
                SetAndNotify(ref _theme, value);

                if (value == "New. . .") {
                    EditTheme();
                }
                else {
                    SysTheme.Load(value);
                }
            }
        }

        public void Save()
        {
            Util.BATSettings settings = new();

            settings.SetSetting("base_dir", BaseGame);
            settings.SetSetting("update_dir", BaseGame);
            settings.SetSetting("dlc_dir", BaseGame);
            settings.SetSetting("theme", Theme);
            settings.SetSetting("lang", Lang);

            settings.SaveSettings();

            ShellViewModel.SettingsViewModel = null;
        }

        public void Browse(string mode)
        {
            System.Windows.Forms.FolderBrowserDialog browse = new();

            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (mode == "base") {
                    BaseGame = browse.SelectedPath;
                }
                else if (mode == "update") {
                    Update = browse.SelectedPath;
                }
                else if (mode == "update") {
                    Dlc = browse.SelectedPath;
                }
            }
        }

        public void EditTheme()
        {
            ThemeViewModel = new(ShellViewModel.WindowManager, this);
            ThemeViewModel.ThemeName = Theme == "New. . ." ? "New Theme" : Theme;
        }

        public void DeleteTheme()
        {
            if (Theme == "System") {
                ShellViewModel.WindowManager.Show($"Yu can't delete the system theme.", "Error");
                return;
            }

            if (File.Exists($"{SysTheme.Folder}\\{Theme}.json")) {
                if (ShellViewModel.WindowManager.Show($"Are you sure you want to delete '{Theme}'?", "Warning", true)) {

                    // Delete theme resource
                    File.Delete($"{SysTheme.Folder}\\{Theme}.json");

                    // Update current theme list and current selection
                    Themes = SysTheme.GetThemes(true);

                    try {
                        Theme = SysTheme.Load(SysTheme.Last);
                    }
                    catch {
                        Theme = SysTheme.Load(SysTheme.Default);
                    }
                }
            }
        }

        private ThemeViewModel? _themeViewModel = null;
        public ThemeViewModel? ThemeViewModel {
            get => _themeViewModel;
            set => SetAndNotify(ref _themeViewModel, value);
        }

        public SettingsViewModel(ShellViewModel shell)
        {
            ShellViewModel = shell;
            Themes = SysTheme.GetThemes(true);
        }
    }
}
