using FirstPlugin;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Environment;

namespace BotwActorToolLib
{
    class Util
    {
        public static readonly List<string> LINKS = new()
        {
            "ActorNameJpn",
            "AIProgramUser",
            "AIScheduleUser",
            "ASUser",
            "AttentionUser",
            "AwarenessUser",
            "BoneControlUser",
            "ActorCaptureUser",
            "ChemicalUser",
            "DamageParamUser",
            "DropTableUser",
            "ElinkUser",
            "GParamUser",
            "LifeConditionUser",
            "LODUser",
            "ModelUser",
            "PhysicsUser",
            "ProfileUser",
            "RgBlendWeightUser",
            "RgConfigListUser",
            "RecipeUser",
            "ShopDataUser",
            "SlinkUser",
            "UMiiUser",
            "XlinkUser",
            "AnimationInfo"
        };
        public static readonly Dictionary<string, (string, string)> AAMP_LINK_REFS = new()
        {
            // "ActorNameJpn",
            { "AIProgramUser", ("AIProgram", ".baiprog") },
            { "ASUser", ("ASList", ".baslist") },
            { "AttentionUser", ("AttClientList", ".batcllist") },
            { "AwarenessUser", ("Awareness", ".bawareness") },
            { "BoneControlUser", ("BoneControl", ".bbonectrl") },
            // "ActorCaptureUser",
            { "ChemicalUser", ("Chemical", ".bchemical") },
            { "DamageParamUser", ("DamageParam", ".bdmgparam") },
            { "DropTableUser", ("DropTable", ".bdrop") },
            // "ElinkUser",
            { "GParamUser", ("GeneralParamList", ".bgparamlist") },
            { "LifeConditionUser", ("LifeCondition", ".blifecondition") },
            { "LODUser", ("LOD", ".blod") },
            { "ModelUser", ("ModelList", ".bmodellist") },
            { "PhysicsUser", ("Physics", ".bphysics") },
            // "ProfileUser",
            { "RgBlendWeightUser", ("RagdollBlendWeight", ".brgbw") },
            { "RgConfigListUser", ("RagdollConfigList", ".brgconfiglist") },
            { "RecipeUser", ("Recipe", ".brecipe") },
            { "ShopDataUser", ("ShopData", ".bshop") },
            // "SlinkUser",
            { "UMiiUser", ("UMii", ".bumii") }
            // "XlinkUser",
        };
        public static readonly Dictionary<string, (string, string)> BYML_LINK_REFS = new()
        {
            { "AIScheduleUser", ("AISchedule", ".baischedule") },
            { "AnimationInfo", ("AnimationInfo", ".baniminfo") }
        };
        public static readonly List<string> LANGUAGES = new()
        {
            "USen",
            "EUen",
            "USfr",
            "USes",
            "EUde",
            "EUes",
            "EUfr",
            "EUit",
            "EUnl",
            "EUru",
            "CNzh",
            "JPja",
            "KRko",
            "TWzh"
        };
        public static Stream GetFile(string FilePath)
        {
            string[] parts = FilePath.Split(new[] { "//" }, System.StringSplitOptions.None);
            Stream stream;
            if (File.Exists(parts[0]))
            {
                stream = File.OpenRead(parts[0]);
            }
            else
            {
                return Stream.Null;
            }
            if (parts.Length > 1)
            {
                SARC sarc = new();
                sarc.Load(stream);
                for (int i = 1; i < parts.Length-1; i++)
                {
                    if (sarc.FileLookup.ContainsKey(parts[i]))
                    {
                        stream = sarc.FileLookup[parts[i]].FileDataStream;
                        _ = UnYazIfNeeded(ref stream);
                        if (sarc.Identify(stream))
                        {
                            sarc.Load(stream);
                        }
                    }
                    else
                    {
                        return Stream.Null;
                    }
                }
                stream = sarc.FileLookup[parts[parts.Length-1]].FileDataStream;
                sarc.Unload();
            }
            return stream;
        }
        public static Stream GetFileAnywhere(string ModRoot, string RelPath)
        {
            Stream MaybeStream = GetFile($"{ModRoot}/{RelPath}");
            return MaybeStream.Length > 0 ? MaybeStream : GetFile(FindFileOrig(RelPath));
        }
        public static bool InjectFilesIntoBootup(string ModRoot, List<(string, byte[])> files)
        {
            files.ForEach(t => InjectFile(ModRoot, $"Pack/Bootup.pack//{t.Item1}", t.Item2));
            return true;
        }
        public static bool InjectFile(string ModRoot, string RelPath, byte[] data)
        {
            string[] parts = RelPath.Split(new[] { "//" }, 2, System.StringSplitOptions.None);
            Stream stream = GetFileAnywhere(ModRoot, RelPath);
            SARC sarc = new();
            bool yazd = UnYazIfNeeded(ref stream);
            sarc.Load(stream);
            sarc.SetFileData(parts[1], data);
            byte[] NewData = sarc.Write();
            sarc.Unload();
            if (yazd)
            {
                NewData = EveryFileExplorer.YAZ0.Compress(NewData, 7);
            }
            File.WriteAllBytes($"{ModRoot}/{RelPath}", NewData);
            return true;
        }
        public static string FindFileOrig(string RelPath)
        {
            BATSettings settings = new();
            string[] parts = RelPath.Split(new[] { "//" }, System.StringSplitOptions.None);
            if (File.Exists($"{settings.GetSetting("update_dir")}/{parts[0]}"))
            {
                return $"{settings.GetSetting("update_dir")}/{RelPath}";
            }
            else if (File.Exists($"{settings.GetSetting("dlc_dir")}/{parts[0]}"))
            {
                return $"{settings.GetSetting("dlc_dir")}/{RelPath}";
            }
            else if (File.Exists($"{settings.GetSetting("game_dir")}/{parts[0]}"))
            {
                return $"{settings.GetSetting("game_dir")}/{RelPath}";
            }
            else
            {
                throw new FileNotFoundException($"{RelPath} doesn't seem to exist.");
            }
        }
        public static List<string> GetResidentActors(string ModRoot)
        {
            List<string> ResidentActors = new();
            string ResidentActorPath;
            if (File.Exists($"{ModRoot}/Pack/Bootup.pack"))
            {
                ResidentActorPath = $"{ModRoot}/Pack/Bootup.pack//Actor/ResidentActors.byml";
            }
            else
            {
                ResidentActorPath = $"{FindFileOrig("Pack/Bootup.pack")}//Actor/ResidentActors.byml";
            }
            var ResidentActorRoot = ByamlExt.Byaml.ByamlFile.FastLoadN(GetFile(ResidentActorPath)).RootNode;
            foreach (var actor in ResidentActorRoot)
            {
                ResidentActors.Add(actor["name"]);
            }
            return ResidentActors;
        }
        public static string GetActorRelPath(string Name, string ModRoot = "")
        {
            List<string> parts = new();
            if (GetResidentActors(ModRoot).Contains(Name))
            {
                parts.Add($"Pack/TitleBG.pack");
            }
            parts.Add($"Actor/Pack/{Name}.sbactorpack");
            return string.Join("//", parts);
        }
        public static string GetModRoot(string AbsPath)
        {
            IEnumerable<string> reversed_path = AbsPath.Split('/').Reverse();
            return String.Join("/", reversed_path.Skip(reversed_path.TakeWhile(s => s != "content" && s != "romfs").Count()).Reverse());
        }
        public static bool UnYazIfNeeded(ref Stream stream)
        {
            using (var reader = new Toolbox.Library.IO.FileReader(stream))
            {
                if (reader.CheckSignature(4, "YAZ0"))
                {
                    stream = new MemoryStream(Yaz0.Decompress(stream.ToArray()));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public class BATSettings
        {
            private readonly IniData Settings;
            public BATSettings()
            {
                IniParser.Parser.IniDataParser Parser = new();
                if (File.Exists($"{GetDataDir()}/settings.ini"))
                {
                    Settings = Parser.Parse(File.ReadAllText($"{GetDataDir()}/settings.ini"));
                }
                else
                {
                    Settings = new();
                    Settings.Sections = new();
                    Settings.Sections.AddSection("General");
                    Settings["General"]["game_dir"] = "";
                    Settings["General"]["update_dir"] = "";
                    Settings["General"]["dlc_dir"] = "";
                    Settings["General"]["dark_theme"] = "false";
                    Settings["General"]["lang"] = "USen";
                    Settings.Sections.AddSection("Window");
                    Settings["Window"]["WinPosX"] = "0";
                    Settings["Window"]["WinPosY"] = "0";
                    Settings["Window"]["WinHeight"] = "0";
                    Settings["Window"]["WinWidth"] = "0";
                }
            }
            public string GetDataDir()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return $"{GetFolderPath(SpecialFolder.LocalApplicationData, SpecialFolderOption.Create)}/botw_actor_tool";
                }
                else
                {
                    return $"{GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.Create)}/botw_actor_tool";
                }
            }
            public void SetSetting(string name, string value)
            {
                Settings["General"][name] = value;
            }
            public string GetSetting(string name)
            {
                return Settings["General"][name];
            }
            public void SaveSettings()
            {
                IniParser.StreamIniDataParser parser = new();
                parser.WriteData(new StreamWriter($"{GetDataDir()}/settings.ini"), Settings);
            }
            public void SetDarkMode(bool enabled)
            {
                Settings["General"]["dark_theme"] = enabled ? "true" : "false";
            }
            public bool GetDarkMode()
            {
                return Settings["General"]["dark_theme"] == "true";
            }
            public void SetWinPosition((int, int) pos)
            {
                Settings["Window"]["WinPosX"] = pos.Item1.ToString();
                Settings["Window"]["WinPosY"] = pos.Item2.ToString();
            }
            public (int, int) GetWinPosition()
            {
                return (int.Parse(Settings["Window"]["WinPosX"]),int.Parse(Settings["Window"]["WinPosY"]));
            }
            public void SetWinSize((int, int) size)
            {
                Settings["Window"]["WinWidth"] = size.Item1.ToString();
                Settings["Window"]["WinHeight"] = size.Item2.ToString();
            }
            public (int, int) GetWinSize()
            {
                return (int.Parse(Settings["Window"]["WinWidth"]), int.Parse(Settings["Window"]["WinHeight"]));
            }
            public bool ValidateGameDir(string path)
            {
                if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    return false;
                }
                if (!File.Exists($"{path}/Pack/Dungeon000.pack"))
                {
                    return false;
                }
                return true;
            }
            public bool ValidateUpdateDir(string path)
            {
                if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    return false;
                }
                if (!File.Exists($"{path}/Actor/Pack/ActorObserverByActorTagTag.sbactorpack"))
                {
                    return false;
                }
                return true;
            }
            public bool ValidateDlcDir(string path)
            {
                if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    return false;
                }
                if (!File.Exists($"{path}/Pack/AocMainField.pack"))
                {
                    return false;
                }
                return true;
            }
        }
    }
}
