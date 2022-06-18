using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class F32ArrayFlag : F32BaseFlag
    {
        public List<float> InitValue = new() { 0.0f };

        public F32ArrayFlag() : base() { MaxValue = 360.0f; MinValue = -1.0f; }
        public F32ArrayFlag(SortedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                foreach (float v in dict["InitValue"]) {
                    InitValue.Add(v);
                }
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                List<float> iv = dict["InitValue"];
                return true;
            }
            catch {
                return false;
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

        public new SortedDictionary<string, dynamic> ToByml()
        {
            SortedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = InitValue;
            return byml;
        }

        public void OverrideParams(Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides)
        {
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
