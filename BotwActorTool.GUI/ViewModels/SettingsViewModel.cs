using BotwActorTool.GUI.ViewResources.Helpers;
using BotwActorTool.GUI.ViewThemes.App;
using Stylet;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace BotwActorTool.GUI.ViewModels
{
    public class SettingsViewModel : Screen
    {
        private Brush ValidatePath(string path, string mode)
        {
            if (string.IsNullOrEmpty(path)) {
                return _default;
            }
            else if (Config.ValidateDir(path, mode)) {
                return _valid;
            }
            else {
                return _invalid;
            }
        }

        private static readonly Brush _default = new SolidColorBrush(SysTheme.ITheme.PrimaryMid.Color);
        private static readonly Brush _valid = "#0CED5F".ToBrush();
        private static readonly Brush _invalid = "#ED2A64".ToBrush();
        private readonly ShellViewModel _shellViewModel;

        private string _baseGame = "";
        public string BaseGame {
            get => _baseGame;
            set {
                SetAndNotify(ref _baseGame, value);
                BaseGameBrush = ValidatePath(value, "base");
            }

        }

        private string _update = "";
        public string Update {
            get => _update;
            set {
                SetAndNotify(ref _update, value);
                UpdateBrush = ValidatePath(value, "update");
            }
        }

        private string _dlc = "";
        public string Dlc {
            get => _dlc;
            set {
                SetAndNotify(ref _dlc, value);
                DlcBrush = ValidatePath(value, "dlc");
            }
        }

        private string _baseGameNx = "";
        public string BaseGameNx {
            get => _baseGameNx;
            set {
                SetAndNotify(ref _baseGameNx, value);
                BaseGameNxBrush = ValidatePath(value, "baseNx");
            }

        }

        private string _dlcNx = "";
        public string DlcNx {
            get => _dlcNx;
            set {
                SetAndNotify(ref _dlcNx, value);
                DlcNxBrush = ValidatePath(value, "dlcNx");
            }
        }

        private Brush _baseGameBrush = _default;
        public Brush BaseGameBrush {
            get => _baseGameBrush;
            set => SetAndNotify(ref _baseGameBrush, value);
        }

        private Brush _updateBrush = _default;
        public Brush UpdateBrush {
            get => _updateBrush;
            set => SetAndNotify(ref _updateBrush, value);
        }

        private Brush _dlcBrush = _default;
        public Brush DlcBrush {
            get => _dlcBrush;
            set => SetAndNotify(ref _dlcBrush, value);
        }

        private Brush _baseGameNxBrush = _default;
        public Brush BaseGameNxBrush {
            get => _baseGameNxBrush;
            set => SetAndNotify(ref _baseGameNxBrush, value);
        }

        private Brush _dlcNxBrush = _default;
        public Brush DlcNxBrush {
            get => _dlcNxBrush;
            set => SetAndNotify(ref _dlcNxBrush, value);
        }

        private string _lang = "USen";
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

        public void Close() => _shellViewModel.SettingsViewModel = null;
        public void Save()
        {
            if (BaseGameBrush == _invalid) {
                _shellViewModel.WindowManager.Show("The WiiU game path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (UpdateBrush == _invalid) {
                _shellViewModel.WindowManager.Show("The WiiU update path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (DlcBrush == _invalid) {
                _shellViewModel.WindowManager.Show("The WiiU DLC path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (BaseGameNxBrush == _invalid && BaseGameBrush == _invalid) {
                _shellViewModel.WindowManager.Show("The Switch game/update path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (DlcNxBrush == _invalid) {
                _shellViewModel.WindowManager.Show("The Switch DLC path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (BaseGameBrush == _default && UpdateBrush == _default && BaseGameNxBrush == _default) {
                _shellViewModel.WindowManager.Show("No game path has been set for Switch or WiiU.\nPlease set one of them before saving.", "Error");
                return;
            }

            if (BaseGameBrush == _valid && UpdateBrush == _default ) {
                _shellViewModel.WindowManager.Show("The WiiU update path has not been set.\nPlease set it before saving.", "Error");
                return;
            }

            Config.GameDir = BaseGame;
            Config.UpdateDir = Update;
            Config.DlcDir = Dlc;
            Config.GameDirNx = BaseGameNx;
            Config.DlcDirNx = DlcNx;
            Config.Theme = Theme;
            Config.Lang = Lang;

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
                else if (mode == "baseNx") {
                    BaseGameNx = browse.SelectedPath;
                }
                else if (mode == "dlcNx") {
                    DlcNx = browse.SelectedPath;
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
            BaseGameNx = Config.GameDirNx;
            DlcNx = Config.DlcDirNx;
            Lang = Config.Lang;

            Console.WriteLine("0.0.6");
        }
    }
}
