using Nintendo.Byml;
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

        public Vec3ArrayFlag(BymlNode dict) : base(dict)
        {
            InitValue.Clear();
            foreach (BymlNode v in dict.Hash["InitValue"].Array[0].Hash["Values"].Array[0].Array) {
                InitValue.Add(new Vector3F(
                    v.Array[0].Float,
                    v.Array[1].Float,
                    v.Array[2].Float
                    ));
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

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["InitValue"] = new BymlNode(new List<BymlNode>());
            byml.Hash["InitValue"].Array.Add(new BymlNode(new Dictionary<string, BymlNode>()));
            byml.Hash["InitValue"].Array[0].Hash["Values"] = new BymlNode(new List<BymlNode>());
            byml.Hash["InitValue"].Array[0].Hash["Values"].Array.Add(new BymlNode(new List<BymlNode>()));
            for (int i = 0; i < InitValue.Count; i++) {
                byml.Hash["InitValue"].Array[0].Hash["Values"].Array[0].Array.Add(new BymlNode(new List<BymlNode>()));
                byml.Hash["InitValue"].Array[0].Hash["Values"].Array[0].Array[i].Array.Add(new BymlNode(InitValue[i].X));
                byml.Hash["InitValue"].Array[0].Hash["Values"].Array[0].Array[i].Array.Add(new BymlNode(InitValue[i].Y));
                byml.Hash["InitValue"].Array[0].Hash["Values"].Array[0].Array[i].Array.Add(new BymlNode(InitValue[i].Z));
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
