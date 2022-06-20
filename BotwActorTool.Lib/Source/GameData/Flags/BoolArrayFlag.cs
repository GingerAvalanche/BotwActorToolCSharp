using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class BoolArrayFlag : BoolBaseFlag
    {
        public List<int> InitValue = new() { 0 };

        public BoolArrayFlag() : base() { }
        public BoolArrayFlag(SortedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                foreach (int v in dict["InitValue"]) {
                    InitValue.Add(v);
                }
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                List<int> iv = dict["InitValue"];
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
            BoolArrayFlag otherBoolArray = (BoolArrayFlag)other;
            if (InitValue.Count != otherBoolArray.InitValue.Count) {
                return false;
            }
            return InitValue.TrueForAll(i => InitValue.IndexOf(i) == otherBoolArray.InitValue.IndexOf(i));
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
