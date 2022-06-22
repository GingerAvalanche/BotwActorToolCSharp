using Nintendo.Byml;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class S32ArrayFlag : S32BaseFlag
    {
        public List<int> InitValue = new() { 0 };

        public S32ArrayFlag() : base() { MaxValue = 6553500; MinValue = -1; }
        public S32ArrayFlag(BymlNode dict) : base(dict)
        {
            InitValue.Clear();
            foreach (BymlNode v in dict.Hash["InitValue"].Array) {
                InitValue.Add(v.Int);
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (!base.Equals(other)) {
                return false;
            }
            S32ArrayFlag otherS32Array = (S32ArrayFlag)other;
            if (InitValue.Count != otherS32Array.InitValue.Count) {
                return false;
            }
            return InitValue.TrueForAll(i => InitValue.IndexOf(i) == otherS32Array.InitValue.IndexOf(i));
        }

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["InitValue"] = new BymlNode(new List<BymlNode>());
            foreach (int i in InitValue)
            {
                byml.Hash["InitValue"].Array.Add(new BymlNode(i));
            }
            return byml;
        }
        public void OverrideParams()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides = Resource.GetOverrides();
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_ARRAY_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_ARRAY_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_ARRAY_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = pair.Value;
                }
            }
        }
    }
}
