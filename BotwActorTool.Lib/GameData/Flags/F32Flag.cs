using Nintendo.Byml;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class F32Flag : F32BaseFlag
    {
        public float InitValue = 0.0f;

        public F32Flag() : base() { MaxValue = 1000000.0f; MinValue = 0.0f; }
        public F32Flag(BymlNode dict) : base(dict)
        {
            InitValue = dict.Hash["InitValue"].Float;
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

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["InitValue"] = new BymlNode(InitValue);
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
