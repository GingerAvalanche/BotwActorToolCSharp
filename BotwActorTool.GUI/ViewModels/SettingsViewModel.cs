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

        private Brush ValidatePath(string path, string mode)
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

        private static readonly Brush _default = "#00000000".ToBrush();
        private static readonly Brush _valid = "#00CC1C".ToBrush();
        private static readonly Brush _invalid = "#FF0000".ToBrush();

        public SettingsView View { get; set; }
        public SettingsViewModel(SettingsView view, bool canClose = true)
        {
            View = view;

            BaseGame = Config.GameDir;
            Update = Config.UpdateDir;
            Dlc = Config.DlcDir;
            BaseGameNx = Config.GameDirNx;
            DlcNx = Config.DlcDirNx;
            Region = GetRegionPair(Config.Lang);
            CanClose = canClose;

            View.FindControl<ToggleSwitch>("ThemeToggle").IsChecked = Config.IsDarkTheme;
        }

        //
        // Properties

        private string baseGame = "";
        public string BaseGame {
            get => baseGame;
            set {
                this.RaiseAndSetIfChanged(ref baseGame, value);
                BaseGameBrush = ValidatePath(value, "BaseGame");
            }
        }

        private string update = "";
        public string Update {
            get => update;
            set {
                this.RaiseAndSetIfChanged(ref update, value);
                UpdateBrush = ValidatePath(value, "Update");
            }
        }

        private string dlc = "";
        public string Dlc {
            get => dlc;
            set {
                this.RaiseAndSetIfChanged(ref dlc, value);
                DlcBrush = ValidatePath(value, "Dlc");
            }
        }

        private string baseGameNx = "";
        public string BaseGameNx {
            get => baseGameNx;
            set {
                this.RaiseAndSetIfChanged(ref baseGameNx, value);
                BaseGameNxBrush = ValidatePath(value, "BaseGameNx");
            }
        }

        private string dlcNx = "";
        public string DlcNx {
            get => dlcNx;
            set {
                this.RaiseAndSetIfChanged(ref dlcNx, value);
                DlcNxBrush = ValidatePath(value, "DlcNx");
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
                if (Config.GameDir != BaseGame || Config.UpdateDir != Update || Config.DlcDir != Dlc ||
                    Config.GameDirNx != BaseGameNx || Config.DlcDirNx != DlcNx) {
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

            Config.GameDir = BaseGame;
            Config.UpdateDir = Update;
            Config.DlcDir = Dlc;
            Config.GameDirNx = BaseGameNx;
            Config.DlcDirNx = DlcNx;
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
                    BaseGame = result;
                }
                else if (mode == "Update") {
                    Update = result;
                }
                else if (mode == "DLC") {
                    Dlc = result;
                }
                else if (mode == "Base Game (Switch)") {
                    BaseGameNx = result;
                }
                else if (mode == "DLC (Switch)") {
                    DlcNx = result;
                }
            }
        }
    }
}
