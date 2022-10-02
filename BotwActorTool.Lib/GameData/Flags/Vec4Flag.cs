using Nintendo.Byml;
using Syroot.Maths;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class Vec4Flag : Vec4BaseFlag
    {
        public Vector4F InitValue = new(0.0f, 0.0f, 0.0f, 0.0f);

        public Vec4Flag() : base()
        {
            MaxValue = new Vector4F(255.0f, 255.0f, 255.0f, 255.0f);
            MinValue = new Vector4F(0.0f, 0.0f, 0.0f, 0.0f);
        }

        public Vec4Flag(BymlNode dict) : base(dict)
        {
            InitValue = new Vector4F(
                dict.Hash["InitValue"].Array[0].Array[0].Float,
                dict.Hash["InitValue"].Array[0].Array[1].Float,
                dict.Hash["InitValue"].Array[0].Array[2].Float,
                dict.Hash["InitValue"].Array[0].Array[3].Float
                );
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

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["InitValue"] = new BymlNode(new List<BymlNode>());
            byml.Hash["InitValue"].Array.Add(new BymlNode(new List<BymlNode>()));
            byml.Hash["InitValue"].Array[0].Array.Add(new BymlNode(InitValue.X));
            byml.Hash["InitValue"].Array[0].Array.Add(new BymlNode(InitValue.Y));
            byml.Hash["InitValue"].Array[0].Array.Add(new BymlNode(InitValue.Z));
            byml.Hash["InitValue"].Array[0].Array.Add(new BymlNode(InitValue.W));
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
