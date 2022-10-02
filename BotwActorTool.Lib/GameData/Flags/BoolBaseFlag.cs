using Nintendo.Byml;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class BoolBaseFlag : BaseFlag
    {
        public bool MaxValue = true;
        public bool MinValue = false;

        public BoolBaseFlag() : base() { }
        public BoolBaseFlag(BymlNode dict) : base(dict)
        {
            MaxValue = dict.Hash["MaxValue"].Bool;
            MinValue = dict.Hash["MinValue"].Bool;
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

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["MaxValue"] = new BymlNode(MaxValue);
            byml.Hash["MinValue"] = new BymlNode(MinValue);
            return byml;
        }
    }
}
