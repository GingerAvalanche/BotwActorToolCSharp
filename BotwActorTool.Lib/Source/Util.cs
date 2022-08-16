using Nintendo.Byml;
using Nintendo.Sarc;
using Nintendo.Yaz0;
using System.Text;

namespace BotwActorTool.Lib
{
    public enum Console
    {
        WiiU,
        Switch,
    }
    public class Util
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

        public static byte[]? GetFile(string filePath)
        {
            string[] parts = filePath.Split("//");

            if (File.Exists(parts[0]) && parts.Length > 1) {
                SarcFile sarc = new(File.ReadAllBytes(parts[0]));

                for (int i = 1; i < parts.Length - 1; i++) {

                    if (sarc.Files.ContainsKey(parts[i])) {

                        byte[] sarcBytes = sarc.Files[parts[i]];
                        UnYazIfNeeded(ref sarcBytes);
                        
                        if (Encoding.ASCII.GetString(sarcBytes[0..4]) == "SARC") {
                            sarc = SarcFile.FromBinary(sarcBytes);
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException($"{parts[i]} doesn't exist.");
                    }
                }

                return sarc.Files[parts[^1]];
            }
            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }

            return null;
        }

        public static byte[] GetFileAnywhere(string modRoot, string relPath)
        {
            byte[]? bytes = GetFile($"{modRoot}/{relPath}");

            if (bytes == null || bytes.Length == 0) {
                bytes = GetFile(FindFileOrig(relPath, GetConsole(modRoot)));
            }

            return bytes!;
        }

        public static BymlFile[] GetAccountSaveFormatFiles(string modRoot)
        {
            BymlFile[] files = new BymlFile[2];
            SarcFile saveformat_sarc = new(Yaz0.Decompress(GetFileAnywhere(modRoot, "Pack/Bootup.pack//GameData/savedataformat.ssarc")));
            foreach (byte[] file in saveformat_sarc.Files.Values)
            {
                BymlFile byml = new(file);
                if (byml.RootNode.Hash["file_list"].Array[0].Hash["file_name"].String == "caption.sav")
                {
                    files[0] = byml;
                }
                else if (byml.RootNode.Hash["file_list"].Array[0].Hash["file_name"].String == "option.sav")
                {
                    files[1] = byml;
                }
            }
            return files;
        }

        public static void InjectFilesIntoBootup(string modRoot, List<(string, byte[])> files)
        {
            files.ForEach(t => InjectFile(modRoot, $"Pack/Bootup.pack//{t.Item1}", t.Item2));
        }

        public static void InjectFile(string modRoot, string relPath, byte[] data)
        {
            string[] parts = relPath.Split("//");
            byte[] bytes = GetFileAnywhere(modRoot, relPath);
            bool yazd = UnYazIfNeeded(ref bytes);

            bytes = InjectHelper(new(bytes), string.Join("", parts[1..^0]), data);

            if (yazd) {
                bytes = Yaz0.Compress(bytes, 7);
            }

            File.WriteAllBytes($"{modRoot}/{relPath}", bytes);
        }

        private static byte[] InjectHelper(SarcFile sarc, string relPath, byte[] data)
        {
            string[] parts = relPath.Split("//");
            if (parts.Length > 1)
            {
                byte[] bytes = sarc.Files[parts[0]];
                bool yazd = UnYazIfNeeded(ref bytes);
                SarcFile nested_sarc = new(bytes);
                bytes = InjectHelper(nested_sarc, string.Join("", parts[1..^0]), data);
                sarc.Files[parts[0]] = yazd ? Yaz0.Compress(bytes) : bytes;
                return sarc.ToBinary();
            }
            else
            {
                if (Path.GetExtension(parts[0]).StartsWith(".s") && Path.GetExtension(parts[0]) != ".sarc")
                {
                    data = Yaz0.Compress(data);
                }
                sarc.Files[parts[0]] = data;
                return sarc.ToBinary();
            }
        }

        public static string FindFileOrig(string relPath, Console console)
        {
            string[] parts = relPath.Split(new[] { "//" }, StringSplitOptions.None);
            switch (console)
            {
                case Console.WiiU:
                    if (File.Exists($"{Config.UpdateDir}/{parts[0]}"))
                    {
                        return $"{Config.UpdateDir}/{relPath}";
                    }
                    else if (File.Exists($"{Config.DlcDir}/{parts[0]}"))
                    {
                        return $"{Config.DlcDir}/{relPath}";
                    }
                    else if (File.Exists($"{Config.GameDir}/{parts[0]}"))
                    {
                        return $"{Config.GameDir}/{relPath}";
                    }
                    break;
                case Console.Switch:
                    if (File.Exists($"{Config.DlcDirNx}/{parts[0]}"))
                    {
                        return $"{Config.DlcDirNx}/{relPath}";
                    }
                    else if (File.Exists($"{Config.GameDirNx}/{parts[0]}"))
                    {
                        return $"{Config.GameDirNx}/{relPath}";
                    }
                    break;
            }
            throw new FileNotFoundException($"{relPath} wasn't found in the {console} files.");
        }

        public static List<string> GetResidentActors(string modRoot)
        {
            List<string> ResidentActors = new();
            string ResidentActorPath;
            if (File.Exists($"{modRoot}/Pack/Bootup.pack")) {
                ResidentActorPath = $"{modRoot}/Pack/Bootup.pack//Actor/ResidentActors.byml";
            }
            else {
                ResidentActorPath = $"{FindFileOrig("Pack/Bootup.pack", GetConsole(modRoot))}//Actor/ResidentActors.byml";
            }
            BymlNode? ResidentActorRoot = BymlFile.FromBinary(GetFile(ResidentActorPath)).RootNode;
            if (ResidentActorRoot == null)
            {
                throw new Exception("Could not find ResidentActors.byml");
            }
            foreach (BymlNode actor in ResidentActorRoot.Array) {
                ResidentActors.Add(actor.Hash["name"].String);
            }
            return ResidentActors;
        }

        public static string GetActorRelPath(string name, string modRoot = "")
        {
            List<string> parts = new();
            if (GetResidentActors(modRoot).Contains(name)) {
                parts.Add($"Pack/TitleBG.pack");
            }
            parts.Add($"Actor/Pack/{name}.sbactorpack");
            return string.Join("//", parts);
        }

        public static string GetModRoot(string absPath)
        {
            absPath = absPath.ToSystemPath(forceUnix: true);
            IEnumerable<string> reversed_path = absPath.Split('/').Reverse();
            return string.Join("/", reversed_path.Skip(reversed_path.TakeWhile(s => s != "content" && s != "romfs").Count()).Reverse());
        }

        public static Console GetConsole(string modRoot)
        {
            return modRoot.Contains("romfs") ? Console.Switch : Console.WiiU;
        }

        public static bool UnYazIfNeeded(ref Stream stream)
        {
            using var reader = new BinaryReader(stream);

            if (Encoding.ASCII.GetString(reader.ReadBytes(4)) == "Yaz0") {
                stream = new MemoryStream(Yaz0.Decompress(stream.ToArray()));
                return true;
            }
            else {
                return false;
            }
        }

        public static bool UnYazIfNeeded(ref byte[] bytes)
        {
            if (Encoding.ASCII.GetString(bytes[0..4]) == "Yaz0") {
                bytes = Yaz0.Decompress(bytes);
                return true;
            }

            return false;
        }
    }
}
