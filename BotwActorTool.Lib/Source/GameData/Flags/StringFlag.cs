#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace BotwActorTool.Lib.Gamedata.Flags
{
    abstract class StringFlag : StringBaseFlag
    {
        public string InitValue;

        public StringFlag() : base() { }
        public StringFlag(SortedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict)) {
                InitValue = dict["InitValue"];
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                string iv = dict["InitValue"];
                return true;
            }
            catch {
                return false;
            }
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
        public new SortedDictionary<string, dynamic> ToByml()
        {
            SortedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = InitValue;
            return byml;
        }
    }
}
