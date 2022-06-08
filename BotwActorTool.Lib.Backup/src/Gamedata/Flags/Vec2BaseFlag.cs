using ByamlExt.Byaml;
using Syroot.Maths;
using System.Collections.Generic;

namespace BotwActorToolLib.Gamedata.Flags
{
    abstract class Vec2BaseFlag : BaseFlag
    {
        public Vector2F MaxValue;
        public Vector2F MinValue;
        public Vec2BaseFlag() : base() { }
        public Vec2BaseFlag(OrderedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict))
            {
                MaxValue = new Vector2F(dict["MaxValue"][0][0], dict["MaxValue"][0][1]);
                MinValue = new Vector2F(dict["MinValue"][0][0], dict["MinValue"][0][1]);
            }
        }
        private static bool ValidateInFlag(OrderedDictionary<string, dynamic> dict)
        {
            try
            {
                float maxvx = dict["MaxValue"][0][0];
                float maxvy = dict["MaxValue"][0][1];
                float minvx = dict["MinValue"][0][0];
                float minvy = dict["MinValue"][0][1];
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
                Vec2BaseFlag otherVec2Base = (Vec2BaseFlag)other;
                if (MaxValue == otherVec2Base.MaxValue &&
                    MinValue == otherVec2Base.MinValue)
                {
                    return true;
                }
            }
            return false;
        }
        public new OrderedDictionary<string, dynamic> ToByml()
        {
            OrderedDictionary<string, dynamic> byml = base.ToByml();
            byml["MaxValue"] = new List<List<int>>(1);
            byml["MaxValue"][0] = new List<int>(2);
            byml["MaxValue"][0][0] = MaxValue.X;
            byml["MaxValue"][0][1] = MaxValue.Y;
            byml["MinValue"] = new List<List<int>>(1);
            byml["MinValue"][0] = new List<int>(2);
            byml["MinValue"][0][0] = MinValue.X;
            byml["MinValue"][0][1] = MinValue.Y;
            return byml;
        }
    }
}
