using Syroot.Maths;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class Vec3ArrayFlag : Vec3BaseFlag
    {
        public List<Vector3F> InitValue = new() { new Vector3F(0.0f, 0.0f, 0.0f) };
        public Vec3ArrayFlag() : base()
        {
            MaxValue = new Vector3F(255.0f, 255.0f, 255.0f);
            MinValue = new Vector3F(0.0f, 0.0f, 0.0f);
        }

        public Vec3ArrayFlag(Dictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                InitValue.Clear();
                foreach (List<List<float>> v in dict["InitValue"][0]["Values"]) {
                    InitValue.Add(new Vector3F(v[0][0], v[0][1], v[0][2]));
                }
            }
        }

        private static bool ValidateInFlag(Dictionary<string, dynamic> dict)
        {
            try {
                List<Dictionary<string, List<List<float>>>> iv = dict["InitValue"];
                foreach (List<List<float>> v in dict["InitValue"]) {
                    float ivx = v[0][0];
                    float ivy = v[0][1];
                    float ivz = v[0][2];
                }
                return true;
            }
            catch {
                return false;
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                Vec3ArrayFlag otherVec3Array = (Vec3ArrayFlag)other;
                if (InitValue == otherVec3Array.InitValue) {
                    return true;
                }
            }
            return false;
        }

        public new Dictionary<string, dynamic> ToByml()
        {
            Dictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = new List<Dictionary<string, List<List<float>>>>(1);
            byml["InitValue"][0] = new Dictionary<string, List<List<float>>>();
            byml["InitValue"][0]["Values"] = new List<List<float>>(InitValue.Count);
            for (int i = 0; i < InitValue.Count; i++) {
                byml["InitValue"][0]["Values"][i] = new List<float> { InitValue[i].X, InitValue[i].Y, InitValue[i].Z };
            }
            return byml;
        }

        public void OverrideParams()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides = Resource.GetOverrides();
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC3_ARRAY_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue.Clear();
                    for (int i = 0; i < pair.Value.Count; i++) {
                        InitValue.Add(new Vector3F(pair.Value[0], pair.Value[1], pair.Value[2]));
                    }
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC3_ARRAY_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = new Vector3F(pair.Value[0], pair.Value[1], pair.Value[2]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC3_ARRAY_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = new Vector3F(pair.Value[0], pair.Value[1], pair.Value[2]);
                }
            }
        }
    }
}
