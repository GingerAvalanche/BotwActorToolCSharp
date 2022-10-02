using Nintendo.Byml;
using Aamp.Security.Cryptography;
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
        public BaseFlag(BymlNode dict) : this()
        {
            DataName = dict.Hash["DataName"].String;
            DeleteRev = dict.Hash["DeleteRev"].Int;
            IsEventAssociated = dict.Hash["IsEventAssociated"].Bool;
            IsOneTrigger = dict.Hash["IsOneTrigger"].Bool;
            IsProgramReadable = dict.Hash["IsProgramReadable"].Bool;
            IsProgramWritable = dict.Hash["IsProgramWritable"].Bool;
            IsSave = dict.Hash["IsSave"].Bool;
            ResetType = dict.Hash["ResetType"].Int;
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

        public BymlNode ToByml()
        {
            Dictionary<string, BymlNode> hash = new() {
                ["DataName"] = new BymlNode(DataName),
                ["HashValue"] = new BymlNode(HashValue),
                ["DeleteRev"] = new BymlNode(DeleteRev),
                ["IsEventAssociated"] = new BymlNode(IsEventAssociated),
                ["IsOneTrigger"] = new BymlNode(IsOneTrigger),
                ["IsProgramReadable"] = new BymlNode(IsProgramReadable),
                ["IsProgramWritable"] = new BymlNode(IsProgramWritable),
                ["IsSave"] = new BymlNode(IsSave),
                ["ResetType"] = new BymlNode(ResetType)
            };
            return new BymlNode(hash);
        }

        public BymlNode ToSvByml()
        {
            Dictionary<string, BymlNode> hash = new() {
                ["DataName"] = new BymlNode(DataName),
                ["HashValue"] = new BymlNode(HashValue)
            };
            return new BymlNode(hash);
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
