using MsbtLib;

namespace BotwActorTool.Lib.Texts
{
    class ActorTexts
    {
        public string ActorName { private get; set; }
        private readonly string OrigActorName;
        private readonly string Profile;
        private readonly Dictionary<string, MsbtEntry> Texts = new()
        {
            { "BaseName", new("", "") },
            { "Name", new("", "") },
            { "Desc", new("", "") },
            { "PictureBook", new("", "") }
        };

        public ActorTexts(string pack, string profile)
        {
            ActorName = Path.GetFileNameWithoutExtension(pack);
            OrigActorName = ActorName;
            Profile = profile;
            string lang = new Util.BATSettings().GetSetting("lang");
            MSBT msbt = new(new MemoryStream(Util.GetFileAnywhere(Util.GetModRoot(pack), $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt") ?? Array.Empty<byte>()));
            Dictionary<string, MsbtEntry> temp = msbt.GetTexts();
            foreach (KeyValuePair<string, MsbtEntry> kvp in Texts) {
                if (temp.ContainsKey($"{ActorName}_{kvp.Key}")) {
                    Texts[kvp.Key] = temp[$"{ActorName}_{kvp.Key}"];
                }
            }
        }

        public void SetText(string name, string text) => Texts[name] = new("", text);
        public string GetText(string name) => Texts[name].Value;

        public void DeleteActor(string modRoot)
        {
            string lang = new Util.BATSettings().GetSetting("lang");
            MSBT msbt = new(new MemoryStream(Util.GetFileAnywhere(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt") ?? Array.Empty<byte>()));
            Dictionary<string, MsbtEntry> AllTexts = msbt.GetTexts();
            AllTexts.Remove($"{OrigActorName}_BaseName");
            AllTexts.Remove($"{OrigActorName}_Name");
            AllTexts.Remove($"{OrigActorName}_Desc");
            AllTexts.Remove($"{OrigActorName}_PictureBook");
            msbt.SetTexts(AllTexts);
            Util.InjectFile(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt", msbt.Write());
        }

        public void Write(string modRoot)
        {
            string lang = new Util.BATSettings().GetSetting("lang");
            MSBT msbt = new(new MemoryStream(Util.GetFileAnywhere(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt") ?? Array.Empty<byte>()));
            Dictionary<string, MsbtEntry> AllTexts = msbt.GetTexts();
            foreach (KeyValuePair<string, MsbtEntry> kvp in Texts) {
                AllTexts[$"{ActorName}_{kvp.Key}"] = kvp.Value;
            }
            msbt.SetTexts(AllTexts);
            Util.InjectFile(modRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt", msbt.Write());
        }
    }
}
