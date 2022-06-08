using ByamlExt.Byaml;

namespace BotwActorToolLib.Gamedata.Flags
{
    abstract class F32BaseFlag : BaseFlag
    {
        public float MaxValue;
        public float MinValue;
        public F32BaseFlag() : base() { }
        public F32BaseFlag(OrderedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict))
            {
                MaxValue = dict["MaxValue"];
                MinValue = dict["MinValue"];
            }
        }
        private static bool ValidateInFlag(OrderedDictionary<string, dynamic> dict)
        {
            try
            {
                float maxv = dict["MaxValue"];
                float minv = dict["MinValue"];
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
                F32BaseFlag otherF32Base = (F32BaseFlag)other;
                if (MaxValue == otherF32Base.MaxValue &&
                    MinValue == otherF32Base.MinValue)
                {
                    return true;
                }
            }
            return false;
        }
        public new OrderedDictionary<string, dynamic> ToByml()
        {
            OrderedDictionary<string, dynamic> byml = base.ToByml();
            byml["MaxValue"] = MaxValue;
            byml["MinValue"] = MinValue;
            return byml;
        }
    }
}
