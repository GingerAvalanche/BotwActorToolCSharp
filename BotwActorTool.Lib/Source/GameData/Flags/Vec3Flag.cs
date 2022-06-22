using Nintendo.Byml;
using Syroot.Maths;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    class Vec3Flag : Vec3BaseFlag
    {
        public Vector3F InitValue = new Vector3F(0.0f, 0.0f, 0.0f);

        public Vec3Flag() : base()
        {
            MaxValue = new Vector3F(100000.0f, 100000.0f, 100000.0f);
            MinValue = new Vector3F(-100000.0f, -100000.0f, -100000.0f);
        }
        public Vec3Flag(BymlNode dict) : base(dict)
        {
            InitValue = new Vector3F(
                dict.Hash["InitValue"].Array[0].Array[0].Float,
                dict.Hash["InitValue"].Array[0].Array[1].Float,
                dict.Hash["InitValue"].Array[0].Array[2].Float
                );
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                Vec3Flag otherVec3 = (Vec3Flag)other;
                if (InitValue == otherVec3.InitValue) {
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
            return byml;
        }

        public void OverrideParams()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides = Resource.GetOverrides();
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC3_OVERRIDES"]["INIT_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    InitValue = new Vector3F(pair.Value[0], pair.Value[1], pair.Value[2]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC3_OVERRIDES"]["MAX_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MaxValue = new Vector3F(pair.Value[0], pair.Value[1], pair.Value[2]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC3_OVERRIDES"]["MIN_VALUE"]) {
                if (new Regex(pair.Key).Matches(DataName).Count > 0) {
                    MinValue = new Vector3F(pair.Value[0], pair.Value[1], pair.Value[2]);
                }
            }
        }
    }
}
