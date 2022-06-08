using ByamlExt.Byaml;

namespace BotwActorToolLib.Gamedata.Flags
{
    abstract class StringBaseFlag : BaseFlag
    {
        public string MaxValue;
        public string MinValue;
        public StringBaseFlag() : base() { }
        public StringBaseFlag(OrderedDictionary<string, dynamic> dict) : base(dict)
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
                string maxv = dict["MaxValue"];
                string minv = dict["MinValue"];
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
                StringBaseFlag otherStringBase = (StringBaseFlag)other;
                if (MaxValue == otherStringBase.MaxValue &&
                    MinValue == otherStringBase.MinValue)
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
