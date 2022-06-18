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
        public static string Folder {
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
        public string Theme { get; set; } = "System";
        public string Lang { get; set; } = "USen";

        #endregion

        public static void LoadConfig()
        {
            if (File.Exists($"{Folder}\\Config.json"))
                Config = JsonSerializer.Deserialize<BatConfig>(File.ReadAllText($"{Folder}\\Config.json")) ?? new();
            else {
                Config = new BatConfig().Save();
            }
        }

        public BatConfig Save()
        {
            Directory.CreateDirectory(Folder);
            File.WriteAllText($"{Folder}\\Config.json", JsonSerializer.Serialize(this));
            return this;
        }

        public bool ValidateGameDir(string path)
        {
            if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory)) {
                return false;
            }
            if (!File.Exists($"{path}/Pack/Dungeon000.pack")) {
                return false;
            }

            return true;
        }

        public bool ValidateUpdateDir(string path)
        {
            if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory)) {
                return false;
            }
            if (!File.Exists($"{path}/Actor/Pack/ActorObserverByActorTagTag.sbactorpack")) {
                return false;
            }

            return true;
        }

        public bool ValidateDlcDir(string path)
        {
            if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory)) {
                return false;
            }
            if (!File.Exists($"{path}/Pack/AocMainField.pack")) {
                return false;
            }

            return true;
        }
    }
}
