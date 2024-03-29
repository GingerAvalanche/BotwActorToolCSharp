﻿#pragma warning disable CA1822 // Mark members as static

global using static BotwActorTool.Lib.BatConfig;
using Avalonia.SettingsFactory.Core;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using static System.Environment;

namespace BotwActorTool.Lib
{
    public enum BotwDir { Game = 0, Update = 1, Dlc = 2 }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Console { WiiU = 0, Switch = 1 }

    public class BatConfig : ISettingsBase
    {
        //
        // Static defenitions
        #region Expand

        public static BatConfig Config { get; set; } = new();

        #endregion

        //
        // Properties
        #region Expand

        public bool RequiresInput { get; set; } = true;

        [JsonIgnore]
        public string UpdateDirNx { get => GameDirNx; }

        [JsonIgnore]
        public Dictionary<string, string> Regions { get; } = Resource.GetRegionList();

        [JsonIgnore]
        public string DataFolder
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? $"{GetFolderPath(SpecialFolder.LocalApplicationData)}/{nameof(BotwActorTool)}" : $"{GetFolderPath(SpecialFolder.ApplicationData)}/{nameof(BotwActorTool)}";

        [Setting("Base Game Directory", "The folder containing the base game files for BOTW, without the update or DLC files. The last folder should be \"content\", e.g. \"C:/Games/Botw/BaseGame/content\"")]
        public string GameDir { get; set; } = "";

        [Setting("Update Directory", "The folder containing the update files for BOTW, version 1.5.0. The last folder should be \"content\", e.g. \"C:/Games/Botw/Update/content\"")]
        public string UpdateDir { get; set; } = "";

        [Setting("DLC Directory", "The folder containing the DLC files for BOTW, version 3.0. The last folder should be \"0010\", e.g. \"C:/Games/Botw/DLC/content/0010\"")]
        public string DlcDir { get; set; } = "";

        [Setting("Switch Base Game Directory", "Path should end in '01007EF00011E000/romfs'")]
        public string GameDirNx { get; set; } = "";

        [Setting("Switch DLC Directory", "Path should end in '01007EF00011F001/romfs'")]
        public string DlcDirNx { get; set; } = "";

        [Setting(UiType.Dropdown, "Resource:RegionList", Name = "Game Region/Language")]
        public string Lang { get; set; } = "NULL";

        [Setting(UiType.Dropdown, Console.Switch, Console.WiiU)]
        public Console Mode { get; set; } = Console.WiiU;

        [Setting(UiType.Dropdown, "Dark", "Light", Category = "Appearance")]
        public string Theme { get; set; } = "Dark";

        #endregion

        public static void LoadConfig()
        {
            if (File.Exists($"{Config.DataFolder}/Config.json")) {
                Config = JsonSerializer.Deserialize<BatConfig>(File.ReadAllText($"{Config.DataFolder}/Config.json")) ?? new();
            }
            else if (File.Exists($"{Config.DataFolder}/../bcml/settings.json")) {

                Config = new();

                Dictionary<string, object> settings =
                    JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText($"{Config.DataFolder}/../bcml/settings.json")) ?? new();

                Config.Lang = settings["lang"].ToString() ?? "";
                Config.GameDir = settings["game_dir"].ToString() ?? "";
                Config.GameDirNx = settings["game_dir_nx"].ToString() ?? "";
                Config.UpdateDir = settings["update_dir"].ToString() ?? "";
                Config.DlcDir = settings["dlc_dir"].ToString() ?? "";
                Config.DlcDirNx = settings["dlc_dir_nx"].ToString() ?? "";

                Config.Save();
            }
            else {
                Config = (BatConfig)new BatConfig().Save();
            }
        }

        public ISettingsBase Save()
        {
            Directory.CreateDirectory(Config.DataFolder);
            File.WriteAllText($"{Config.DataFolder}/Config.json", JsonSerializer.Serialize(this));
            return this;
        }

        public bool ValidateDir(string path, string mode)
        {
            if (path == null) {
                return false;
            }

            if (File.Exists(path)) {
                return false;
            }

            return mode switch {
                "GameDir" => File.Exists($"{path}/Pack/Dungeon000.pack") && path.EndsWith("content"),
                "UpdateDir" => File.Exists($"{path}/Actor/Pack/ActorObserverByActorTagTag.sbactorpack") && path.EndsWith("content"),
                "DlcDir" => File.Exists($"{path}/Pack/AocMainField.pack") && path.ToSystemPath().EndsWith("content/0010"),
                "GameDirNx" => File.Exists($"{path}/Actor/Pack/ActorObserverByActorTagTag.sbactorpack") && File.Exists($"{path}/Pack/Dungeon000.pack")  && path.EndsWith("romfs"),
                "DlcDirNx" => File.Exists($"{path}/Pack/AocMainField.pack") && path.EndsWith("romfs"),
                _ => false,
            };
        }

        public string GetDir(BotwDir dir) => dir switch {
            BotwDir.Game => Mode == Console.Switch ? GameDirNx : GameDir,
            BotwDir.Update => Mode == Console.Switch ? GameDirNx : UpdateDir,
            BotwDir.Dlc => Mode == Console.Switch ? DlcDirNx : DlcDir,
            _ => throw new InvalidDataException($"The BotwDir type '{dir}' is not implemented yet.")
        };
    }
}
