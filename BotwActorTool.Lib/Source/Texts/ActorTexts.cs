using MsbtLib;

namespace BotwActorTool.Lib.Texts
{
    class ActorTexts
    {
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
            string lang = Config.Lang;
            MSBT msbt = new(new MemoryStream(Util.GetFileAnywhere(Util.GetModRoot(pack), $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{profile}.msbt")!));
            Dictionary<string, MsbtEntry> temp = msbt.GetTexts();
            foreach (KeyValuePair<string, MsbtEntry> kvp in texts) {
                if (temp.ContainsKey($"{ActorName}_{kvp.Key}")) {
                    texts[kvp.Key] = temp[$"{ActorName}_{kvp.Key}"];
                }
            }
        }

        public void DeleteActor(string modRoot)
        {
            string lang = Config.Lang;
            MSBT msbt = new(new MemoryStream(Util.GetFileAnywhere(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{profile}.msbt")!));
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
            string lang = Config.Lang;
            MSBT msbt = new(new MemoryStream(Util.GetFileAnywhere(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{profile}.msbt") ?? Array.Empty<byte>()));
            Dictionary<string, MsbtEntry> AllTexts = msbt.GetTexts();
            foreach (KeyValuePair<string, MsbtEntry> kvp in texts) {
                AllTexts[$"{ActorName}_{kvp.Key}"] = kvp.Value;
            }
            msbt.SetTexts(AllTexts);
            Util.InjectFile(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{profile}.msbt", msbt.Write());
        }
    }
}
