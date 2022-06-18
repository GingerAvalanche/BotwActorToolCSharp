#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class StringBaseFlag : BaseFlag
    {
        public string MaxValue;
        public string MinValue;

        public StringBaseFlag() : base() { }
        public StringBaseFlag(SortedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                MaxValue = dict["MaxValue"];
                MinValue = dict["MinValue"];
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                string maxv = dict["MaxValue"];
                string minv = dict["MinValue"];
                return true;
            }
            catch {
                return false;
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                StringBaseFlag otherStringBase = (StringBaseFlag)other;
                if (MaxValue == otherStringBase.MaxValue &&
                    MinValue == otherStringBase.MinValue) {
                    return true;
                }
            }
            return false;
        }

        public new SortedDictionary<string, dynamic> ToByml()
        {
            SortedDictionary<string, dynamic> byml = base.ToByml();
            byml["MaxValue"] = MaxValue;
            byml["MinValue"] = MinValue;
            return byml;
        }
    }
}
