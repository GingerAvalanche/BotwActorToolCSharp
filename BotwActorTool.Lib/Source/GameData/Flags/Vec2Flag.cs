using Syroot.Maths;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class Vec2Flag : Vec2BaseFlag
    {
        public Vector2F InitValue = new Vector2F(0.0f, 0.0f);

        public Vec2Flag() : base()
        {
            MaxValue = new Vector2F(255.0f, 255.0f);
            MinValue = new Vector2F(0.0f, 0.0f);
        }

        public Vec2Flag(SortedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                InitValue = new Vector2F(dict["InitValue"][0][0], dict["InitValue"][0][1]);
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                float ivx = dict["InitValue"][0][0];
                float ivy = dict["InitValue"][0][1];
                return true;
            }
            catch {
                return false;
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                Vec2Flag otherVec2 = (Vec2Flag)other;
                if (InitValue == otherVec2.InitValue) {
                    return true;
                }
            }
            return false;
        }

        public new SortedDictionary<string, dynamic> ToByml()
        {
            SortedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = new List<List<int>>(1);
            byml["InitValue"][0] = new List<int>(2);
            byml["InitValue"][0][0] = InitValue.X;
            byml["InitValue"][0][1] = InitValue.Y;
            return byml;
        }

        public void OverrideParams()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides = Resource.GetOverrides();
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC2_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = new Vector2F(pair.Value[0], pair.Value[1]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC2_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = new Vector2F(pair.Value[0], pair.Value[1]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC2_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = new Vector2F(pair.Value[0], pair.Value[1]);
                }
            }
        }
    }
}
