using Nintendo.Byml;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class StringArrayFlag : StringBaseFlag
    {
        public List<string> InitValue = new();
        public StringArrayFlag() : base() { }

        public StringArrayFlag(BymlNode dict) : base(dict)
        {
            foreach (BymlNode v in dict.Hash["InitValue"].Array[0].Hash["Values"].Array) {
                InitValue.Add(v.String);
            }
        }

        public new bool Equals(BaseFlag other)
        {
            if (!base.Equals(other)) {
                return false;
            }
            StringArrayFlag otherStringArray = (StringArrayFlag)other;
            if (InitValue.Count != otherStringArray.InitValue.Count) {
                return false;
            }
            return InitValue.TrueForAll(i => InitValue.IndexOf(i) == otherStringArray.InitValue.IndexOf(i));
        }

        public new BymlNode ToByml()
        {
            BymlNode byml = base.ToByml();
            byml.Hash["InitValue"] = new BymlNode(new List<BymlNode>());
            byml.Hash["InitValue"].Array.Add(new BymlNode(new Dictionary<string, BymlNode>()));
            byml.Hash["InitValue"].Array[0].Hash["Values"] = new BymlNode(InitValue);
            return byml;
        }
    }
}
