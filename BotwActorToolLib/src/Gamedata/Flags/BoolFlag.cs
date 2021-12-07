using ByamlExt.Byaml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BotwActorToolLib.Gamedata.Flags
{
    class BoolFlag : BoolBaseFlag
    {
        public int Category = -1;
        public int InitValue = 0;
        private bool p_IsRevival = false;
        public BoolFlag() : base() { }
        public BoolFlag(OrderedDictionary<string, dynamic> dict, bool revival = false) : base(dict)
        {
            if (ValidateInFlag(dict))
            {
                if (dict.ContainsKey("Category"))
                {
                    Category = dict["Category"];
                }
                InitValue = dict["InitValue"];
                p_IsRevival = revival;
            }
        }
        private static bool ValidateInFlag(OrderedDictionary<string, dynamic> dict)
        {
            try
            {
                int c = dict["Category"];
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
                BoolFlag otherBool = (BoolFlag)other;
                if (Category == otherBool.Category &&
                    InitValue == otherBool.InitValue)
                {
                    return true;
                }
            }
            return false;
        }
        public new OrderedDictionary<string, dynamic> ToByml()
        {
            OrderedDictionary<string, dynamic> byml = base.ToByml();
            byml["Category"] = Category;
            byml["InitValue"] = InitValue;
            return byml;
        }
        public void OverrideParams(Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides)
        {
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_OVERRIDES"]["CATEGORY"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    Category = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_OVERRIDES"]["INIT_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_OVERRIDES"]["MAX_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_OVERRIDES"]["MIN_VALUE"])
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
