﻿using System.Text.Json;

namespace BotwActorTool.Lib
{
    public class Resource
    {
        private static Dictionary<string, Dictionary<string, string>>? GenericLinkFiles { get; set; }
        private static Dictionary<string, Dictionary<string, float>>? InstSizeData { get; set; }
        private static Dictionary<string, Dictionary<string, List<string>>>? KeysByProfile { get; set; }
        private static Dictionary<string, string>? RegionList { get; set; }
        private static Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>>? Overrides { get; set; }
        private static Dictionary<string, Dictionary<string, string>>? VanillaParams { get; set; }

        internal static Dictionary<string, Dictionary<string, string>> GetGenericLinkFiles()
        {
            if (GenericLinkFiles == null)
            {
                using Stream stream = File.OpenRead("Data/GenericLinkFiles.json");
                GenericLinkFiles = (Dictionary<string, Dictionary<string, string>>)JsonSerializer.Deserialize(stream, typeof(Dictionary<string, Dictionary<string, string>>))!;
            }
            return GenericLinkFiles;
        }
        internal static Dictionary<string, Dictionary<string, float>> GetInstSizeData()
        {
            if (InstSizeData == null)
            {
                using Stream stream = File.OpenRead("Data/InstSizeData.json");
                InstSizeData = (Dictionary<string, Dictionary<string, float>>)JsonSerializer.Deserialize(stream, typeof(Dictionary<string, Dictionary<string, float>>))!;
            }
            return InstSizeData;
        }
        internal static Dictionary<string, Dictionary<string, List<string>>> GetKeysByProfile()
        {
            if (KeysByProfile == null)
            {
                using Stream stream = File.OpenRead("Data/KeysByProfile.json");
                KeysByProfile = (Dictionary<string, Dictionary<string, List<string>>>)JsonSerializer.Deserialize(stream, typeof(Dictionary<string, Dictionary<string, List<string>>>))!;
            }
            return KeysByProfile;
        }
        internal static Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> GetOverrides()
        {
            if (Overrides == null)
            {
                using Stream stream = File.OpenRead("Data/Overrides.json");
                Overrides = (Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>>)JsonSerializer.Deserialize(stream, typeof(Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>>))!;
            }
            return Overrides;
        }
        internal static Dictionary<string, Dictionary<string, string>> GetVanillaParams()
        {
            if (VanillaParams == null)
            {
                using Stream stream = File.OpenRead("Data/VanillaParams.json");
                VanillaParams = (Dictionary<string, Dictionary<string, string>>)JsonSerializer.Deserialize(stream, typeof(Dictionary<string, Dictionary<string, string>>))!;
            }
            return VanillaParams;
        }
        public static Dictionary<string, string> GetRegionList()
        {
            if (RegionList == null) {
                using Stream stream = File.OpenRead("Data/RegionList.json");
                RegionList = JsonSerializer.Deserialize<Dictionary<string, string>>(stream)!;
            }
            return RegionList;
        }

        public static T? GetDynamic<T>(string name)
        {
            if (File.Exists($"Data/{name}.json")) {
                using Stream stream = File.OpenRead($"Data/{name}.json");
                return JsonSerializer.Deserialize<T>(stream)!;
            }

            return default;
        }
    }
}
