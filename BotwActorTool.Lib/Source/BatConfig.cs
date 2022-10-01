#pragma warning disable CA1822 // Mark members as static

global using static BotwActorTool.Lib.BatConfig;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using static System.Environment;

namespace BotwActorTool.Lib
{
    public enum BotwDir
    {
        Base = 0,
        Update = 1,
        Dlc = 2,
        Root = 3,
    }

    public class BatConfig
    {
        //
        // Static defenitions
        #region Expand

        public static BatConfig Config { get; set; } = new();

        #endregion

        //
        // Properties
        #region Expand

        [JsonIgnore]
        public string UpdateDirNx { get => GameDirNx; }

        [JsonIgnore]
        public string DataFolder {
            get {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    return $"{GetFolderPath(SpecialFolder.LocalApplicationData, SpecialFolderOption.Create)}/BotwActorTool";
                }
                else {
                    return $"{GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.Create)}/BotwActorTool";
                }
            }
        }

        public string GameDir { get; set; } = "";
        public string UpdateDir { get; set; } = "";
        public string DlcDir { get; set; } = "";
        public string GameDirNx { get; set; } = "";
        public string DlcDirNx { get; set; } = "";
        public bool IsDarkTheme { get; set; } = true;
        public string Lang { get; set; } = "NULL";

        #endregion

        public static void LoadConfig()
        {
            if (File.Exists($"{Config.DataFolder}\\Config.json")) {
                Config = JsonSerializer.Deserialize<BatConfig>(File.ReadAllText($"{Config.DataFolder}\\Config.json")) ?? new();
            }
            else if (File.Exists($"{Config.DataFolder}\\..\\bcml\\settings.json")) {

                Config = new();

                Dictionary<string, object> settings =
                    JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText($"{Config.DataFolder}\\..\\bcml\\settings.json")) ?? new();

                Config.Lang = settings["lang"].ToString() ?? "";
                Config.GameDir = settings["game_dir"].ToString() ?? "";
                Config.GameDirNx = settings["game_dir_nx"].ToString() ?? "";
                Config.UpdateDir = settings["update_dir"].ToString() ?? "";
                Config.DlcDir = settings["dlc_dir"].ToString() ?? "";
                Config.DlcDirNx = settings["dlc_dir_nx"].ToString() ?? "";

                Config.Save();
            }
            else {
                Config = new BatConfig().Save();
            }
        }

        public BatConfig Save()
        {
            Directory.CreateDirectory(Config.DataFolder);
            File.WriteAllText($"{Config.DataFolder}\\Config.json", JsonSerializer.Serialize(this));
            return this;
        }

        public bool ValidateDir(string path, string mode)
        {
            if (path == "NULL") {
                return false;
            }

            if (File.Exists(path)) {
                return false;
            }

            return mode switch {
                "GameDir" => File.Exists($"{path}\\Pack\\Dungeon000.pack") && path.EndsWith("content"),
                "UpdateDir" => File.Exists($"{path}\\Actor\\Pack\\ActorObserverByActorTagTag.sbactorpack") && path.EndsWith("content"),
                "DlcDir" => File.Exists($"{path}\\Pack\\AocMainField.pack") && path.EndsWith("content\\0010"),
                "GameDirNx" => File.Exists($"{path}\\Actor\\Pack\\ActorObserverByActorTagTag.sbactorpack") && File.Exists($"{path}\\Pack\\Dungeon000.pack")  && path.EndsWith("romfs"),
                "DlcDirNx" => File.Exists($"{path}\\Pack\\AocMainField.pack") && path.EndsWith("romfs"),
                _ => false,
            };
        }

        public bool Validate()
        {
            foreach (var prop in GetType().GetProperties(BindingFlags.Instance)) {
                if (!ValidateDir((string)prop.GetValue(this)!, prop.Name)) {
                    return false;
                }
            }

            return true ;
        }

        public string GetDir(BotwDir dir) => dir switch {
            BotwDir.Base => ValidateDir(GameDirNx, nameof(GameDirNx)) ? GameDirNx : GameDir,
            BotwDir.Update => ValidateDir(GameDirNx, nameof(GameDirNx)) ? GameDirNx : UpdateDir,
            BotwDir.Dlc => ValidateDir(DlcDirNx, nameof(DlcDirNx)) ? DlcDirNx : DlcDir,
            BotwDir.Root => DataFolder,
            _ => throw new InvalidDataException($"No handled BotwDir type was passed.")
        };
    }
}
