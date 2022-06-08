﻿namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class BoolBaseFlag : BaseFlag
    {
        public bool MaxValue = true;
        public bool MinValue = false;

        public BoolBaseFlag() : base() { }
        public BoolBaseFlag(SortedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                MaxValue = dict["MaxValue"];
                MinValue = dict["MinValue"];
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                bool maxv = dict["MaxValue"];
                bool minv = dict["MinValue"];
                return true;
            }
            catch {
                return false;
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                BoolBaseFlag otherBoolBase = (BoolBaseFlag)other;
                if (MaxValue == otherBoolBase.MaxValue &&
                    MinValue == otherBoolBase.MinValue) {
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
