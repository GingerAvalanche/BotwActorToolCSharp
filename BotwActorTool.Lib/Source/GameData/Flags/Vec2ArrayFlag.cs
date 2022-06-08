using Syroot.Maths;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class Vec2ArrayFlag : Vec2BaseFlag
    {
        public List<Vector2F> InitValue = new() { new Vector2F(0.0f, 0.0f) };

        public Vec2ArrayFlag() : base()
        {
            MaxValue = new Vector2F(255.0f, 255.0f);
            MinValue = new Vector2F(0.0f, 0.0f);
        }

        public Vec2ArrayFlag(SortedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                InitValue.Clear();
                foreach (List<List<float>> v in dict["InitValue"][0]["Values"]) {
                    InitValue.Add(new Vector2F(v[0][0], v[0][1]));
                }
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                List<Dictionary<string, List<List<float>>>> iv = dict["InitValue"];
                foreach (List<List<float>> v in dict["InitValue"]) {
                    float ivx = v[0][0];
                    float ivy = v[0][1];
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
                Vec2ArrayFlag otherVec2Array = (Vec2ArrayFlag)other;
                if (InitValue == otherVec2Array.InitValue) {
                    return true;
                }
            }
            return false;
        }

        public new SortedDictionary<string, dynamic> ToByml()
        {
            SortedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = new List<Dictionary<string, List<List<float>>>>(1);
            byml["InitValue"][0] = new Dictionary<string, List<List<float>>>();
            byml["InitValue"][0]["Values"] = new List<List<float>>(InitValue.Count);
            for (int i = 0; i < InitValue.Count; i++) {
                byml["InitValue"][0]["Values"][i] = new List<float> { InitValue[i].X, InitValue[i].Y };
            }
            return byml;
        }

        public void OverrideParams(Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides)
        {
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC2_ARRAY_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue.Clear();
                    for (int i = 0; i < pair.Value.Count; i++) {
                        InitValue.Add(new Vector2F(pair.Value[0], pair.Value[1]));
                    }
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC2_ARRAY_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = new Vector2F(pair.Value[0], pair.Value[1]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC2_ARRAY_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = new Vector2F(pair.Value[0], pair.Value[1]);
                }
            }
        }
    }
}
