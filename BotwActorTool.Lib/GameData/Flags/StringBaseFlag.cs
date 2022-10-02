using Nintendo.Byml;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class StringBaseFlag : BaseFlag
    {
        public string MaxValue = "";
        public string MinValue = "";

        public StringBaseFlag() : base() { }
        public StringBaseFlag(BymlNode dict) : base(dict)
        {
            MaxValue = dict.Hash["MaxValue"].String;
            MinValue = dict.Hash["MinValue"].String;
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

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["MaxValue"] = new BymlNode(MaxValue);
            byml.Hash["MinValue"] = new BymlNode(MinValue);
            return byml;
        }
    }
}
