using Nintendo.Byml;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class BoolArrayFlag : BoolBaseFlag
    {
        public List<int> InitValue = new() { 0 };

        public BoolArrayFlag() : base() { }
        public BoolArrayFlag(BymlNode dict) : base(dict)
        {
            foreach (BymlNode v in dict.Hash["InitValue"].Array) {
                InitValue.Add(v.Int);
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (!base.Equals(other)) {
                return false;
            }
            BoolArrayFlag otherBoolArray = (BoolArrayFlag)other;
            if (InitValue.Count != otherBoolArray.InitValue.Count) {
                return false;
            }
            return InitValue.TrueForAll(i => InitValue.IndexOf(i) == otherBoolArray.InitValue.IndexOf(i));
        }

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["InitValue"] = new BymlNode(new List<BymlNode>());
            foreach (int val in InitValue)
            {
                byml.Hash["InitValue"].Array.Add(new BymlNode(val));
            }
            return byml;
        }

        public void OverrideParams()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides = Resource.GetOverrides();
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_ARRAY_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_ARRAY_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["BOOL_ARRAY_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = pair.Value;
                }
            }
        }
    }
}
