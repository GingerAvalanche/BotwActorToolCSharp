using Nintendo.Byml;
using Syroot.Maths;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class Vec2BaseFlag : BaseFlag
    {
        public Vector2F MaxValue;
        public Vector2F MinValue;

        public Vec2BaseFlag() : base() { }
        public Vec2BaseFlag(BymlNode dict) : base(dict)
        {
            MaxValue = new Vector2F(
                dict.Hash["MaxValue"].Array[0].Array[0].Float,
                dict.Hash["MaxValue"].Array[0].Array[1].Float
                );
            MinValue = new Vector2F(
                dict.Hash["MinValue"].Array[0].Array[0].Float,
                dict.Hash["MinValue"].Array[0].Array[1].Float
                );
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                Vec2BaseFlag otherVec2Base = (Vec2BaseFlag)other;
                if (MaxValue == otherVec2Base.MaxValue &&
                    MinValue == otherVec2Base.MinValue) {
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
            byml.Hash["MinValue"] = new BymlNode(new List<BymlNode>());
            byml.Hash["MinValue"].Array.Add(new BymlNode(new List<BymlNode>()));
            byml.Hash["MinValue"].Array[0].Array.Add(new BymlNode(MinValue.X));
            byml.Hash["MinValue"].Array[0].Array.Add(new BymlNode(MinValue.Y));
            return byml;
        }
    }
}
