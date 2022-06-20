namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class F32BaseFlag : BaseFlag
    {
        public float MaxValue;
        public float MinValue;

        public F32BaseFlag() : base() { }
        public F32BaseFlag(Dictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                MaxValue = dict["MaxValue"];
                MinValue = dict["MinValue"];
            }
        }

        private static bool ValidateInFlag(Dictionary<string, dynamic> dict)
        {
            try {
                float maxv = dict["MaxValue"];
                float minv = dict["MinValue"];
                return true;
            }
            catch {
                return false;
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                F32BaseFlag otherF32Base = (F32BaseFlag)other;
                if (MaxValue == otherF32Base.MaxValue &&
                    MinValue == otherF32Base.MinValue) {
                    return true;
                }
            }
            return false;
        }

        public new Dictionary<string, dynamic> ToByml()
        {
            Dictionary<string, dynamic> byml = base.ToByml();
            byml["MaxValue"] = MaxValue;
            byml["MinValue"] = MinValue;
            return byml;
        }
    }
}
