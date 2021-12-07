using ByamlExt.Byaml;

namespace BotwActorToolLib.Gamedata.Flags
{
    abstract class StringFlag : StringBaseFlag
    {
        public string InitValue;
        public StringFlag() : base() { }
        public StringFlag(OrderedDictionary<string, dynamic> dict) : base(dict)
        {
            if (ValidateInFlag(dict))
            {
                InitValue = dict["InitValue"];
            }
        }
        private static bool ValidateInFlag(OrderedDictionary<string, dynamic> dict)
        {
            try
            {
                string iv = dict["InitValue"];
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
                StringFlag otherString = (StringFlag)other;
                if (InitValue == otherString.InitValue)
                {
                    return true;
                }
            }
            return false;
        }
        public new OrderedDictionary<string, dynamic> ToByml()
        {
            OrderedDictionary<string, dynamic> byml = base.ToByml();
            byml["InitValue"] = InitValue;
            return byml;
        }
    }
}
