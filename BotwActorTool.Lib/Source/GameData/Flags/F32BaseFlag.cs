using Nintendo.Byml;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class F32BaseFlag : BaseFlag
    {
        public float MaxValue;
        public float MinValue;

        public F32BaseFlag() : base() { }
        public F32BaseFlag(BymlNode dict) : base(dict)
        {
            MaxValue = dict.Hash["MaxValue"].Float;
            MinValue = dict.Hash["MinValue"].Float;
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

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["MaxValue"] = new BymlNode(MaxValue);
            byml.Hash["MinValue"] = new BymlNode(MinValue);
            return byml;
        }
    }
}
