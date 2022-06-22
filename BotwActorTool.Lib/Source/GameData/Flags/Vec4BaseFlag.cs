using Nintendo.Byml;
using Syroot.Maths;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class Vec4BaseFlag : BaseFlag
    {
        public Vector4F MaxValue;
        public Vector4F MinValue;

        public Vec4BaseFlag() : base() { }
        public Vec4BaseFlag(BymlNode dict) : base(dict)
        {
            MaxValue = new Vector4F(
                dict.Hash["MaxValue"].Array[0].Array[0].Float,
                dict.Hash["MaxValue"].Array[0].Array[1].Float,
                dict.Hash["MaxValue"].Array[0].Array[2].Float,
                dict.Hash["MaxValue"].Array[0].Array[3].Float
                );
            MinValue = new Vector4F(
                dict.Hash["MinValue"].Array[0].Array[0].Float,
                dict.Hash["MinValue"].Array[0].Array[1].Float,
                dict.Hash["MinValue"].Array[0].Array[2].Float,
                dict.Hash["MinValue"].Array[0].Array[3].Float
                );
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                Vec4BaseFlag otherVec4Base = (Vec4BaseFlag)other;
                if (MaxValue == otherVec4Base.MaxValue &&
                    MinValue == otherVec4Base.MinValue) {
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
            byml.Hash["MaxValue"].Array[0].Array[0] = new BymlNode(MaxValue.X);
            byml.Hash["MaxValue"].Array[0].Array[1] = new BymlNode(MaxValue.Y);
            byml.Hash["MaxValue"].Array[0].Array[2] = new BymlNode(MaxValue.Z);
            byml.Hash["MaxValue"].Array[0].Array[3] = new BymlNode(MaxValue.W);
            byml.Hash["MinValue"] = new BymlNode(new List<BymlNode>());
            byml.Hash["MinValue"].Array.Add(new BymlNode(new List<BymlNode>()));
            byml.Hash["MinValue"].Array[0].Array[0] = new BymlNode(MinValue.X);
            byml.Hash["MinValue"].Array[0].Array[1] = new BymlNode(MinValue.Y);
            byml.Hash["MinValue"].Array[0].Array[2] = new BymlNode(MinValue.Z);
            byml.Hash["MinValue"].Array[0].Array[3] = new BymlNode(MinValue.W);
            return byml;
        }
    }
}
