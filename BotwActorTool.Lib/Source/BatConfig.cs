#pragma warning disable CA1822 // Mark members as static

global using static BotwActorTool.Lib.BatConfig;

using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using static System.Environment;

namespace BotwActorTool.Lib
{
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
        public string Lang { get; set; } = "USen";

        #endregion

        public static void LoadConfig()
        {
            if (File.Exists($"{Config.DataFolder}\\Config.json"))
                Config = JsonSerializer.Deserialize<BatConfig>(File.ReadAllText($"{Config.DataFolder}\\Config.json")) ?? new();
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
            if (File.Exists(path))
                return false;

            return mode switch {
                "BaseGame" => File.Exists($"{path}\\Pack\\Dungeon000.pack") && path.EndsWith("content"),
                "Update" => File.Exists($"{path}\\Actor\\Pack\\ActorObserverByActorTagTag.sbactorpack") && path.EndsWith("content"),
                "Dlc" => File.Exists($"{path}\\Pack\\AocMainField.pack") && path.EndsWith("content\\0010"),
                "BaseGameNx" => File.Exists($"{path}\\Actor\\Pack\\ActorObserverByActorTagTag.sbactorpack") && File.Exists($"{path}\\Pack\\Dungeon000.pack")  && path.EndsWith("romfs"),
                "DlcNx" => File.Exists($"{path}\\Pack\\AocMainField.pack") && path.EndsWith("romfs"),
                _ => false,
            };
        }
    }
}
