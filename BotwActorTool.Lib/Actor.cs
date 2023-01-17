using BotwActorTool.Lib.Gamedata;
using BotwActorTool.Lib.Gamedata.Flags;
using BotwActorTool.Lib.Info;
using BotwActorTool.Lib.Texts;
using MsbtLib;
using Nintendo.Aamp;
using Nintendo.Byml;
using Nintendo.Sarc;
using Nintendo.Yaz0;
using Syroot.BinaryData.Core;

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
        private FarActor? far_actor;
        private readonly FlagStore store;
        private readonly Dictionary<string, HashSet<int>> flag_hashes;
        public string[] AnimSeqNames { get => pack.AnimSeqNames; }
        public override bool HasFar { get => far_actor != null; }
        public Dictionary<string, MsbtEntry>? Texts { get => texts.HasTexts ? texts.Texts : null; }
        public Actor(string name, string modRoot) : base(name, modRoot)
        {
            store = new();
            flag_hashes = new() { { "bool_data", new() }, { "s32_data", new() } };
            texts = new(name, pack.GetLink("ProfileUser"), modRoot);

            if (info.isHasFar is true)
            {
                far_actor = new FarActor($"{origname}_Far", modRoot);
            }
        }

        public static Actor LoadActor(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);
            string modRoot = Util.GetModRoot(path);
            return new Actor(name, modRoot);
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
            if (link == "ProfileUser")
            {
                texts.SetProfile(linkref);
            }
            needs_info_update = true;
        }
        public override void SetLinkData(string link, string data)
        {
            pack.SetLinkDataYaml(link, data);
            needs_info_update = true;
        }

        public string GetAnimSeqList() => pack.GetLinkDataYaml("ASUser");
        public void SetAnimSeqList(string data)
        {
            List<string> as_names = new();
            AampFile aamp = AampFile.FromYml(data);
            foreach (ParamObject obj in aamp.RootNode.Lists("ASDefines")!.ParamObjects)
            {
                as_names.Add((string)obj.Params("Filename")!.Value);
            }
            string[] existing_names = pack.AnimSeqNames;
            foreach (string name in as_names)
            {
                if (!existing_names.Contains(name))
                {
                    pack.SetAnimSeq(name, AampFile.New(2).ToYml());
                }
            }
            foreach (string name in existing_names)
            {
                if (!as_names.Contains(name))
                {
                    pack.RemoveAnimSeq(name);
                }
            }
            pack.SetLinkDataYaml("ASUser", data);
        }
        public string GetAnimSeq(string name) => pack.GetAnimSeq(name);
        public void SetAnimSeq(string name, string data) => pack.SetAnimSeq(name, data);

        public bool SetHasFar(bool enabled)
        {
            if (resident || (enabled ^ HasFar))
            {
                return false;
            }
            else if (enabled)
            {
                far_actor = new FarActor(Name, pack.GetLinkDataBytes("PhysicsUser"));
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
            ActorInfo.SetActorInfoFile(new BymlFile(Util.GetFileAnywhere(modRoot, "Actor/Pack/ActorInfo.product.sbyml")));
            base.Write(modRoot);
            far_actor?.Write(modRoot);
            ActorInfo.Write(modRoot);
            ActorInfo.ClearActorInfoFile();
            texts.Write(modRoot);
            store.Write(modRoot);
        }

        public ArmorUpgradable CanMakeArmorUpgradable()
        {
            if (resident)
            {
                return ArmorUpgradable.IsResident;
            }
            else if (!Name.Contains("Armor_"))
            {
                return ArmorUpgradable.NotAnArmor;
            }
            ParamObject obj = GetPackAampFile("GParamUser").RootNode.Objects("Armor")!;
            if ((int)obj.Params("StarNum")!.Value != 1)
            {
                return ArmorUpgradable.IsUpgrade;
            }
            else if (((StringEntry)obj.Params("NextRankName")!.Value).ToString() != "''")
            {
                return ArmorUpgradable.AlreadyUpgradable;
            }
            return ArmorUpgradable.CanUpgrade;
        }

        public void MakeArmorUpgradable(string modRoot, string firstUpgradeName)
        {
            if (CanMakeArmorUpgradable() != ArmorUpgradable.CanUpgrade)
            {
                return;
            }

            AampFile gparam = new(pack.GetLinkDataBytes("GParamUser"));
            ParamEntry starNum = gparam.RootNode.Objects("Armor")!.Params("StarNum")!;
            ParamEntry enableCompBonus = gparam.RootNode.Objects("SeriesArmor")!.Params("EnableCompBonus")!;
            StringEntry nextRank = (StringEntry)gparam.RootNode.Objects("Armor")!.Params("NextRankName")!.Value;
            nextRank.Data = nextRank.EncodeType.GetBytes(firstUpgradeName);
            pack.SetLinkDataBytes("GParamUser", gparam.ToBinary());

            needs_info_update = true;
            Write(modRoot);

            // Set UseIconActorName if need be, to be left for every upgrade actor
            StringEntry armorIconName = (StringEntry)gparam.RootNode.Objects("Item")!.Params("UseIconActorName")!.Value;
            if (armorIconName.ToString() == "''")
            {
                armorIconName.Data = armorIconName.EncodeType.GetBytes(Name);
            }

            // Create recipe for first upgrade, using this actor as ingredient
            AampFile recipe = new(new byte[] {
                0x41, 0x41, 0x4D, 0x50, 0x01, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00,
                0x00, 0x2C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00,
                0x00, 0x00, 0x78, 0x6D, 0x6C, 0x00, 0x10, 0x00, 0x00, 0x00, 0x6C,
                0xCB, 0xF6, 0xA4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            });

            ParamObject[] objects = new ParamObject[2];
            ParamEntry[] entries = new ParamEntry[2];
            entries[0] = new() { HashString = "TableNum", Value = 1 };
            entries[1] = new() { HashString = "Table01", Value = new StringEntry("Normal0") };
            objects[0] = new() { HashString = "Header", ParamEntries = entries };
            entries = new ParamEntry[7];
            entries[0] = new() { HashString = "ColumnNum", Value = 3 };
            StringEntry prevIngredient = new(Name);
            entries[1] = new() { HashString = "ItemName01", Value = prevIngredient };
            entries[2] = new() { HashString = "ItemNum01", Value = 1 };
            entries[3] = new() { HashString = "ItemName02", Value = new StringEntry("Item_Enemy_00") };
            entries[4] = new() { HashString = "ItemNum02", Value = 1 };
            entries[5] = new() { HashString = "ItemName03", Value = new StringEntry("Item_Enemy_01") };
            entries[6] = new() { HashString = "ItemNum03", Value = 1 };
            objects[1] = new() { HashString = "Normal0", ParamEntries = new ParamEntry[7] };
            recipe.RootNode.ParamObjects = objects;

            string[] upgradeNameParts = firstUpgradeName.Split('_');
            int firstUpgradeNum = int.Parse(upgradeNameParts[1]);
            for (int currUpgradeNum = 0; currUpgradeNum < 4; currUpgradeNum++)
            {
                // Name stuff
                upgradeNameParts[1] = $"{firstUpgradeNum + currUpgradeNum:D3}";
                string currActorName = string.Join("_", upgradeNameParts);
                texts.ActorName = currActorName;
                SetFlags(currActorName);
                pack.SetName(currActorName, false);

                // GParam stuff
                pack.SetLink("GParamUser", currActorName);
                starNum.Value = currUpgradeNum + 2; // StarNum 2 = currUpgradeNum 0 = 1 star in inventory UI
                if (currUpgradeNum == 1)
                {
                    enableCompBonus.Value = true;
                }
                upgradeNameParts[1] = currUpgradeNum == 3 ? "''" : $"{firstUpgradeNum + currUpgradeNum + 1:D3}";
                nextRank.Data = nextRank.EncodeType.GetBytes(string.Join("_", upgradeNameParts));
                pack.SetLinkDataBytes("GParamUser", gparam.ToBinary());

                // Recipe stuff
                pack.SetLink("RecipeUser", currActorName);
                pack.SetLinkDataBytes("RecipeUser", recipe.ToBinary());
                // Recipe has been written to actor, so set current actor as ingredient for next upgrade
                prevIngredient.Data = prevIngredient.EncodeType.GetBytes(currActorName);

                needs_info_update = true;
                Write(modRoot);
            }
        }
    }
}
