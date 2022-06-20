using BotwActorTool.Lib.Gamedata.Flags;
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

        public void AddFlagsFromByml(string filename, SortedDictionary<string, dynamic> byml)
        {
            bool IsRevival = filename.Contains("revival");

            foreach (KeyValuePair<string, dynamic> pair in byml) {
                foreach (SortedDictionary<string, dynamic> flag in pair.Value)
                {
                    BaseFlag f = pair.Key switch
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
                        _ => throw new Exception($"Unknown flag type {pair.Key}"),
                    };
                    if (f.HashValue == 0)
                    {
                        continue;
                    }
                    CurrFlagStore[pair.Key].Add(f.HashValue, f);
                    OrigFlagStore[pair.Key].Add(f.HashValue, f);
                }
            }
        }

        public void AddFlagsFromBymlNoOverwrite(string filename, SortedDictionary<string, dynamic> byml)
        {
            bool IsRevival = filename.Contains("revival");

            foreach (KeyValuePair<string, dynamic> pair in byml) {
                foreach (SortedDictionary<string, dynamic> flag in pair.Value) {
                    BaseFlag f = pair.Key switch
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
                        _ => throw new Exception($"Unknown flag type {pair.Key}"),
                    };
                    if (f.HashValue == 0 || CurrFlagStore[pair.Key].ContainsKey(f.HashValue))
                    {
                        continue;
                    }
                    CurrFlagStore[pair.Key].Add(f.HashValue, f);
                    OrigFlagStore[pair.Key].Add(f.HashValue, f);
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

        public List<SortedDictionary<string, dynamic>> ToBgdata(string prefix)
        {
            string type = prefix.Replace("revival_", "");
            List<SortedDictionary<string, dynamic>> ret = new();
            switch (prefix) {
                case "bool_data":
                case "bool_array_data":
                case "s32_data":
                case "s32_array_data":
                case "f32_data":
                case "f32_array_data":
                case "string_data":
                case "string64_data":
                case "string64_array_data":
                case "string256_data":
                case "string256_array_data":
                case "vector2f_data":
                case "vector2f_array_data":
                case "vector3f_data":
                case "vector3f_array_data":
                case "vector4f_data":
                    ret.AddRange(CurrFlagStore[type]
                        .Where(p => !p.Value.IsRevival)
                        .Select(p => p.Value.ToByml()));
                    break;
                case "revival_bool_data":
                case "revival_s32_data":
                    ret.AddRange(CurrFlagStore[type]
                        .Where(p => p.Value.IsRevival)
                        .Select(p => p.Value.ToByml()));
                    break;
            }
            return ret.OrderBy(f => f["HashValue"]).ToList();
        }

        public List<SortedDictionary<string, dynamic>> ToSvdata()
        {
            List<SortedDictionary<string, dynamic>> ret = new();
            ret.AddRange(CurrFlagStore
                .SelectMany(p => p.Value.Values)
                .Where(f => f.IsSave && !IgnoredSaveFlags.Contains(f.DataName))
                .Select(f => f.ToSvByml()));
            return ret.OrderBy(f => f["HashValue"]).ToList();
        }
    }
}
