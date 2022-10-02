using Nintendo.Byml;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class S32BaseFlag : BaseFlag
    {
        public int MaxValue;
        public int MinValue;

        public S32BaseFlag() : base() { }
        public S32BaseFlag(BymlNode dict) : base(dict)
        {
            MaxValue = dict.Hash["MaxValue"].Int;
            MinValue = dict.Hash["MinValue"].Int;
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                S32BaseFlag otherS32Base = (S32BaseFlag)other;
                if (MaxValue == otherS32Base.MaxValue &&
                    MinValue == otherS32Base.MinValue) {
                    return true;
                }
            }
            return false;
        }

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["MaxValue"] = new BymlNode(MaxValue);
            byml.Hash["MinValue"] = new BymlNode(MinValue);
            return byml;
        }
    }
}
