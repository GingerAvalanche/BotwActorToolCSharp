﻿using ByamlExt.Byaml;

namespace BotwActorToolLib.Gamedata.Flags
{
    abstract class S32BaseFlag : BaseFlag
    {
        public int MaxValue;
        public int MinValue;
        public S32BaseFlag() : base() { }
        public S32BaseFlag(OrderedDictionary<string, dynamic> dict) : base(dict)
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
                int maxv = dict["MaxValue"];
                int minv = dict["MinValue"];
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
                S32BaseFlag otherS32Base = (S32BaseFlag)other;
                if (MaxValue == otherS32Base.MaxValue &&
                    MinValue == otherS32Base.MinValue)
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
