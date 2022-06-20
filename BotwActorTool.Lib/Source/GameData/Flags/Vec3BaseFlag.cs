using Syroot.Maths;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class Vec3BaseFlag : BaseFlag
    {
        public Vector3F MaxValue;
        public Vector3F MinValue;

        public Vec3BaseFlag() : base() { }
        public Vec3BaseFlag(Dictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                MaxValue = new Vector3F(dict["MaxValue"][0][0], dict["MaxValue"][0][1], dict["MaxValue"][0][2]);
                MinValue = new Vector3F(dict["MinValue"][0][0], dict["MinValue"][0][1], dict["MinValue"][0][2]);
            }
        }

        private static bool ValidateInFlag(Dictionary<string, dynamic> dict)
        {
            try {
                float maxvx = dict["MaxValue"][0][0];
                float maxvy = dict["MaxValue"][0][1];
                float maxvz = dict["MaxValue"][0][2];
                float minvx = dict["MinValue"][0][0];
                float minvy = dict["MinValue"][0][1];
                float minvz = dict["MinValue"][0][2];
                return true;
            }
            catch {
                return false;
            }
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

        public new Dictionary<string, dynamic> ToByml()
        {
            Dictionary<string, dynamic> byml = base.ToByml();
            byml["MaxValue"] = new List<List<int>>(1);
            byml["MaxValue"][0] = new List<int>(3);
            byml["MaxValue"][0][0] = MaxValue.X;
            byml["MaxValue"][0][1] = MaxValue.Y;
            byml["MaxValue"][0][2] = MaxValue.Z;
            byml["MinValue"] = new List<List<int>>(1);
            byml["MinValue"][0] = new List<int>(3);
            byml["MinValue"][0][0] = MinValue.X;
            byml["MinValue"][0][1] = MinValue.Y;
            byml["MinValue"][0][2] = MinValue.Z;
            return byml;
        }
    }
}
