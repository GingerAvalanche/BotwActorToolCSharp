using ByamlExt.Byaml;
using System.Collections.Generic;

namespace BotwActorToolLib.Gamedata.Flags
{
    abstract class StringArrayFlag : StringBaseFlag
    {
        public List<string> InitValue = new List<string>(1);
        public StringArrayFlag() : base() { }
        public StringArrayFlag(OrderedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict))
            {
                foreach (string v in dict["InitValue"][0]["Values"])
                {
                    InitValue.Add(v);
                }
            }
        }
        private static bool ValidateInFlag(OrderedDictionary<string, dynamic> dict)
        {
            try
            {
                List<Dictionary<string, List<string>>> iv = dict["InitValue"];
                if (iv[0].Count > 1) { throw new System.Exception(); }
                // This is actually to force errors if the Lists are empty/Dictionary is made wrong
                if (iv[0]["Values"][0].GetType() == typeof(string)) { }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public new bool Equals(BaseFlag other)
        {
            if (!base.Equals(other))
            {
                return false;
            }
            StringArrayFlag otherStringArray = (StringArrayFlag)other;
            if (InitValue.Count != otherStringArray.InitValue.Count)
            {
                return false;
            }
            return InitValue.TrueForAll(i => InitValue.IndexOf(i) == otherStringArray.InitValue.IndexOf(i));
        }
        public new OrderedDictionary<string, dynamic> ToByml()
        {
            OrderedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = new List<Dictionary<string, List<string>>>(1);
            byml["InitValue"][0]["Values"] = InitValue;
            return byml;
        }
    }
}
