using Nintendo.Byml;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class StringFlag : StringBaseFlag
    {
        public string InitValue = "";

        public StringFlag() : base() { }
        public StringFlag(BymlNode dict) : base(dict)
        {
            InitValue = dict.Hash["InitValue"].String;
        }

        public new bool Equals(BaseFlag other)
        {
            if (base.Equals(other)) {
                StringFlag otherString = (StringFlag)other;
                if (InitValue == otherString.InitValue) {
                    return true;
                }
            }
            return false;
        }
        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["InitValue"] = new BymlNode(InitValue);
            return byml;
        }
    }
}
