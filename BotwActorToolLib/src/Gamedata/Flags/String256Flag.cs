using ByamlExt.Byaml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BotwActorToolLib.Gamedata.Flags
{
    class String256Flag : StringFlag
    {
        public String256Flag() : base() { }
        public String256Flag(OrderedDictionary<string, dynamic> dict) : base(dict) { }
        public void OverrideParams(Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides)
        {
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["STR256_OVERRIDES"]["INIT_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["STR256_OVERRIDES"]["MAX_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["STR256_OVERRIDES"]["MIN_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    MinValue = pair.Value;
                }
            }
        }
    }
}
