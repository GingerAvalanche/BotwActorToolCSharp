using Syroot.Maths;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class Vec4Flag : Vec4BaseFlag
    {
        public Vector4F InitValue = new Vector4F(0.0f, 0.0f, 0.0f, 0.0f);

        public Vec4Flag() : base()
        {
            MaxValue = new Vector4F(255.0f, 255.0f, 255.0f, 255.0f);
            MinValue = new Vector4F(0.0f, 0.0f, 0.0f, 0.0f);
        }

        public Vec4Flag(SortedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                InitValue = new Vector4F(dict["InitValue"][0][0], dict["InitValue"][0][1], dict["InitValue"][0][2], dict["InitValue"][0][3]);
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                float ivx = dict["InitValue"][0][0];
                float ivy = dict["InitValue"][0][1];
                float ivz = dict["InitValue"][0][2];
                float ivw = dict["InitValue"][0][3];
                return true;
            }
            catch {
                return false;
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                Vec4Flag otherVec4 = (Vec4Flag)other;
                if (InitValue == otherVec4.InitValue) {
                    return true;
                }
            }
            return false;
        }

        public new SortedDictionary<string, dynamic> ToByml()
        {
            SortedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = new List<List<int>>(1);
            byml["InitValue"][0] = new List<int>(4);
            byml["InitValue"][0][0] = InitValue.X;
            byml["InitValue"][0][1] = InitValue.Y;
            byml["InitValue"][0][2] = InitValue.Z;
            byml["InitValue"][0][3] = InitValue.W;
            return byml;
        }

        public void OverrideParams()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides = Resource.GetOverrides();
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC4_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = new Vector4F(pair.Value[0], pair.Value[1], pair.Value[2], pair.Value[3]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC4_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = new Vector4F(pair.Value[0], pair.Value[1], pair.Value[2], pair.Value[3]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC4_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = new Vector4F(pair.Value[0], pair.Value[1], pair.Value[2], pair.Value[3]);
                }
            }
        }
    }
}
