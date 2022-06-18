using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class String32Flag : StringFlag
    {
        public String32Flag() : base() { }
        public String32Flag(SortedDictionary<string, dynamic> dict) : base(dict) { }

        public void OverrideParams(Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides)
        {
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["STR32_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["STR32_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["STR32_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = pair.Value;
                }
            }
        }
    }
}
