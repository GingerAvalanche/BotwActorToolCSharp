using ByamlExt.Byaml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BotwActorToolLib.Gamedata.Flags
{
    class S32Flag : S32BaseFlag
    {
        public int InitValue = 0;
        private bool p_IsRevival = false;
        public S32Flag() : base() { MaxValue = 2147483647; MinValue = 0; }
        public S32Flag(OrderedDictionary<string, dynamic> dict, bool revival = false) : base(dict)
        {
            if (ValidateInFlag(dict))
            {
                InitValue = dict["InitValue"];
                p_IsRevival = revival;
            }
        }
        private static bool ValidateInFlag(OrderedDictionary<string, dynamic> dict)
        {
            try
            {
                int iv = dict["InitValue"];
                return true;
            }
            catch
            {
                return false;
            }
        }
        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other))
            {
                S32Flag otherS32 = (S32Flag)other;
                if (InitValue == otherS32.InitValue)
                {
                    return true;
                }
            }
            return false;
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
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_OVERRIDES"]["INIT_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_OVERRIDES"]["MAX_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_OVERRIDES"]["MIN_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    MinValue = pair.Value;
                }
            }
        }
        public new bool IsRevival { get => p_IsRevival; }
    }
}
