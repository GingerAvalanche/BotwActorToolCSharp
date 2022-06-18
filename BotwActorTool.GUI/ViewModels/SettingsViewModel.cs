using BotwActorTool.GUI.ViewResources.Helpers;
using BotwActorTool.GUI.ViewThemes.App;
using Stylet;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace BotwActorTool.GUI.ViewModels
{
    public class SettingsViewModel : Screen
    {
        private readonly Brush _valid = "#0CED5F".ToBrush();
        private readonly Brush _invalid = "#ED2A64".ToBrush();
        private readonly ShellViewModel _shellViewModel;

        private string _baseGame = "";
        public string BaseGame {
            get => _baseGame;
            set {
                SetAndNotify(ref _baseGame, value);
                if (Config.ValidateGameDir(value)) {
                    BaseGameBrush = _valid;
                }
                else {
                    BaseGameBrush = _invalid;
                }
            }

        }

        private string _update = "";
        public string Update {
            get => _update;
            set {
                SetAndNotify(ref _update, value);
                if (Config.ValidateUpdateDir(value)) {
                    UpdateBrush = _valid;
                }
                else {
                    UpdateBrush = _invalid;
                }
            }
        }

        private string _dlc = "";
        public string Dlc {
            get => _dlc;
            set {
                SetAndNotify(ref _dlc, value);
                if (Config.ValidateDlcDir(value)) {
                    DlcBrush = _valid;
                }
                else {
                    DlcBrush = _invalid;
                }
            }
        }

        private Brush _baseGameBrush = new SolidColorBrush(SysTheme.ITheme.PrimaryMid.Color);
        public Brush BaseGameBrush {
            get => _baseGameBrush;
            set => SetAndNotify(ref _baseGameBrush, value);
        }

        private Brush _updateBrush = new SolidColorBrush(SysTheme.ITheme.PrimaryMid.Color);
        public Brush UpdateBrush {
            get => _updateBrush;
            set => SetAndNotify(ref _updateBrush, value);
        }

        private Brush _dlcBrush = new SolidColorBrush(SysTheme.ITheme.PrimaryMid.Color);
        public Brush DlcBrush {
            get => _dlcBrush;
            set => SetAndNotify(ref _dlcBrush, value);
        }

        private string _lang = "USen";
        public string Lang {
            get => _lang;
            set => SetAndNotify(ref _lang, value);
        }

        private string _mode = "WiiU";
        public string Mode {
            get => _mode;
            set {
                SetAndNotify(ref _mode, value);
                ShowUpdate = _mode == "Switch" ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility _showUpdate = Visibility.Visible;
        public Visibility ShowUpdate {
            get => _showUpdate;
            set => SetAndNotify(ref _showUpdate, value);
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

        public void Close() => _shellViewModel.SettingsViewModel = null;
        public void Save()
        {
            if (BaseGameBrush == _invalid) {
                _shellViewModel.WindowManager.Show("The game path is invalid or nor set.\nPlease correct it before saving.", "Error");
                return;
            }

            if (UpdateBrush == _invalid && Mode != "Switch") {
                _shellViewModel.WindowManager.Show("The update path is invalid or nor set.\nPlease correct it before saving.", "Error");
                return;
            }

            Config.GameDir = BaseGame;
            Config.UpdateDir = Update;
            Config.DlcDir = Dlc;
            Config.Theme = Theme;
            Config.Lang = Lang;
            Config.Mode = Mode;
            Config.Save();

            Close();
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
                else if (mode == "dlc") {
                    Dlc = browse.SelectedPath;
                }
            }
        }

        public void EditTheme()
        {
            ThemeViewModel = new(_shellViewModel.WindowManager, this);
            ThemeViewModel.ThemeName = Theme == "New. . ." ? "New Theme" : Theme;
        }

        public void DeleteTheme()
        {
            if (Theme == "System") {
                _shellViewModel.WindowManager.Show($"You can't delete the system theme.", "Error");
                return;
            }

            if (File.Exists($"{SysTheme.Folder}\\{Theme}.json")) {
                if (_shellViewModel.WindowManager.Show($"Are you sure you want to delete '{Theme}'?", "Warning", true)) {

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
            _shellViewModel = shell;
            Themes = SysTheme.GetThemes(true);

            BaseGame = Config.GameDir;
            Update = Config.UpdateDir;
            Dlc = Config.DlcDir;
            Lang = Config.Lang;
            Mode = Config.Mode;
        }
    }
}
