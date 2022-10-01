#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using Avalonia.Controls;
using Avalonia.Dialogs;
using Avalonia.Media;
using Avalonia.Themes.Fluent;
using BotwActorTool.GUI.Extensions;
using BotwActorTool.GUI.Views;
using BotwActorTool.Lib;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.ViewModels
{
    public class SettingsViewModel : ReactiveObject
    {
        //
        // Misc

        public bool CanClose { get; } = true;

        public KeyValuePair<string, string> GetRegionPair(string regionPairKey)
        {
            var regionLang = regionPairKey;

            foreach (var tag in Resource.GetRegionList()) {
                regionLang = regionLang.Replace(tag.Key, tag.Value);
            }

            return new($"{regionLang} ({regionPairKey[0..2]})", regionPairKey);
        }

        private Brush ValidatePath(string path, [CallerMemberName] string mode = "")
        {
            if (string.IsNullOrEmpty(path)) {
                return _default;
            }
            else if (Config.ValidateDir(path, mode)) {

                foreach (var file in Directory.GetFiles($"{path}/Pack", "Bootup_*.pack")) {
                    var region = Path.GetFileNameWithoutExtension(file).Replace("Bootup_", "");
                    KeyValuePair<string, string> pair = GetRegionPair(region);

                    bool addPair = true;

                    for (int i = 0; i < Regions.Count; i++) {
                        if (Regions[i].Value == pair.Value) {
                            Regions[i] = pair;
                            addPair = false;
                        }
                    }

                    if (addPair && region != "Graphics") Regions.Add(pair);
                    Region = Regions.First();
                }

                return _valid;
            }
            else {
                return _invalid;
            }
        }

        internal static readonly Brush _default = "#00000000".ToBrush();
        internal static readonly Brush _valid = "#00CC1C".ToBrush();
        internal static readonly Brush _invalid = "#FF0000".ToBrush();

        public SettingsView View { get; set; }
        public SettingsViewModel(SettingsView view, bool canClose = true)
        {
            View = view;

            GameDir = Config.GameDir;
            UpdateDir = Config.UpdateDir;
            DlcDir = Config.DlcDir;
            GameDirNx = Config.GameDirNx;
            DlcDirNx = Config.DlcDirNx;
            Region = GetRegionPair(Config.Lang);
            CanClose = canClose;

            View.FindControl<ToggleSwitch>("ThemeToggle").IsChecked = Config.IsDarkTheme;
        }

        //
        // Properties

        private string gameDir = "";
        public string GameDir {
            get => gameDir;
            set {
                this.RaiseAndSetIfChanged(ref gameDir, value);
                BaseGameBrush = ValidatePath(value);
            }
        }

        private string updateDir = "";
        public string UpdateDir {
            get => updateDir;
            set {
                this.RaiseAndSetIfChanged(ref updateDir, value);
                UpdateBrush = ValidatePath(value);
            }
        }

        private string dlcDir = "";
        public string DlcDir {
            get => dlcDir;
            set {
                this.RaiseAndSetIfChanged(ref dlcDir, value);
                DlcBrush = ValidatePath(value);
            }
        }

        private string gameDirNx = "";
        public string GameDirNx {
            get => gameDirNx;
            set {
                this.RaiseAndSetIfChanged(ref gameDirNx, value);
                BaseGameNxBrush = ValidatePath(value);
            }
        }

        private string dlcDirNx = "";
        public string DlcDirNx {
            get => dlcDirNx;
            set {
                this.RaiseAndSetIfChanged(ref dlcDirNx, value);
                DlcNxBrush = ValidatePath(value);
            }
        }

        private KeyValuePair<string, string> region = new();
        public KeyValuePair<string, string> Region {
            get => region;
            set => this.RaiseAndSetIfChanged(ref region, value);
        }

        private ObservableCollection<KeyValuePair<string, string>> regions = new();
        public ObservableCollection<KeyValuePair<string, string>> Regions {
            get => regions;
            set => this.RaiseAndSetIfChanged(ref regions, value);
        }

        private Brush baseGameBrush = _default;
        public Brush BaseGameBrush {
            get => baseGameBrush;
            set => this.RaiseAndSetIfChanged(ref baseGameBrush, value);
        }

        private Brush updateBrush = _default;
        public Brush UpdateBrush {
            get => updateBrush;
            set => this.RaiseAndSetIfChanged(ref updateBrush, value);
        }

        private Brush dlcBrush = _default;
        public Brush DlcBrush {
            get => dlcBrush;
            set => this.RaiseAndSetIfChanged(ref dlcBrush, value);
        }

        private Brush baseGameNxBrush = _default;
        public Brush BaseGameNxBrush {
            get => baseGameNxBrush;
            set => this.RaiseAndSetIfChanged(ref baseGameNxBrush, value);
        }

        private Brush dlcNxBrush = _default;
        public Brush DlcNxBrush {
            get => dlcNxBrush;
            set => this.RaiseAndSetIfChanged(ref dlcNxBrush, value);
        }

        //
        // Functions

        public void ChangeTheme()
            => App.Fluent.Mode = App.Fluent.Mode == FluentThemeMode.Dark ? App.Fluent.Mode = FluentThemeMode.Light : App.Fluent.Mode = FluentThemeMode.Dark;

        public async void Close(bool warn = true)
        {
            if (warn) {
                if (Config.GameDir != GameDir || Config.UpdateDir != UpdateDir || Config.DlcDir != DlcDir ||
                    Config.GameDirNx != GameDirNx || Config.DlcDirNx != DlcDirNx) {
                    var result = await View.Window.ShowMessageBox("Are you sure you want to discard chages?", "Warning", MessageBoxButtons.YesNoCancel);
                    if (result == MessageBoxResult.No || result == MessageBoxResult.Cancel) {
                        return;
                    }
                }
            }

            ((AppViewModel)View.Window.DataContext).SettingsView = null;
            ((AppViewModel)View.Window.DataContext).IsSettingsOpen = false;
        }

        public void Save()
        {
            if (BaseGameBrush == _invalid) {
                View.Window.ShowMessageBox("The WiiU game path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (UpdateBrush == _invalid) {
                View.Window.ShowMessageBox("The WiiU update path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (DlcBrush == _invalid) {
                View.Window.ShowMessageBox("The WiiU DLC path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (BaseGameNxBrush == _invalid && BaseGameBrush == _invalid) {
                View.Window.ShowMessageBox("The Switch game/update path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (DlcNxBrush == _invalid) {
                View.Window.ShowMessageBox("The Switch DLC path is invalid.\nPlease correct or delete it before saving.", "Error");
                return;
            }

            if (BaseGameBrush == _default && UpdateBrush == _default && BaseGameNxBrush == _default) {
                View.Window.ShowMessageBox("No game path has been set for Switch or WiiU.\nPlease set one of them before saving.", "Error");
                return;
            }

            if (BaseGameBrush == _valid && UpdateBrush == _default) {
                View.Window.ShowMessageBox("The WiiU update path has not been set.\nPlease set it before saving.", "Error");
                return;
            }

            Config.GameDir = GameDir;
            Config.UpdateDir = UpdateDir;
            Config.DlcDir = DlcDir;
            Config.GameDirNx = GameDirNx;
            Config.DlcDirNx = DlcDirNx;
            Config.IsDarkTheme = App.Fluent.Mode == FluentThemeMode.Dark;
            Config.Lang = Region.Value;

            Config.Save();
            Close(false);
        }

        public async void Browse(string mode)
        {
            var result = await new OpenFolderDialog() {
                Title = $"Select the {mode} Folder"
            }.ShowAsync(View.Window);

            if (!string.IsNullOrEmpty(result)) {
                if (mode == "Base Game") {
                    GameDir = result;
                }
                else if (mode == "Update") {
                    UpdateDir = result;
                }
                else if (mode == "DLC") {
                    DlcDir = result;
                }
                else if (mode == "Base Game (Switch)") {
                    GameDirNx = result;
                }
                else if (mode == "DLC (Switch)") {
                    DlcDirNx = result;
                }
            }
        }
    }
}
