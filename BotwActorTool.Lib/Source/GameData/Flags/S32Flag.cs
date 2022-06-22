using Nintendo.Byml;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class S32Flag : S32BaseFlag
    {
        private bool _isRevival = false;

        public int InitValue = 0;

        public S32Flag() : base() { MaxValue = 2147483647; MinValue = 0; }
        public S32Flag(BymlNode dict, bool revival = false) : base(dict)
        {
            InitValue = dict.Hash["InitValue"].Int;
            _isRevival = revival;
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                S32Flag otherS32 = (S32Flag)other;
                if (InitValue == otherS32.InitValue) {
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
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["S32_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = pair.Value;
                }
            }
        }
        public new bool IsRevival { get => _isRevival; }
    }
}
