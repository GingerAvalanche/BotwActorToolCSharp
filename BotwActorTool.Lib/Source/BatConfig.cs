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
        public string Theme { get; set; } = "System";
        public string Lang { get; set; } = "USen";

        #endregion

        public static void LoadConfig()
        {
            if (File.Exists($"{Config.DataFolder}\\Config.json"))
                Config = JsonSerializer.Deserialize<BatConfig>(File.ReadAllText($"{Config.DataFolder}\\Config.json")) ?? new();
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
                "base" => File.Exists($"{path}\\Pack\\Dungeon000.pack") && path.EndsWith("content"),
                "update" => File.Exists($"{path}\\Actor\\Pack\\ActorObserverByActorTagTag.sbactorpack") && path.EndsWith("content"),
                "dlc" => File.Exists($"{path}\\Pack\\AocMainField.pack") && path.EndsWith("content\\0010"),
                "baseNx" => File.Exists($"{path}\\Actor\\Pack\\ActorObserverByActorTagTag.sbactorpack") && File.Exists($"{path}\\Pack\\Dungeon000.pack")  && path.EndsWith("romfs"),
                "dlcNx" => File.Exists($"{path}\\Pack\\AocMainField.pack") && path.EndsWith("romfs"),
                _ => false,
            };
        }
    }
}
