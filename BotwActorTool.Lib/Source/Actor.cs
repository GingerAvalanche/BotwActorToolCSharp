using BotwActorTool.Lib.Gamedata;
using BotwActorTool.Lib.Gamedata.Flags;
using BotwActorTool.Lib.Info;
using BotwActorTool.Lib.Pack;
using BotwActorTool.Lib.Texts;
using MsbtLib;
using Nintendo.Aamp;
using Nintendo.Byml;
using Nintendo.Sarc;
using Syroot.BinaryData.Core;
using Yaz0Library;

namespace BotwActorTool.Lib
{
    public class Actor
    {
        private static readonly string[] FAR_LINKS = new string[]
        {
            "GParamUser",
            "LifeConditionUser",
            "ModelUser",
            "PhysicsUser",
        };
        private static readonly Dictionary<string, Type> FLAG_CLASSES = new()
        {
            { "_DispNameFlag", typeof(BoolFlag) },
            { "EquipTime_", typeof(S32Flag) },
            { "IsGet_", typeof(BoolFlag) },
            { "IsNewPictureBook_", typeof(BoolFlag) },
            { "IsRegisteredPictureBook_", typeof(BoolFlag) },
            { "PictureBookSize_", typeof(S32Flag) },
            { "PorchTime_", typeof(S32Flag) },
        };
        private static readonly Dictionary<string, string[]> FLAG_TYPES = new()
        {
            {
                "Animal",
                new string[]
                {
                    "IsNewPictureBook_",
                    "IsRegisteredPictureBook_",
                    "PictureBookSize_"
                }
            },
            {
                "Armor",
                new string[]
                {
                    "EquipTime_",
                    "IsGet_",
                    "PorchTime_"
                }
            },
            {
                "Enemy",
                new string[]
                {
                    "IsNewPictureBook_",
                    "IsRegisteredPictureBook_",
                    "PictureBookSize_"
                }
            },
            {
                "Item",
                new string[]
                {
                    "IsGet_",
                    "IsNewPictureBook_",
                    "IsRegisteredPictureBook_",
                    "PictureBookSize_"
                }
            },
            {
                "Npc",
                new string[]
                {
                    "_DispNameFlag"
                }
            },
            {
                "Weapon",
                new string[]
                {
                    "EquipTime_",
                    "IsGet_",
                    "IsNewPictureBook_",
                    "IsRegisteredPictureBook_",
                    "PictureBookSize_",
                    "PorchTime_"
                }
            },
        };

        private readonly ActorInfo info;
        private readonly ActorPack pack;
        private readonly ActorTexts texts;
        private bool needs_info_update;
        private FarActor? far_actor;
        private readonly FlagStore store;
        private readonly Dictionary<string, HashSet<int>> flag_hashes;
        private bool resident;
        private readonly string origname;
        public string Name { get => pack.Name; }
        public bool HasFar { get => far_actor != null; }
        public bool Resident { get => resident; set => resident = value; }
        public string Tags { get => pack.Tags; set { pack.Tags = value; needs_info_update = true; } }
        public string Tags2 { get => pack.Tags2; set { pack.Tags2 = value; needs_info_update = true; } }
        public Dictionary<string, MsbtEntry> Texts { get => texts.Texts; }

        public Actor(string filename)
        {
            origname = Path.GetFileNameWithoutExtension(filename);
            pack = new ActorPack(origname, new(Yaz0.Decompress(Util.GetFile(filename))));
            resident = filename.Contains("TitleBG.pack");
            store = new();
            flag_hashes = new() { { "bool_data", new() }, { "s32_data", new() } };
            texts = new(filename, pack.GetLink("ProfileUser"));

            ActorInfo.LoadActorInfoFile(Util.GetModRoot(filename));
            info = new ActorInfo(this).LoadFromActorInfoByml();
            string far_filename = filename.Replace(origname, $"{origname}_Far");
            if (File.Exists(far_filename))
            {
                far_actor = new FarActor(new SarcFile(Util.GetFile(far_filename)));
            }
            ActorInfo.ReleaseActorInfoFile();
        }

        public void SetName(string name)
        {
            pack.SetName(name);
            texts.ActorName = name;
            SetFlags(name);
            needs_info_update = true;
            far_actor?.SetName($"{name}_Far");
        }

        public string GetLink(string link) => pack.GetLink(link);
        public bool SetLink(string link, string linkref)
        {
            if (HasFar)
            {
                if (link == "LifeConditionUser" && linkref == "Dummy")
                {
                    return false;
                }
                else if (FAR_LINKS.Contains(link))
                {
                    far_actor?.SetLink(link, linkref);
                }
            }
            pack.SetLink(link, linkref);
            needs_info_update = true;
            return true;
        }

        public bool SetHasFar(bool enabled)
        {
            if (resident || (enabled ^ HasFar))
            {
                return false;
            }
            else if (enabled)
            {
                far_actor = new FarActor(Name, pack.GetAampFile("PhysicsUser").ToBinary());
                needs_info_update = true;
                return true;
            }
            far_actor = null;
            return true;
        }

        public string GetLinkData(string link) => pack.GetLinkData(link);
        public void SetLinkData(string link, string data)
        {
            pack.SetLinkData(link, data);
            needs_info_update = true;
        }
        public AampFile GetPackAampFile(string link) => pack.GetAampFile(link);

        private void SetFlags(string name)
        {
            foreach ((string flag_type, HashSet<int> hashes) in flag_hashes)
            {
                foreach (int hash in hashes)
                {
                    store.Remove(flag_type, hash);
                }
                hashes.Clear();
            }
            string actor_type = name.Split("_")[0];
            if (FLAG_TYPES.ContainsKey(actor_type))
            {
                foreach (string prefix in FLAG_TYPES[actor_type])
                {
                    BaseFlag flag = (BaseFlag)Activator.CreateInstance(FLAG_CLASSES[prefix])!;
                    string flag_type = FLAG_CLASSES[prefix] == typeof(BoolFlag) ? "bool_data" : "s32_data";
                    flag.DataName = prefix.StartsWith('_') ? $"{name}{prefix}" : $"{prefix}{name}";
                    flag_hashes[flag_type].Add(flag.HashValue);
                    flag.GetType().GetMethod("OverrideParams")!.Invoke(flag, null);
                    store.Add(flag_type, flag);
                }
            }
        }
        public BymlNode GetInfo() => info.GetInfoByml();

        public void Update()
        {
            if (needs_info_update)
            {
                info.Update();
                needs_info_update = false;
            }
        }

        public void Write(string modRoot)
        {
            Endian test = Util.GetFileAnywhere(modRoot, "Pack/Bootup.pack")[6] == 0xFE ? Endian.Big : Endian.Little;
            string console = pack.Endianness == Endian.Big ? "WiiU" : "Switch";
            if (test != pack.Endianness)
            {
                throw new InvalidDataException(
                    $@"Actor pack is {pack.Endianness} endian but loaded Bootup.pack is {test} endian! 
                    This would cause game errors, if saved. Please set your dumps in settings to {console} 
                    dumps to save this actor."
                );
            }

            string actor_path = $"Actor/Pack/{pack.Name}.sbactorpack";
            byte[] compressed_bytes = Yaz0.Compress(pack.Write());
            if (resident)
            {
                string titlebg_path = $"{modRoot}/Pack/TitleBG.pack";
                if (!File.Exists(titlebg_path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(titlebg_path)!);
                    File.Copy(Util.FindFileOrig("Pack/TitleBG.pack", console), titlebg_path);
                }
                Util.InjectFile(modRoot, actor_path, compressed_bytes);
            }
            else
            {
                actor_path = $"{modRoot}/{actor_path}";
                if (!File.Exists(actor_path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(actor_path)!);
                }
                File.WriteAllBytes(actor_path, compressed_bytes);
            }

            texts.Write(modRoot);

            ActorInfo.LoadActorInfoFile(modRoot);
            Update();
            info.Apply();
            far_actor?.Write(modRoot);
            ActorInfo.Write(modRoot);
            ActorInfo.ReleaseActorInfoFile();
        }
    }
}
