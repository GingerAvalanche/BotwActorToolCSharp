using ByamlExt.Byaml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BotwActorToolLib.Gamedata.Flags
{
    class S32ArrayFlag : S32BaseFlag
    {
        public List<int> InitValue = new List<int> { 0 };
        public S32ArrayFlag() : base() { MaxValue = 6553500; MinValue = -1; }
        public S32ArrayFlag(OrderedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict))
            {
                foreach (int v in dict["InitValue"])
                {
                    InitValue.Add(v);
                }
            }
        }
        private static bool ValidateInFlag(OrderedDictionary<string, dynamic> dict)
        {
            try
            {
                List<int> iv = dict["InitValue"];
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
            S32ArrayFlag otherS32Array = (S32ArrayFlag)other;
            if (InitValue.Count != otherS32Array.InitValue.Count)
            {
                return false;
            }
            return InitValue.TrueForAll(i => InitValue.IndexOf(i) == otherS32Array.InitValue.IndexOf(i));
        }
        public new OrderedDictionary<string, dynamic> ToByml()
        {
            OrderedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = InitValue;
            return byml;
        }
        public void OverrideParams(Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides)
        {
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_ARRAY_OVERRIDES"]["INIT_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_ARRAY_OVERRIDES"]["MAX_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_ARRAY_OVERRIDES"]["MIN_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    MinValue = pair.Value;
                }
            }
        }
    }
}
