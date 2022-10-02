using Nintendo.Byml;
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

        public Vec2Flag(BymlNode dict) : base(dict)
        {
            InitValue = new Vector2F(
                dict.Hash["InitValue"].Array[0].Array[0].Float,
                dict.Hash["InitValue"].Array[0].Array[1].Float
                );
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

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["InitValue"] = new BymlNode(new List<BymlNode>());
            byml.Hash["InitValue"].Array.Add(new BymlNode(new List<BymlNode>()));
            byml.Hash["InitValue"].Array[0].Array.Add(new BymlNode(InitValue.X));
            byml.Hash["InitValue"].Array[0].Array.Add(new BymlNode(InitValue.Y));
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
