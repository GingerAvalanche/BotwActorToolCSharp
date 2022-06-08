﻿using System.Collections.Generic;
using System.IO;

namespace BotwActorToolLib.Texts
{
    class ActorTexts
    {
        public string ActorName { private get; set; }
        private readonly string OrigActorName;
        private readonly string Profile;
        private readonly Dictionary<string, string> Texts = new()
        {
            { "BaseName", "" },
            { "Name", "" },
            { "Desc", "" },
            { "PictureBook", "" }
        };
        public ActorTexts(string pack, string profile)
        {
            ActorName = Path.GetFileNameWithoutExtension(pack);
            OrigActorName = ActorName;
            Profile = profile;
            string lang = new Util.BATSettings().GetSetting("lang");
            MSBT.MSBT msbt = new(Util.GetFileAnywhere(Util.GetModRoot(pack), $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt"));
            Dictionary<string, string> temp = msbt.GetTexts();
            foreach (KeyValuePair<string, string> kvp in Texts)
            {
                if (temp.ContainsKey($"{ActorName}_{kvp.Key}"))
                {
                    Texts[kvp.Key] = temp[$"{ActorName}_{kvp.Key}"];
                }
            }
        }
        public void SetText(string name, string text)
        {
            Texts[name] = text;
        }
        public string GetText(string name)
        {
            return Texts[name];
        }
        public void DeleteActor(string ModRoot)
        {
            string lang = new Util.BATSettings().GetSetting("lang");
            MSBT.MSBT msbt = new(Util.GetFileAnywhere(ModRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt"));
            Dictionary<string, string> AllTexts = msbt.GetTexts();
            AllTexts.Remove($"{OrigActorName}_BaseName");
            AllTexts.Remove($"{OrigActorName}_Name");
            AllTexts.Remove($"{OrigActorName}_Desc");
            AllTexts.Remove($"{OrigActorName}_PictureBook");
            msbt.SetTexts(AllTexts);
            Util.InjectFile(ModRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt", msbt.Write().ToArray());
        }
        public void Write(string ModRoot)
        {
            string lang = new Util.BATSettings().GetSetting("lang");
            MSBT.MSBT msbt = new(Util.GetFileAnywhere(ModRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt"));
            Dictionary<string, string> AllTexts = msbt.GetTexts();
            foreach (KeyValuePair<string, string> kvp in Texts)
            {
                AllTexts[$"{ActorName}_{kvp.Key}"] = kvp.Value;
            }
            msbt.SetTexts(AllTexts);
            Util.InjectFile(ModRoot, $"Pack/Bootup_{lang}.pack//Message/Msg_{lang}.product.ssarc//ActorType/{Profile}.msbt", msbt.Write().ToArray());
        }
    }
}