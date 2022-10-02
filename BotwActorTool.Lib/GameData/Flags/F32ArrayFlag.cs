using Nintendo.Byml;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class F32ArrayFlag : F32BaseFlag
    {
        public List<float> InitValue = new() { 0.0f };

        public F32ArrayFlag() : base() { MaxValue = 360.0f; MinValue = -1.0f; }
        public F32ArrayFlag(BymlNode dict) : base(dict)
        {
            InitValue.Clear();
            foreach (BymlNode v in dict.Hash["InitValue"].Array) {
                InitValue.Add(v.Float);
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (!base.Equals(other)) {
                return false;
            }
            F32ArrayFlag otherF32Array = (F32ArrayFlag)other;
            if (InitValue.Count != otherF32Array.InitValue.Count) {
                return false;
            }
            return InitValue.TrueForAll(i => InitValue.IndexOf(i) == otherF32Array.InitValue.IndexOf(i));
        }

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["InitValue"] = new BymlNode(new List<BymlNode>());
            foreach (float f in InitValue)
            {
                byml.Hash["InitValue"].Array.Add(new BymlNode(f));
            }
            return byml;
        }

        public void OverrideParams()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides = Resource.GetOverrides();
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["F32_ARRAY_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["F32_ARRAY_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["F32_ARRAY_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = pair.Value;
                }
            }
        }
    }
}
