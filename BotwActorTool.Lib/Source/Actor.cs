using BotwActorTool.Lib.Gamedata;
using BotwActorTool.Lib.Gamedata.Flags;
using BotwActorTool.Lib.Info;
using BotwActorTool.Lib.Texts;
using MsbtLib;
using Nintendo.Byml;
using Nintendo.Sarc;
using Syroot.BinaryData.Core;
using Yaz0Library;

namespace BotwActorTool.Lib
{
    public class Actor : FarActor
    {
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
        
        private readonly ActorTexts texts;
        private bool needs_info_update;
        private FarActor? far_actor;
        private readonly FlagStore store;
        private readonly Dictionary<string, HashSet<int>> flag_hashes;
        private bool resident;
        public override bool HasFar { get => far_actor != null; }
        public bool Resident { get => resident; set => resident = value; }
        public Dictionary<string, MsbtEntry> Texts { get => texts.Texts; }
        public Actor(string filename) : base(filename)
        {
            resident = filename.Contains("TitleBG.pack");
            store = new();
            flag_hashes = new() { { "bool_data", new() }, { "s32_data", new() } };
            texts = new(filename, pack.GetLink("ProfileUser"));

            string far_filename = filename.Replace(origname, $"{origname}_Far");
            if (File.Exists(far_filename))
            {
                far_actor = new FarActor(far_filename);
            }
            ActorInfo.ReleaseActorInfoFile();
        }

        public override void SetName(string name)
        {
            base.SetName(name);
            texts.ActorName = name;
            SetFlags(name);
            far_actor?.SetName($"{name}_Far");
        }
        public override void SetLink(string link, string linkref)
        {
            base.SetLink(link, linkref);
            pack.SetLink(link, linkref);
            needs_info_update = true;
        }
        public override void SetLinkData(string link, string data)
        {
            pack.SetLinkData(link, data);
            needs_info_update = true;
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

        public override void Write(string modRoot)
        {
            ActorInfo.LoadActorInfoFile(modRoot);
            Console console = pack.Endianness == Endian.Big ? Console.WiiU : Console.Switch;
            if (resident)
            {
                string titlebg_path = $"{modRoot}/Pack/TitleBG.pack";
                if (!File.Exists(titlebg_path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(titlebg_path)!);
                    File.Copy(Util.FindFileOrig("Pack/TitleBG.pack", console), titlebg_path);
                }
                string actor_path = $"Actor/Pack/{pack.Name}.sbactorpack";
                byte[] compressed_bytes = Yaz0.Compress(pack.Write());
                Util.InjectFile(modRoot, actor_path, compressed_bytes);
                Update();
                info.Apply();
            }
            else
            {
                base.Write(modRoot);
            }

            far_actor?.Write(modRoot);
            ActorInfo.Write(modRoot);
            ActorInfo.ReleaseActorInfoFile();

            texts.Write(modRoot);

            string bootup_path = $"{modRoot}/Pack/Bootup.pack";
            if (!File.Exists(bootup_path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(bootup_path)!);
                File.Copy(Util.FindFileOrig("Pack/Bootup.pack", console), bootup_path);
            }
            SarcFile gamedata_sarc = new(Yaz0.Decompress(Util.GetFile($"{bootup_path}/GameData/gamedata.ssarc")));
            foreach ((string name, byte[] data) in gamedata_sarc.Files)
            {
                store.AddFlagsFromBymlNoOverwrite(name, new(data));
            }

            gamedata_sarc = new(new Dictionary<string, byte[]>(), pack.Endianness);
            foreach ((string filename, BymlFile file) in store.ToBgdata())
            {
                gamedata_sarc.Files[filename] = file.ToBinary();
            }
            SortedDictionary<string, BymlFile> savedata_files = store.ToSvdata();
            int format_num = savedata_files.Count;
            foreach (BymlFile file in Util.GetAccountSaveFormatFiles(modRoot))
            {
                savedata_files[$"/saveformat_{format_num}"] = file;
                format_num++;
            }
            SarcFile savedata_sarc = new(new Dictionary<string, byte[]>(), pack.Endianness);
            foreach ((string filename, BymlFile file) in savedata_files)
            {
                savedata_sarc.Files[filename] = file.ToBinary();
            }
            Util.InjectFilesIntoBootup(modRoot, new()
            {
                ("GameData/gamedata.ssarc", gamedata_sarc.ToBinary()),
                ("GameData/savedataformat.ssarc", savedata_sarc.ToBinary()),
            });
        }
    }
}
