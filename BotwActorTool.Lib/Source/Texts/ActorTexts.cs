using MsbtLib;

namespace BotwActorTool.Lib.Texts
{
    class ActorTexts
    {
        private static readonly HashSet<string> msbtProfiles = new()
        {
            "ArmorHead",
            "ArmorLower",
            "ArmorUpper",
            "Bullet",
            "CapturedActor",
            "CookResult",
            "DemoNPC",
            "Dragon",
            "Enemy",
            "GelEnemy",
            "GiantEnemy",
            "Guardian",
            "Horse",
            "HorseObject",
            "HorseReins",
            "HorseSaddle",
            "Item",
            "LastBoss",
            "MapConstActive",
            "MapConstPassive",
            "MapDynamicActive",
            "MapDynamicPassive",
            "NPC",
            "PlayerItem",
            "Prey",
            "Sandworm",
            "Siteboss",
            "Swarm",
            "WeaponBow",
            "WeaponLargeSword",
            "WeaponShield",
            "WeaponSmallSword",
            "WeaponSpear",
        };
        private bool hasTexts;
        public bool HasTexts { get; }
        public string ActorName { private get; set; }
        public Dictionary<string, MsbtEntry> Texts { get => texts; }
        private readonly string orig_actor_name;
        private readonly string profile;
        private readonly Dictionary<string, MsbtEntry> texts = new()
        {
            { "BaseName", new("", "") },
            { "Name", new("", "") },
            { "Desc", new("", "") },
            { "PictureBook", new("", "") }
        };

        public ActorTexts(string pack, string profile)
        {
            ActorName = Path.GetFileNameWithoutExtension(pack);
            orig_actor_name = ActorName;
            this.profile = profile;
            hasTexts = msbtProfiles.Contains(profile);
            if (!hasTexts)
            {
                return;
            }
            string lang = Config.Lang;
            MSBT msbt = new(Util.GetFileAnywhere(Util.GetModRoot(pack), $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{profile}.msbt"));
            Dictionary<string, MsbtEntry> temp = msbt.GetTexts();
            foreach (string key in texts.Keys) {
                if (temp.ContainsKey($"{ActorName}_{key}")) {
                    texts[key] = temp[$"{ActorName}_{key}"];
                }
            }
        }

        public void SetProfile(string profile)
        {
            hasTexts = msbtProfiles.Contains(profile);
        }

        public void DeleteActor(string modRoot)
        {
            if (!hasTexts)
            {
                return;
            }
            string lang = Config.Lang;
            MSBT msbt = new(new MemoryStream(Util.GetFileAnywhere(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{profile}.msbt")));
            Dictionary<string, MsbtEntry> AllTexts = msbt.GetTexts();
            AllTexts.Remove($"{orig_actor_name}_BaseName");
            AllTexts.Remove($"{orig_actor_name}_Name");
            AllTexts.Remove($"{orig_actor_name}_Desc");
            AllTexts.Remove($"{orig_actor_name}_PictureBook");
            msbt.SetTexts(AllTexts);
            Util.InjectFile(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{profile}.msbt", msbt.Write());
        }

        public void Write(string modRoot)
        {
            if (!hasTexts)
            {
                return;
            }
            string lang = Config.Lang;
            MSBT msbt = new(new MemoryStream(Util.GetFileAnywhere(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{profile}.msbt")));
            Dictionary<string, MsbtEntry> AllTexts = msbt.GetTexts();
            foreach ((string key, MsbtEntry entry) in texts) {
                AllTexts[$"{ActorName}_{key}"] = entry;
            }
            msbt.SetTexts(AllTexts);
            Util.InjectFile(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{profile}.msbt", msbt.Write());
        }
    }
}
