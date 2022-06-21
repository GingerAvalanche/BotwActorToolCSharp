using BotwActorTool.Lib.Info;
using BotwActorTool.Lib.Pack;
using Nintendo.Aamp;
using Nintendo.Sarc;
using Syroot.BinaryData.Core;

namespace BotwActorTool.Lib
{
    public class FarActor
    {
        private static readonly Dictionary<string, string> FAR_LINK_DEFAULTS = new()
        {
            { "ActorNameJpn", "" },
            { "Priority", "Default" },
            { "AIProgramUser", "Dummy" },
            { "AIScheduleUser", "Dummy" },
            { "ASUser", "Dummy" },
            { "AttentionUser", "Dummy" },
            { "AwarenessUser", "Dummy" },
            { "BoneControlUser", "Dummy" },
            { "ActorCaptureUser", "Dummy" },
            { "ChemicalUser", "Dummy" },
            { "DamageParamUser", "Dummy" },
            { "DropTableUser", "Dummy" },
            { "ElinkUser", "Dummy" },
            { "GParamUser", "Dummy" },
            { "LifeConditionUser", "Landmark01km"},
            { "LODUser", "Dummy" },
            { "ModelUser", ""},
            { "PhysicsUser", ""},
            { "ProfileUser", "MapConstPassive" },
            { "RgBlendWeightUser", "Dummy" },
            { "RgConfigListUser", "Dummy" },
            { "RecipeUser", "Dummy" },
            { "ShopDataUser", "Dummy" },
            { "SlinkUser", "Dummy" },
            { "UMiiUser", "Dummy" },
            { "XlinkUser", "Dummy" },
            { "AnimationInfo", "Dummy" },
            { "ActorScale", "1.0" },
        };

        private readonly ActorInfo info;
        private readonly ActorPack pack;
        private bool needs_info_update;
        private readonly string origname;
        public string Name { get => pack.Name; }
        public string Tags { get => pack.Tags; set { pack.Tags = value; needs_info_update = true; } }
        public string Tags2 { get => pack.Tags2; set { pack.Tags2 = value; needs_info_update = true; } }

        public FarActor(string name, byte[] physics_file)
        {
            origname = $"{name}_Far";
            Dictionary<string, byte[]> files = new();

            // Create the ActorLink
            AampFile file = AampFile.New(2);
            file.RootNode.ParamObjects = new ParamObject[3];
            file.RootNode.ParamObjects[0] = new ParamObject()
            {
                HashString = "LinkTarget",
                ParamEntries = new ParamEntry[FAR_LINK_DEFAULTS.Count]
            };
            int count = 0;
            foreach ((string link, string linkref) in FAR_LINK_DEFAULTS)
            {
                file.RootNode.ParamObjects[0].ParamEntries[count] = new()
                {
                    HashString = link,
                    ParamType = ParamType.StringRef,
                    Value = linkref switch
                    {
                        "Default" or "Dummy" or "Landmark01km" or "MapConstPassive" => linkref,
                        "1.0" => float.Parse(linkref),
                        "" => link switch
                        {
                            "ActorNameJpn" or "ModelUser" => $"{name}_Far",
                            "PhysicsUser" => name,
                            _ => throw new Exception("IMPOSSIBRU!"),
                        },
                        _ => throw new Exception("IMPOSSIBRU!"),
                    },
                };
            }
            file.RootNode.ParamObjects[1] = new ParamObject()
            {
                HashString = "Tags",
                ParamEntries = new ParamEntry[]
                {
                    new()
                    {
                        HashString = "Tag0",
                        ParamType = ParamType.StringRef,
                        Value = "AutoCreateFarActor",
                    }
                },
            };
            file.RootNode.ParamObjects[2] = new ParamObject()
            {
                Hash = 1115720914,
                ParamEntries = new ParamEntry[]
                {
                    new()
                    {
                        HashString = "Tag0",
                        ParamType = ParamType.StringRef,
                        Value = "ReplaceThisTagWithAValidOne",
                    }
                },
            };
            files[$"Actor/ActorLink/{name}_Far.bxml"] = file.ToBinary();

            // Create the LifeCondition
            file = AampFile.New(2);
            file.RootNode.ParamObjects = new ParamObject[]
            {
                new()
                {
                    HashString = "DisplayDistance",
                    ParamEntries = new ParamEntry[]
                    {
                        new()
                        {
                            HashString = "Item",
                            ParamType = ParamType.Float,
                            Value = 1000.0,
                        }
                    }
                },
                new()
                {
                    HashString = "AutoDisplayDistanceAlgorithm",
                    ParamEntries = new ParamEntry[]
                    {
                        new()
                        {
                            HashString = "Item",
                            ParamType = ParamType.StringRef,
                            Value = "Bounding.Y",
                        }
                    }
                },
                new()
                {
                    HashString = "YLimitAlgorithm",
                    ParamEntries = new ParamEntry[]
                    {
                        new()
                        {
                            HashString = "Item",
                            ParamType = ParamType.StringRef,
                            Value = "NoLimit",
                        }
                    }
                },
            };
            files[$"Actor/LifeCondition/Landmark01km.blifecondition"] = file.ToBinary();

            // Create the ModelList
            file = AampFile.New(2);
            file.RootNode.ParamObjects = new ParamObject[]
            {
                new()
                {
                    HashString = "ControllerInfo",
                    ParamEntries = new ParamEntry[]
                    {
                        new()
                        {
                            Hash = 452881839,
                            ParamType = ParamType.Boolean,
                            Value = true,
                        },
                        new()
                        {
                            HashString = "MulColor",
                            ParamType = ParamType.Color4F,
                            Value = new float[]
                            {
                                1.0f, 1.0f, 1.0f, 1.0f,
                            },
                        },
                        new()
                        {
                            HashString = "AddColor",
                            ParamType = ParamType.Color4F,
                            Value = new float[]
                            {
                                0.0f, 0.0f, 0.0f, 0.0f,
                            },
                        },
                        new()
                        {
                            Hash = 2335622703,
                            ParamType = ParamType.String64,
                            Value = "Fill",
                        },
                        new()
                        {
                            Hash = 468054209,
                            ParamType = ParamType.String64,
                            Value = "MapUnitShape",
                        },
                        new()
                        {
                            Hash = 3089169744,
                            ParamType = ParamType.String64,
                            Value = "",
                        },
                        new()
                        {
                            Hash = 2592531187,
                            ParamType = ParamType.String64,
                            Value = "",
                        },
                        new()
                        {
                            Hash = 3331358099,
                            ParamType = ParamType.String64,
                            Value = "",
                        },
                        new()
                        {
                            Hash = 3142504426,
                            ParamType = ParamType.String64,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "BaseScale",
                            ParamType = ParamType.Vector3F,
                            Value = new float[]
                            {
                                1.0f, 1.0f, 1.0f,
                            },
                        },
                        new()
                        {
                            HashString = "VariationMatAnim",
                            ParamType = ParamType.String64,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "VariationMatAnimFrame",
                            ParamType = ParamType.Int,
                            Value = 0,
                        },
                        new()
                        {
                            HashString = "VariationShaderAnim",
                            ParamType = ParamType.String64,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "VariationShaderAnimFrame",
                            ParamType = ParamType.Int,
                            Value = 0,
                        },
                        new()
                        {
                            Hash = 1528658372,
                            ParamType = ParamType.String32,
                            Value = "Auto",
                        },
                        new()
                        {
                            HashString = "FarModelCullingCenter",
                            ParamType = ParamType.Vector3F,
                            Value = new float[]
                            {
                                0.0f, 0.0f, 0.0f,
                            },
                        },
                        new()
                        {
                            HashString = "FarModelCullingRadius",
                            ParamType = ParamType.Float,
                            Value = 0.0f,
                        },
                        new()
                        {
                            HashString = "FarModelCullingHeight",
                            ParamType = ParamType.Float,
                            Value = 0.0f,
                        },
                        new()
                        {
                            HashString = "CalcAABBASKey",
                            ParamType = ParamType.String64,
                            Value = "Wait",
                        },
                    },
                },
                new()
                {
                    HashString = "Attention",
                    ParamEntries = new ParamEntry[]
                    {
                        new()
                        {
                            HashString = "IsEnableAttention",
                            ParamType = ParamType.Boolean,
                            Value = true,
                        },
                        new()
                        {
                            HashString = "LookAtBone",
                            ParamType = ParamType.String32,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "LookAtBoneOffset",
                            ParamType = ParamType.Vector3F,
                            Value = new float[]
                            {
                                0.0f, 0.0f, 0.0f,
                            },
                        },
                        new()
                        {
                            HashString = "CursorOffsetY",
                            ParamType = ParamType.Float,
                            Value = 0.0f,
                        },
                        new()
                        {
                            HashString = "AIInfoOffsetY",
                            ParamType = ParamType.Float,
                            Value = 0.0f,
                        },
                        new()
                        {
                            HashString = "CutTargetBone",
                            ParamType = ParamType.String32,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "CutTargetOffset",
                            ParamType = ParamType.Vector3F,
                            Value = new float[]
                            {
                                0.0f, 0.0f, 0.0f,
                            },
                        },
                        new()
                        {
                            HashString = "GameCameraBone",
                            ParamType = ParamType.String32,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "GameCameraOffset",
                            ParamType = ParamType.Vector3F,
                            Value = new float[]
                            {
                                0.0f, 0.0f, 0.0f,
                            },
                        },
                        new()
                        {
                            HashString = "BowCameraBone",
                            ParamType = ParamType.String32,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "BowCameraOffset",
                            ParamType = ParamType.Vector3F,
                            Value = new float[]
                            {
                                0.0f, 0.0f, 0.0f,
                            },
                        },
                        new()
                        {
                            HashString = "AttackTargetBone",
                            ParamType = ParamType.String32,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "AttackTargetOffset",
                            ParamType = ParamType.Vector3F,
                            Value = new float[]
                            {
                                0.0f, 0.0f, 0.0f,
                            },
                        },
                        new()
                        {
                            HashString = "AttackTargetOffsetBack",
                            ParamType = ParamType.Float,
                            Value = 0.0f,
                        },
                        new()
                        {
                            HashString = "AtObstacleChkOffsetBone",
                            ParamType = ParamType.String32,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "AtObstacleChkOffset",
                            ParamType = ParamType.Vector3F,
                            Value = new float[]
                            {
                                0.0f, 0.0f, 0.0f,
                            },
                        },
                        new()
                        {
                            HashString = "AtObstacleChkUseLookAtPos",
                            ParamType = ParamType.Boolean,
                            Value = true,
                        },
                        new()
                        {
                            HashString = "CursorAIInfoBaseBone",
                            ParamType = ParamType.String32,
                            Value = "",
                        },
                        new()
                        {
                            HashString = "CursorAIInfoBaseOffset",
                            ParamType = ParamType.Vector3F,
                            Value = new float[]
                            {
                                0.0f, 0.0f, 0.0f,
                            },
                        },
                    },
                },
            };
            file.RootNode.ChildParams = new ParamList[]
            {
                new()
                {
                    HashString = "ModelData",
                    ChildParams = new ParamList[]
                    {
                        new()
                        {
                            HashString = "ModelData_0",
                            ParamObjects = new ParamObject[]
                            {
                                new()
                                {
                                    HashString = "Base",
                                    ParamEntries = new ParamEntry[]
                                    {
                                        new()
                                        {
                                            HashString = "Folder",
                                            ParamType = ParamType.String64,
                                            Value = name,
                                        },
                                    },
                                },
                            },
                            ChildParams = new ParamList[]
                            {
                                new()
                                {
                                    HashString = "Unit",
                                    ParamObjects = new ParamObject[]
                                    {
                                        new()
                                        {
                                            HashString = "Unit_0",
                                            ParamEntries = new ParamEntry[]
                                            {
                                                new()
                                                {
                                                    HashString = "UnitName",
                                                    ParamType = ParamType.String64,
                                                    Value = $"{name}_Far",
                                                },
                                                new()
                                                {
                                                    HashString = "BindBone",
                                                    ParamType = ParamType.String64,
                                                    Value = "",
                                                },
                                            },
                                        },
                                    },
                                },
                            },
                        },
                    },
                },
                new()
                {
                    HashString = "AnmTarget_0",
                    ParamObjects = new ParamObject[]
                    {
                        new()
                        {
                            HashString = "Base",
                            ParamEntries = new ParamEntry[]
                            {
                                new()
                                {
                                    HashString = "NumASSlot",
                                    ParamType = ParamType.Int,
                                    Value = 1,
                                },
                                new()
                                {
                                    HashString = "IsParticleEnable",
                                    ParamType = ParamType.Boolean,
                                    Value = true,
                                },
                                new()
                                {
                                    HashString = "TargetType",
                                    ParamType = ParamType.Int,
                                    Value = 0,
                                },
                            },
                        },
                    },
                },
            };
            files[$"Actor/ModelList/{name}_Far.bmodellist"] = file.ToBinary();

            // Get the Physics from the base pack
            files[$"Actor/Physics/{name}.bphysics"] = physics_file;

            // Create the SarcFile
            SarcFile far_sarc = new(files, Endian.Little);
            pack = new($"{name}_Far", far_sarc);
            info = new(this);
            needs_info_update = true;
        }
        public FarActor(SarcFile sarc)
        {
            origname = Path.GetFileNameWithoutExtension(sarc.Files.Keys.First(s => Path.GetExtension(s) == ".bxml"));
            pack = new ActorPack(origname, sarc);
            info = new(this);
        }

        public void SetName(string name)
        {
            pack.SetName(name);
            needs_info_update = true;
        }

        public string GetLink(string link) => pack.GetLink(link);
        public void SetLink(string link, string linkref)
        {
            pack.SetLink(link, linkref);
            needs_info_update = true;
        }

        public string GetLinkData(string link) => pack.GetLinkData(link);
        public void SetLinkData(string link, string data)
        {
            pack.SetLinkData(link, data);
            needs_info_update = true;
        }
        public AampFile GetPackAampFile(string link) => pack.GetAampFile(link);

        public void Update()
        {
            if (needs_info_update)
            {
                info.Update();
            }
        }

        public void Write(string mod_root, bool big_endian)
        {
            throw new NotImplementedException();
        }
    }
}
