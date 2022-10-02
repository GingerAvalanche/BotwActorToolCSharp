using Nintendo.Byml;
using Syroot.Maths;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class Vec3BaseFlag : BaseFlag
    {
        public Vector3F MaxValue;
        public Vector3F MinValue;

        public Vec3BaseFlag() : base() { }
        public Vec3BaseFlag(BymlNode dict) : base(dict)
        {
            MaxValue = new Vector3F(
                dict.Hash["MaxValue"].Array[0].Array[0].Float,
                dict.Hash["MaxValue"].Array[0].Array[1].Float,
                dict.Hash["MaxValue"].Array[0].Array[2].Float
                );
            MinValue = new Vector3F(
                dict.Hash["MinValue"].Array[0].Array[0].Float,
                dict.Hash["MinValue"].Array[0].Array[1].Float,
                dict.Hash["MinValue"].Array[0].Array[2].Float
                );
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                Vec3BaseFlag otherVec3Base = (Vec3BaseFlag)other;
                if (MaxValue == otherVec3Base.MaxValue &&
                    MinValue == otherVec3Base.MinValue) {
                    return true;
                }
            }
            return false;
        }

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["MaxValue"] = new BymlNode(new List<BymlNode>());
            byml.Hash["MaxValue"].Array.Add(new BymlNode(new List<BymlNode>()));
            byml.Hash["MaxValue"].Array[0].Array.Add(new BymlNode(MaxValue.X));
            byml.Hash["MaxValue"].Array[0].Array.Add(new BymlNode(MaxValue.Y));
            byml.Hash["MaxValue"].Array[0].Array.Add(new BymlNode(MaxValue.Z));
            byml.Hash["MinValue"] = new BymlNode(new List<BymlNode>());
            byml.Hash["MinValue"].Array.Add(new BymlNode(new List<BymlNode>()));
            byml.Hash["MinValue"].Array[0].Array.Add(new BymlNode(MinValue.X));
            byml.Hash["MinValue"].Array[0].Array.Add(new BymlNode(MinValue.Y));
            byml.Hash["MinValue"].Array[0].Array.Add(new BymlNode(MinValue.Z));
            return byml;
        }
    }
}
