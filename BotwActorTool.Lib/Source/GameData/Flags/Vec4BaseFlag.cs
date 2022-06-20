using Syroot.Maths;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class Vec4BaseFlag : BaseFlag
    {
        public Vector4F MaxValue;
        public Vector4F MinValue;

        public Vec4BaseFlag() : base() { }
        public Vec4BaseFlag(Dictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                MaxValue = new Vector4F(dict["MaxValue"][0][0], dict["MaxValue"][0][1], dict["MaxValue"][0][2], dict["MaxValue"][0][3]);
                MinValue = new Vector4F(dict["MinValue"][0][0], dict["MinValue"][0][1], dict["MinValue"][0][2], dict["MinValue"][0][3]);
            }
        }

        private static bool ValidateInFlag(Dictionary<string, dynamic> dict)
        {
            try {
                float maxvx = dict["MaxValue"][0][0];
                float maxvy = dict["MaxValue"][0][1];
                float maxvz = dict["MaxValue"][0][2];
                float maxvw = dict["MaxValue"][0][3];
                float minvx = dict["MinValue"][0][0];
                float minvy = dict["MinValue"][0][1];
                float minvz = dict["MinValue"][0][2];
                float minvw = dict["MinValue"][0][3];
                return true;
            }
            catch {
                return false;
            }
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

        public new Dictionary<string, dynamic> ToByml()
        {
            Dictionary<string, dynamic> byml = base.ToByml();
            byml["MaxValue"] = new List<List<int>>(1);
            byml["MaxValue"][0] = new List<int>(4);
            byml["MaxValue"][0][0] = MaxValue.X;
            byml["MaxValue"][0][1] = MaxValue.Y;
            byml["MaxValue"][0][2] = MaxValue.Z;
            byml["MaxValue"][0][3] = MaxValue.W;
            byml["MinValue"] = new List<List<int>>(1);
            byml["MinValue"][0] = new List<int>(4);
            byml["MinValue"][0][0] = MinValue.X;
            byml["MinValue"][0][1] = MinValue.Y;
            byml["MinValue"][0][2] = MinValue.Z;
            byml["MinValue"][0][3] = MinValue.W;
            return byml;
        }
    }
}
