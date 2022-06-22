using BotwActorTool.Lib.Gamedata.Flags;
using Nintendo.Byml;
using System.Collections.ObjectModel;

namespace BotwActorTool.Lib.Gamedata
{
    public class FlagStore
    {
        private static readonly ReadOnlyCollection<string> IgnoredSaveFlags = new(new List<string>
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

        public void AddFlagsFromByml(string filename, BymlFile byml)
        {
            bool IsRevival = filename.Contains("revival");

            foreach ((string type, BymlNode hash) in byml.RootNode.Hash) {
                foreach (BymlNode flag in hash.Array)
                {
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
            }
        }

        public void AddFlagsFromBymlNoOverwrite(string filename, BymlFile byml)
        {
            bool IsRevival = filename.Contains("revival");

            foreach ((string type, BymlNode hash) in byml.RootNode.Hash)
            {
                foreach (BymlNode flag in hash.Array)
                {
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

        public BymlFile ToBgdata(string prefix)
        {
            string type = prefix.Replace("revival_", "");
            Dictionary<string, BymlNode> rootNode = new()
            {
                {
                    prefix,
                    new(
                        prefix switch
                        {
                            "revival_bool_data" or "revival_s32_data" => CurrFlagStore[type]
                                .Where(p => p.Value.IsRevival)
                                .Select(p => p.Value.ToByml())
                                .ToList(),
                            _ => CurrFlagStore[type]
                                .Where(p => !p.Value.IsRevival)
                                .Select(p => p.Value.ToByml())
                                .ToList(),
                        }
                    )
                }
            };
            return new(rootNode);
        }

        public BymlFile ToSvdata()
        {
            BymlNode settings = new(new Dictionary<string, BymlNode>()
            {
                { "IsCommon", new(false) },
                { "IsCommonAtSameAccount", new(false) },
                { "IsSaveSecureCode", new(true) },
                { "file_name", new("game_data.sav") },
            });
            BymlNode flags = new(CurrFlagStore
                    .SelectMany(p => p.Value.Values)
                    .Where(f => f.IsSave && !IgnoredSaveFlags.Contains(f.DataName))
                    .Select(f => f.ToSvByml())
                    .ToList());
            BymlNode array = new(new List<BymlNode>() {
                settings,
                flags,
            });
            return new BymlFile(new Dictionary<string, BymlNode>() { { "file_list", array } });
        }
    }
}
