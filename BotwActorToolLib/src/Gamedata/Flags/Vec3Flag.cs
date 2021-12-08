using ByamlExt.Byaml;
using Syroot.Maths;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BotwActorToolLib.Gamedata.Flags
{
    class Vec3Flag : Vec3BaseFlag
    {
        public Vector3F InitValue = new Vector3F(0.0f, 0.0f, 0.0f);
        public Vec3Flag() : base()
        {
            MaxValue = new Vector3F(100000.0f, 100000.0f, 100000.0f);
            MinValue = new Vector3F(-100000.0f, -100000.0f, -100000.0f);
        }
        public Vec3Flag(OrderedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict))
            {
                InitValue = new Vector3F(dict["InitValue"][0][0], dict["InitValue"][0][1], dict["InitValue"][0][2]);
            }
        }
        private static bool ValidateInFlag(OrderedDictionary<string, dynamic> dict)
        {
            try
            {
                float ivx = dict["InitValue"][0][0];
                float ivy = dict["InitValue"][0][1];
                float ivz = dict["InitValue"][0][2];
                return true;
            }
            catch
            {
                return false;
            }
        }
        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other))
            {
                Vec3Flag otherVec3 = (Vec3Flag)other;
                if (InitValue == otherVec3.InitValue)
                {
                    return true;
                }
            }
            return false;
        }
        public new OrderedDictionary<string, dynamic> ToByml()
        {
            OrderedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = new List<List<int>>(1);
            byml["InitValue"][0] = new List<int>(3);
            byml["InitValue"][0][0] = InitValue.X;
            byml["InitValue"][0][1] = InitValue.Y;
            byml["InitValue"][0][2] = InitValue.Z;
            return byml;
        }
        public void OverrideParams(Dictionary<string, Dictionary<string, Dictionary<string, dynamic>>> overrides)
        {
            OverrideParams(overrides["STANDARD_OVERRIDES"]);
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC3_OVERRIDES"]["INIT_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    InitValue = new Vector3F(pair.Value[0], pair.Value[1], pair.Value[2]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC3_OVERRIDES"]["MAX_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    MaxValue = new Vector3F(pair.Value[0], pair.Value[1], pair.Value[2]);
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["VEC3_OVERRIDES"]["MIN_VALUE"])
            {
                if (new Regex(pair.Key).Matches(DataName).Count > 0)
                {
                    MinValue = new Vector3F(pair.Value[0], pair.Value[1], pair.Value[2]);
                }
            }
        }
    }
}
