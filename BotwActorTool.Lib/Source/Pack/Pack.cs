using Nintendo.Sarc;
using Nintendo.Aamp;
using Nintendo.Byml;

namespace BotwActorTool.Lib.Pack
{
    internal class Pack
    {
        private string _actorname;
        private Dictionary<string, AampFile> _aampfiles = new();
        private Dictionary<string, BymlFile> _bymlfiles = new();
        private Dictionary<string, byte[]> _miscfiles = new();
        private Dictionary<string, string> _links = new();
        private List<string> _tags = new();
        private List<string> _tags2 = new();
        public string Name { get => _actorname; }
        public string Tags { get => string.Join(", ", _tags); set => _tags = value.Split(", ").ToList(); }
        public string Tags2 { get => string.Join(", ", _tags2); set => _tags2 = value.Split(", ").ToList(); }

        public Pack(string actorname, SarcFile sarc)
        {
            _actorname = actorname;
            List<string> handled = new() { $"Actor/ActorLink/{_actorname}.bcml" };
            AampFile actorlink = new(sarc.Files[handled[0]]);
            foreach (ParamObject obj in actorlink.RootNode.ParamObjects)
            {
                switch (obj.Hash)
                {
                    case 723360088: // LinkTarget
                        foreach (ParamEntry entry in obj.ParamEntries)
                        {
                            _links[entry.HashString] = entry.HashString == "ActorScale" ? ((float)entry.Value!).ToString() : (string)entry.Value!;
                        }
                        break;
                    case 3482204952: // Tags
                        _tags = obj.ParamEntries.Select(e => (string)e.Value!).ToList();
                        break;
                    case 1115720914: // misc tags
                        _tags2 = obj.ParamEntries.Select(e => (string)e.Value!).ToList();
                        break;
                }
            }

            foreach ((string link, (string folder, string ext)) in Util.AAMP_LINK_REFS)
            {
                string filename = $"Actor/{folder}/{_links[link]}{ext}";
                _aampfiles[link] = new AampFile(sarc.Files[filename]);
                handled.Add(filename);
            }

            foreach ((string link, (string folder, string ext)) in Util.BYML_LINK_REFS)
            {
                string filename = $"Actor/{folder}/{_links[link]}{ext}";
                _bymlfiles[link] = new BymlFile(sarc.Files[filename]);
                handled.Add(filename);
            }

            foreach ((string name, byte[] data) in sarc.Files)
            {
                if (!handled.Contains(name))
                {
                    _miscfiles.Add(name, data);
                }
            }
        }

        public void SetName(string name)
        {
            foreach ((string link, string linkref) in _links)
            {
                if (linkref == _actorname)
                {
                    _links[link] = name;
                }
            }

            foreach ((string link, AampFile aamp) in _aampfiles)
            {
                string yaml = aamp.ToYml();
                string new_yaml = yaml.Replace(_actorname, name);
                if (!ReferenceEquals(yaml, new_yaml))
                {
                    _aampfiles[link] = AampFile.FromYml(new_yaml);
                }
            }

            foreach ((string link, BymlFile byml) in _bymlfiles)
            {
                string yaml = byml.ToYaml();
                string new_yaml = yaml.Replace(_actorname, name);
                if (!ReferenceEquals(yaml, new_yaml))
                {
                    _bymlfiles[link] = BymlFile.FromYaml(new_yaml);
                }
            }

            foreach (string filename in _miscfiles.Keys)
            {
                if (filename.Contains(_actorname))
                {
                    string new_filename = filename.Replace(_actorname, name);
                    _miscfiles[new_filename] = _miscfiles[filename];
                    _miscfiles.Remove(filename);
                }
            }

            _actorname = name;

            if (_actorname.Contains("Armor_") && _links["ModelUser"] == _actorname)
            {
                _aampfiles["ModelUser"].RootNode
                    .ChildParams.First(l => l.HashString == "ModelData")
                    .ChildParams.First(l => l.HashString == "ModelData_0")
                    .ParamObjects.First(o => o.HashString == "Base")
                    .ParamEntries.First(e => e.HashString == "Folder").Value = string.Join("_", _actorname.Split("_")[..^1]);
            }
        }

        public string GetLink(string link) => _links[link];
        public void SetLink(string link, string linkref)
        {
            string old_ref = _links[link];
            _links[link] = linkref;

            if (Util.AAMP_LINK_REFS.ContainsKey(link))
            {
                if (old_ref == "Dummy")
                {
                    _aampfiles[link] = new AampFile(new byte[] { 0x41, 0x41, 0x4D, 0x50, 0x02, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x6C, 0xCB, 0xF6, 0xA4, 0x03, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00 });
                }
                else if (linkref == "Dummy")
                {
                    _aampfiles.Remove(link);
                }
            }
            else if (Util.BYML_LINK_REFS.ContainsKey(link))
            {
                if (old_ref == "Dummy")
                {
                    _bymlfiles[link] = new BymlFile(new byte[] { 0x42, 0x59, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0xC1, 0x00, 0x00, 0x00 });
                }
                else if (linkref == "Dummy")
                {
                    _bymlfiles.Remove(link);
                }
            }
        }

        public string GetLinkData(string link)
        {
            throw new NotImplementedException();
        }
        public void SetLinkData(string link, string data)
        {
            throw new NotImplementedException();
        }

        public AampFile GetActorLink()
        {
            throw new NotImplementedException();
        }

        public byte[] Write(bool big_endian)
        {
            throw new NotImplementedException();
        }
    }
}
