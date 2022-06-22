using Nintendo.Byml;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class BoolFlag : BoolBaseFlag
    {
        private bool _isRevival = false;

        public int Category = -1;
        public int InitValue = 0;

        public BoolFlag() : base() { }
        public BoolFlag(BymlNode dict, bool revival = false) : base(dict)
        {
            if (dict.Hash.ContainsKey("Category")) {
                Category = dict.Hash["Category"].Int;
            }
            InitValue = dict.Hash["InitValue"].Int;
            _isRevival = revival;
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

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["Category"] = new BymlNode(Category);
            byml.Hash["InitValue"] = new BymlNode(InitValue);
            return byml;
        }

        public void OverrideParams()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides = Resource.GetOverrides();
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
