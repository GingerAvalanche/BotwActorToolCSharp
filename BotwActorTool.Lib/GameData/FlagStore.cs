using BotwActorTool.Lib.Gamedata.Flags;
using Nintendo.Byml;
using System.Collections.ObjectModel;

namespace BotwActorTool.Lib.Gamedata
{
    public class FlagStore
    {
        private static readonly string[] FLAG_KEYS = new string[]
        {
            "DataName",
            "DeleteRev",
            "InitValue",
            "IsEventAssociated",
            "IsOneTrigger",
            "IsProgramReadable",
            "IsProgramWritable",
            "IsSave",
            "MaxValue",
            "MinValue",
            "ResetType"
        };
        private static readonly ReadOnlyCollection<string> IGNORED_SAVE_FLAGS = new(new List<string>
        {
            "AlbumPictureIndex",
            "IsGet_Obj_AmiiboItem",
            "CaptionPictSize",
            "SeakSensorPictureIndex",
            "AoC_HardMode_Enabled",
            "FamouseValue",
            "SaveDistrictName",
            "LastSaveTime_Lower",
            "GameClear",
            "IsChangedByDebug",
            "SaveLocationName",
            "IsSaveByAuto",
            "LastSaveTime_Upper",
            "IsLogicalDelete",
            "GyroOnOff",
            "PlayReport_CtrlMode_Ext",
            "PlayReport_CtrlMode_Free",
            "NexUniqueID_Upper",
            "MiniMapDirection",
            "CameraRLReverse",
            "JumpButtonChange",
            "TextRubyOnOff",
            "VoiceLanguage",
            "PlayReport_CtrlMode_Console_Free",
            "PlayReport_PlayTime_Handheld",
            "BalloonTextOnOff",
            "PlayReport_AudioChannel_Other",
            "PlayReport_AudioChannel_5_1ch",
            "NexIsPosTrackUploadAvailableCache",
            "NexsSaveDataUploadIntervalHoursCache",
            "NexUniqueID_Lower",
            "TrackBlockFileNumber",
            "Option_LatestAoCVerPlayed",
            "NexPosTrackUploadIntervalHoursCache",
            "NexLastUploadTrackBlockHardIndex",
            "MainScreenOnOff",
            "PlayReport_AudioChannel_Stereo",
            "NexIsSaveDataUploadAvailableCache",
            "NexLastUploadSaveDataTime",
            "PlayReport_AllPlayTime",
            "NexLastUploadTrackBlockIndex",
            "PlayReport_CtrlMode_Console_Ext",
            "AmiiboItemOnOff",
            "TrackBlockFileNumber_Hard",
            "StickSensitivity",
            "TextWindowChange",
            "IsLastPlayHardMode",
            "PlayReport_CtrlMode_Console_FullKey",
            "NexLastUploadTrackBlockTime",
            "PlayReport_CtrlMode_FullKey",
            "PlayReport_PlayTime_Console",
            "PlayReport_AudioChannel_Mono",
            "CameraUpDownReverse",
            "PlayReport_CtrlMode_Handheld"
        });

        private readonly Dictionary<string, Dictionary<int, BaseFlag>> CurrFlagStore = new()
        {
            { "bool_data", new Dictionary<int, BaseFlag>() },
            { "bool_array_data", new Dictionary<int, BaseFlag>() },
            { "s32_data", new Dictionary<int, BaseFlag>() },
            { "s32_array_data", new Dictionary<int, BaseFlag>() },
            { "f32_data", new Dictionary<int, BaseFlag>() },
            { "f32_array_data", new Dictionary<int, BaseFlag>() },
            { "string_data", new Dictionary<int, BaseFlag>() },
            { "string64_data", new Dictionary<int, BaseFlag>() },
            { "string64_array_data", new Dictionary<int, BaseFlag>() },
            { "string256_data", new Dictionary<int, BaseFlag>() },
            { "string256_array_data", new Dictionary<int, BaseFlag>() },
            { "vector2f_data", new Dictionary<int, BaseFlag>() },
            { "vector2f_array_data", new Dictionary<int, BaseFlag>() },
            { "vector3f_data", new Dictionary<int, BaseFlag>() },
            { "vector3f_array_data", new Dictionary<int, BaseFlag>() },
            { "vector4f_data", new Dictionary<int, BaseFlag>() }
        };

        private readonly Dictionary<string, Dictionary<int, BaseFlag>> OrigFlagStore = new()
        {
            { "bool_data", new Dictionary<int, BaseFlag>() },
            { "bool_array_data", new Dictionary<int, BaseFlag>() },
            { "s32_data", new Dictionary<int, BaseFlag>() },
            { "s32_array_data", new Dictionary<int, BaseFlag>() },
            { "f32_data", new Dictionary<int, BaseFlag>() },
            { "f32_array_data", new Dictionary<int, BaseFlag>() },
            { "string_data", new Dictionary<int, BaseFlag>() },
            { "string64_data", new Dictionary<int, BaseFlag>() },
            { "string64_array_data", new Dictionary<int, BaseFlag>() },
            { "string256_data", new Dictionary<int, BaseFlag>() },
            { "string256_array_data", new Dictionary<int, BaseFlag>() },
            { "vector2f_data", new Dictionary<int, BaseFlag>() },
            { "vector2f_array_data", new Dictionary<int, BaseFlag>() },
            { "vector3f_data", new Dictionary<int, BaseFlag>() },
            { "vector3f_array_data", new Dictionary<int, BaseFlag>() },
            { "vector4f_data", new Dictionary<int, BaseFlag>() }
        };

        public FlagStore() { }

        public void Clear()
        {
            foreach (string key in CurrFlagStore.Keys)
            {
                CurrFlagStore[key].Clear();
            }
            foreach (string key in OrigFlagStore.Keys)
            {
                OrigFlagStore[key].Clear();
            }
        }

        public void AddFlagsFromByml(string filename, BymlFile byml)
        {
            bool IsRevival = filename.Contains("revival");

            foreach ((string type, BymlNode hash) in byml.RootNode.Hash)
            {
                bool error = false;
                foreach (BymlNode flag in hash.Array)
                {
                    foreach (string key in FLAG_KEYS)
                    {
                        if (!flag.Hash.ContainsKey(key))
                        {
                            System.Console.Error.WriteLine($"Malformed flag: {flag.Hash["HashValue"].Int} missing {key}");
                            error = true;
                        }
                    }
                    if (error)
                    {
                        continue;
                    }

                    BaseFlag f = type switch
                    {
                        "bool_data" => new BoolFlag(flag, IsRevival),
                        "bool_array_data" => new BoolArrayFlag(flag),
                        "s32_data" => new S32Flag(flag, IsRevival),
                        "s32_array_data" => new S32ArrayFlag(flag),
                        "f32_data" => new F32Flag(flag),
                        "f32_array_data" => new F32ArrayFlag(flag),
                        "string_data" => new String32Flag(flag),
                        "string64_data" => new String64Flag(flag),
                        "string64_array_data" => new String64ArrayFlag(flag),
                        "string256_data" => new String256Flag(flag),
                        "string256_array_data" => new String256ArrayFlag(flag),
                        "vector2f_data" => new Vec2Flag(flag),
                        "vector2f_array_data" => new Vec2ArrayFlag(flag),
                        "vector3f_data" => new Vec3Flag(flag),
                        "vector3f_array_data" => new Vec3ArrayFlag(flag),
                        "vector4f_data" => new Vec4Flag(flag),
                        _ => throw new Exception($"Unknown flag type {type}"),
                    };
                    if (f.HashValue == 0)
                    {
                        continue;
                    }
                    CurrFlagStore[type].Add(f.HashValue, f);
                    OrigFlagStore[type].Add(f.HashValue, f);
                }
                if (error)
                {
                    throw new KeyNotFoundException($"Malformed flags found in {type} in {filename}. See error log for details.");
                }
            }
        }

        public void AddFlagsFromBymlNoOverwrite(string filename, BymlFile byml)
        {
            bool IsRevival = filename.Contains("revival");

            foreach ((string type, BymlNode hash) in byml.RootNode.Hash)
            {
                bool error = false;
                foreach (BymlNode flag in hash.Array)
                {
                    foreach (string key in FLAG_KEYS)
                    {
                        if (!flag.Hash.ContainsKey(key))
                        {
                            System.Console.Error.WriteLine($"Malformed flag: {flag.Hash["HashValue"].Int} missing {key}");
                            error = true;
                        }
                    }
                    if (error)
                    {
                        continue;
                    }

                    BaseFlag f = type switch
                    {
                        "bool_data" => new BoolFlag(flag, IsRevival),
                        "bool_array_data" => new BoolArrayFlag(flag),
                        "s32_data" => new S32Flag(flag, IsRevival),
                        "s32_array_data" => new S32ArrayFlag(flag),
                        "f32_data" => new F32Flag(flag),
                        "f32_array_data" => new F32ArrayFlag(flag),
                        "string_data" => new String32Flag(flag),
                        "string64_data" => new String64Flag(flag),
                        "string64_array_data" => new String64ArrayFlag(flag),
                        "string256_data" => new String256Flag(flag),
                        "string256_array_data" => new String256ArrayFlag(flag),
                        "vector2f_data" => new Vec2Flag(flag),
                        "vector2f_array_data" => new Vec2ArrayFlag(flag),
                        "vector3f_data" => new Vec3Flag(flag),
                        "vector3f_array_data" => new Vec3ArrayFlag(flag),
                        "vector4f_data" => new Vec4Flag(flag),
                        _ => throw new Exception($"Unknown flag type {type}"),
                    };
                    if (f.HashValue == 0 || CurrFlagStore[type].ContainsKey(f.HashValue))
                    {
                        continue;
                    }
                    CurrFlagStore[type].Add(f.HashValue, f);
                    OrigFlagStore[type].Add(f.HashValue, f);
                }
                if (error)
                {
                    throw new KeyNotFoundException($"Malformed flags found in {type} in {filename}. See error log for details.");
                }
            }
        }

        public BaseFlag Find(string type, int hash)
        {
            if (CurrFlagStore[type].ContainsKey(hash)) {
                return CurrFlagStore[type][hash];
            }
            return new BoolFlag();
        }

        public List<BaseFlag> FindAll(string type, string name)
        {
            return CurrFlagStore[type]
                .Where(p => p.Value.DataName.Contains(name))
                .Select(p => p.Value)
                .ToList();
        }

        public HashSet<int> FindAllHashes(string type, string name)
        {
            return CurrFlagStore[type]
                .Where(p => p.Value.DataName.Contains(name))
                .Select(p => p.Value.HashValue)
                .ToHashSet();
        }

        public void Add(string type, BaseFlag flag) => CurrFlagStore[type][flag.HashValue] = flag;
        public void Remove(string type, int hash) => CurrFlagStore[type].Remove(hash);

        public int GetNewNum()
        {
            int ret = 0;
            foreach (KeyValuePair<string, Dictionary<int, BaseFlag>> pair in CurrFlagStore) {
                ret += GetNew(pair.Key).Count;
            }
            return ret;
        }

        public int GetModifiedNum()
        {
            int ret = 0;
            foreach (KeyValuePair<string, Dictionary<int, BaseFlag>> pair in CurrFlagStore) {
                ret += GetModified(pair.Key).Count;
            }
            return ret;
        }

        public int GetRemovedNum()
        {
            int ret = 0;
            foreach (KeyValuePair<string, Dictionary<int, BaseFlag>> pair in CurrFlagStore) {
                ret += GetRemoved(pair.Key).Count;
            }
            return ret;
        }

        public HashSet<string> GetNew(string type)
        {
            return CurrFlagStore[type]
                .Where(p => !OrigFlagStore[type].ContainsKey(p.Key))
                .Select(p => p.Value.DataName)
                .ToHashSet();
        }

        public HashSet<string> GetModified(string type)
        {
            return CurrFlagStore[type]
                .Where(p => OrigFlagStore[type].ContainsKey(p.Key) && !p.Value.Equals(OrigFlagStore[type][p.Key]))
                .Select(p => p.Value.DataName)
                .ToHashSet();
        }

        public HashSet<string> GetRemoved(string type)
        {
            return OrigFlagStore[type]
                .Where(p => !CurrFlagStore[type].ContainsKey(p.Key))
                .Select(p => p.Value.DataName)
                .ToHashSet();
        }

        public int GetChangesNum()
        {
            return GetNewNum() + GetModifiedNum() + GetRemovedNum();
        }

        public int GetNewNumSvData()
        {
            int ret = 0;
            foreach (KeyValuePair<string, Dictionary<int, BaseFlag>> pair in CurrFlagStore) {
                ret += GetNewSvData(pair.Key).Count;
            }
            return ret;
        }

        public int GetModifiedNumSvData()
        {
            return 0;
        }

        public int GetRemovedNumSvData()
        {
            int ret = 0;
            foreach (KeyValuePair<string, Dictionary<int, BaseFlag>> pair in CurrFlagStore) {
                ret += GetRemovedSvData(pair.Key).Count;
            }
            return ret;
        }

        public HashSet<string> GetNewSvData(string type)
        {
            return CurrFlagStore[type]
                .Where(p => p.Value.IsSave && !OrigFlagStore[type].ContainsKey(p.Key))
                .Select(p => p.Value.DataName)
                .ToHashSet();
        }

        public HashSet<string> GetModifiedSvData(string type)
        {
            return new();
        }

        public HashSet<string> GetRemovedSvData(string type)
        {
            return OrigFlagStore[type]
                .Where(p => p.Value.IsSave && !CurrFlagStore[type].ContainsKey(p.Key))
                .Select(p => p.Value.DataName)
                .ToHashSet();
        }

        public SortedDictionary<string, BymlFile> ToBgdata()
        {
            SortedDictionary<string, BymlFile> bgdata_files = new();
            Dictionary<string, int> file_counts = new();
            Dictionary<string, int> flag_counts = new()
            {
                { "bool_array_data", 4096 }, // forces the loop to create the first file on the first iteration
                { "bool_data", 4096 },
                { "f32_array_data", 4096 },
                { "f32_data", 4096 },
                { "revival_bool_data", 4096 },
                { "revival_s32_data", 4096 },
                { "s32_array_data", 4096 },
                { "s32_data", 4096 },
                { "string256_array_data", 4096 },
                { "string256_data", 4096 },
                { "string32_data", 4096 },
                { "string64_array_data", 4096 },
                { "string64_data", 4096 },
                { "vector2f_array_data", 4096 },
                { "vector2f_data", 4096 },
                { "vector3f_array_data", 4096 },
                { "vector3f_data", 4096 },
                { "vector4f_data", 4096 },
            };
            string filename;
            string file_key;
            foreach ((string type, Dictionary<int, BaseFlag> flags) in CurrFlagStore)
            {
                foreach (BaseFlag flag in flags.Values)
                {
                    file_key = flag.IsRevival ? $"revival_{type}" : type;
                    if (flag_counts[file_key] == 4096)
                    {
                        bgdata_files[$"/{file_key}_{file_counts[file_key]++}"] = new(
                            new Dictionary<string, BymlNode>()
                            {
                                { type, new(new List<BymlNode>()) }
                            }
                        );
                        flag_counts[file_key] = 0;
                    }
                    filename = $"/{file_key}_{file_counts[file_key]}";
                    bgdata_files[filename].RootNode.Hash[type].Array.Add(flag.ToByml());
                    flag_counts[file_key]++;
                }
            }
            return bgdata_files;
        }

        public SortedDictionary<string, BymlFile> ToSvdata()
        {
            BymlNode settings = new(new Dictionary<string, BymlNode>()
            {
                { "IsCommon", new(false) },
                { "IsCommonAtSameAccount", new(false) },
                { "IsSaveSecureCode", new(true) },
                { "file_name", new("game_data.sav") },
            });
            SortedDictionary<string, BymlFile> svdata_files = new();
            int file_count = 0;
            int flag_count = 8192; // forces the loop to create the first file on the first iteration
            string filename;
            foreach (Dictionary<int, BaseFlag> flags in CurrFlagStore.Values)
            {
                foreach (BaseFlag flag in flags.Values)
                {
                    if (!flag.IsSave || IGNORED_SAVE_FLAGS.Contains(flag.DataName))
                    {
                        continue;
                    }
                    if (flag_count == 8192)
                    {
                        svdata_files[$"/saveformat_{file_count++}"] = new(new Dictionary<string, BymlNode>()
                        {
                            {
                                "file_list",
                                new(new List<BymlNode>()
                                {
                                    settings,
                                    new(new List<BymlNode>())
                                })
                            }
                        });
                        flag_count = 0;
                    }
                    filename = $"/saveformat_{file_count}";
                    svdata_files[filename].RootNode.Hash["file_list"].Array[1].Array.Add(flag.ToByml());
                    flag_count++;
                }
            }
            foreach (BymlFile byml in svdata_files.Values)
            {
                byml.RootNode.Hash["save_info"] = new(new List<BymlNode>()
                {
                    new BymlNode(new Dictionary<string, BymlNode>()
                    {
                        { "directory_num", new BymlNode(file_count + 3) },
                        { "is_build_machine", new BymlNode(true) },
                        { "revision", new BymlNode(18203) },
                    })
                });
            }
            return svdata_files;
        }
    }
}
