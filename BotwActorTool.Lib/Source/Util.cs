using Nintendo.Byml;
using Nintendo.Sarc;
using System.Text;
using Yaz0Library;

namespace BotwActorTool.Lib
{
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
                    else {
                        return null;
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

        public static byte[]? GetFileAnywhere(string modRoot, string relPath)
        {
            byte[]? bytes = GetFile($"{modRoot}/{relPath}");

            if (bytes == null || bytes.Length == 0) {
                bytes = GetFile(FindFileOrig(relPath));
            }

            return bytes;
        }

        public static void InjectFilesIntoBootup(string modRoot, List<(string, byte[])> files)
        {
            files.ForEach(t => InjectFile(modRoot, $"Pack/Bootup.pack//{t.Item1}", t.Item2));
        }

        public static void InjectFile(string modRoot, string relPath, byte[] data)
        {
            string[] parts = relPath.Split("//");
            byte[] bytes = GetFileAnywhere(modRoot, relPath) ?? Array.Empty<byte>();
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

        public static string FindFileOrig(string RelPath)
        {
            string[] parts = RelPath.Split(new[] { "//" }, StringSplitOptions.None);
            if (File.Exists($"{Config.UpdateDir}/{parts[0]}")) {
                return $"{Config.UpdateDir}/{RelPath}";
            }
            else if (File.Exists($"{Config.DlcDir}/{parts[0]}")) {
                return $"{Config.DlcDir}/{RelPath}";
            }
            else if (File.Exists($"{Config.GameDir}/{parts[0]}")) {
                return $"{Config.GameDir}/{RelPath}";
            }
            else {
                throw new FileNotFoundException($"{RelPath} doesn't seem to exist.");
            }
        }

        public static List<string> GetResidentActors(string ModRoot)
        {
            List<string> ResidentActors = new();
            string ResidentActorPath;
            if (File.Exists($"{ModRoot}/Pack/Bootup.pack")) {
                ResidentActorPath = $"{ModRoot}/Pack/Bootup.pack//Actor/ResidentActors.byml";
            }
            else {
                ResidentActorPath = $"{FindFileOrig("Pack/Bootup.pack")}//Actor/ResidentActors.byml";
            }
            var ResidentActorRoot = BymlFile.FromBinary(GetFile(ResidentActorPath)).RootNode;
            if (ResidentActorRoot == null)
            {
                throw new Exception("Could not find ResidentActors.byml");
            }
            foreach (var actor in ResidentActorRoot) {
                ResidentActors.Add(actor["name"]);
            }
            return ResidentActors;
        }

        public static string GetActorRelPath(string Name, string ModRoot = "")
        {
            List<string> parts = new();
            if (GetResidentActors(ModRoot).Contains(Name)) {
                parts.Add($"Pack/TitleBG.pack");
            }
            parts.Add($"Actor/Pack/{Name}.sbactorpack");
            return string.Join("//", parts);
        }

        public static string GetModRoot(string AbsPath)
        {
            IEnumerable<string> reversed_path = AbsPath.Split('/').Reverse();
            return string.Join("/", reversed_path.Skip(reversed_path.TakeWhile(s => s != "content" && s != "romfs").Count()).Reverse());
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
