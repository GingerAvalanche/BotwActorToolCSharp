using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class BoolFlag : BoolBaseFlag
    {
        private bool _isRevival = false;

        public int Category = -1;
        public int InitValue = 0;

        public BoolFlag() : base() { }
        public BoolFlag(SortedDictionary<string, dynamic> dict, bool revival = false) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                if (dict.ContainsKey("Category")) {
                    Category = dict["Category"];
                }
                InitValue = dict["InitValue"];
                _isRevival = revival;
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                int c = dict["Category"];
                int iv = dict["InitValue"];
                return true;
            }
            catch {
                return false;
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                BoolFlag otherBool = (BoolFlag)other;
                if (Category == otherBool.Category &&
                    InitValue == otherBool.InitValue) {
                    return true;
                }
            }
            return false;
        }

        public new SortedDictionary<string, dynamic> ToByml()
        {
            SortedDictionary<string, dynamic> byml = base.ToByml();
            byml["Category"] = Category;
            byml["InitValue"] = InitValue;
            return byml;
        }

        public void OverrideParams(Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides)
        {
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_OVERRIDES"]["CATEGORY"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    Category = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = pair.Value;
                }
            }
        }

        public new bool IsRevival { get => _isRevival; }
    }
}
