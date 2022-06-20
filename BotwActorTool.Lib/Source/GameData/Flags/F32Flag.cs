using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class F32Flag : F32BaseFlag
    {
        public float InitValue = 0.0f;

        public F32Flag() : base() { MaxValue = 1000000.0f; MinValue = 0.0f; }
        public F32Flag(SortedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                InitValue = dict["InitValue"];
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                float iv = dict["InitValue"];
                return true;
            }
            catch {
                return false;
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                F32Flag otherF32 = (F32Flag)other;
                if (InitValue == otherF32.InitValue) {
                    return true;
                }
            }
            return false;
        }

        public new SortedDictionary<string, dynamic> ToByml()
        {
            SortedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = InitValue;
            return byml;
        }

        public void OverrideParams()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides = Resource.GetOverrides();
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["F32_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["F32_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["F32_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = pair.Value;
                }
            }
        }
    }
}
