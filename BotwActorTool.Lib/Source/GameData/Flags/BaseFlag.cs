using Aamp.Security.Cryptography;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BotwActorTool.Lib.Gamedata.Flags
{
    public abstract class BaseFlag
    {
        private string _dataName = "";
        private int _hashValue;

        public int DeleteRev = -1;
        public bool IsEventAssociated = false;
        public bool IsOneTrigger = false;
        public bool IsProgramReadable = true;
        public bool IsProgramWritable = true;
        public bool IsSave = false;
        public int ResetType = 0;

        public BaseFlag() { }
        public BaseFlag(SortedDictionary<string, dynamic> dict) : this()
        {
            if (ValidateInFlag(dict)) {
                DataName = dict["DataName"];
                DeleteRev = dict["DeleteRev"];
                IsEventAssociated = dict["IsEventAssociated"];
                IsOneTrigger = dict["IsOneTrigger"];
                IsProgramReadable = dict["IsProgramReadable"];
                IsProgramWritable = dict["IsProgramWritable"];
                IsSave = dict["IsSave"];
                ResetType = dict["ResetType"];
            }
        }

        private static bool ValidateInFlag(SortedDictionary<string, dynamic> dict)
        {
            try {
                string dn = dict["DataName"];
                int hv = dict["HashValue"];
                int dr = dict["DeleteRev"];
                bool iea = dict["IsEventAssociated"];
                bool iot = dict["IsOneTrigger"];
                bool ipr = dict["IsProgramReadable"];
                bool ipw = dict["IsProgramWritable"];
                bool isave = dict["IsSave"];
                int rt = dict["ResetType"];
                return true;
            }
            catch {
                return false;
            }
        }

        public bool Equals(BaseFlag other)
        {
            if (this == other ||
                (GetType() == other.GetType() &&
                DataName == other.DataName &&
                DeleteRev == other.DeleteRev &&
                HashValue == other.HashValue &&
                IsEventAssociated == other.IsEventAssociated &&
                IsOneTrigger == other.IsOneTrigger &&
                IsProgramReadable == other.IsProgramReadable &&
                IsProgramWritable == other.IsProgramWritable &&
                IsSave == other.IsSave &&
                ResetType == other.ResetType)) {
                return true;
            }
            return false;
        }

        public SortedDictionary<string, dynamic> ToByml()
        {
            SortedDictionary<string, dynamic> byml = new() {
                ["DataName"] = DataName,
                ["HashValue"] = HashValue,
                ["DeleteRev"] = DeleteRev,
                ["IsEventAssociated"] = IsEventAssociated,
                ["IsOneTrigger"] = IsOneTrigger,
                ["IsProgramReadable"] = IsProgramReadable,
                ["IsProgramWritable"] = IsProgramWritable,
                ["IsSave"] = IsSave,
                ["ResetType"] = ResetType
            };
            return byml;
        }

        public SortedDictionary<string, dynamic> ToSvByml()
        {
            SortedDictionary<string, dynamic> byml = new() {
                ["DataName"] = DataName,
                ["HashValue"] = HashValue
            };
            return byml;
        }

        public bool Exists() => HashValue != 0;

        public void OverrideParams(Dictionary<string, Dictionary<string, dynamic>> overrides)
        {
            foreach (KeyValuePair<string, dynamic> pair in overrides["IS_EVENT_ASSOCIATED"]) {
                if (new Regex(pair.Key).Matches(_dataName).Count > 0) {
                    IsEventAssociated = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["IS_ONE_TRIGGER"]) {
                if (new Regex(pair.Key).Matches(_dataName).Count > 0) {
                    IsOneTrigger = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["IS_PROGRAM_READABLE"]) {
                if (new Regex(pair.Key).Matches(_dataName).Count > 0) {
                    IsProgramReadable = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["IS_PROGRAM_WRITABLE"]) {
                if (new Regex(pair.Key).Matches(_dataName).Count > 0) {
                    IsProgramWritable = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["IS_SAVE"]) {
                if (new Regex(pair.Key).Matches(_dataName).Count > 0) {
                    IsSave = pair.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> pair in overrides["RESET_TYPE"]) {
                if (new Regex(pair.Key).Matches(_dataName).Count > 0) {
                    ResetType = pair.Value;
                }
            }
        }

        public string DataName { get => _dataName; set { _dataName = value; _hashValue = (int)Crc32.Compute(_dataName); } }
        public int HashValue { get => _hashValue; }
        public bool IsRevival { get => false; }
    }
}
