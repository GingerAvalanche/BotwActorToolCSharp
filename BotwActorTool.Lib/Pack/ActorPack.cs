﻿using Nintendo.Sarc;
using Nintendo.Aamp;
using Nintendo.Byml;
using Syroot.BinaryData.Core;

namespace BotwActorTool.Lib.Pack
{
    internal class ActorPack
    {
        private string _actorname;
        private readonly Dictionary<string, AampFile> _aampfiles = new();
        private readonly Dictionary<string, AampFile> _animseqfiles = new();
        private readonly Dictionary<string, BymlFile> _bymlfiles = new();
        private readonly Dictionary<string, byte[]> _miscfiles = new();
        private readonly Dictionary<string, string> _links = new();
        private string[] _tags = Array.Empty<string>();
        private string[] _tags2 = Array.Empty<string>();
        private readonly Endian endianness;
        public string Name { get => _actorname; }
        public string[] AnimSeqNames { get => _animseqfiles.Keys.OrderBy(k => k).ToArray(); }
        public string Tags { get => string.Join(", ", _tags); set => _tags = value.Split(",").Select(s => s.Trim()).ToArray(); }
        public string Tags2 { get => string.Join(", ", _tags2); set => _tags2 = value.Split(",").Select(s => s.Trim()).ToArray(); }
        public Endian Endianness { get => endianness; }

        public ActorPack(string actorname, SarcFile sarc)
        {
            _actorname = actorname;
            endianness = sarc.Endianness;
            List<string> handled = new() { $"Actor/ActorLink/{_actorname}.bxml" };
            AampFile actorlink = new(sarc.Files[handled[0]]);
            foreach (ParamObject obj in actorlink.RootNode.ParamObjects)
            {
                switch (obj.Hash)
                {
                    case 723360088: // LinkTarget
                        foreach (ParamEntry entry in obj.ParamEntries)
                        {
                            _links[entry.HashString] = entry.HashString == "ActorScale" ? ((float)entry.Value!).ToString() : ((StringEntry)entry.Value!).ToString();
                        }
                        break;
                    case 3482204952: // Tags
                        _tags = obj.ParamEntries.Select(e => ((StringEntry)e.Value!).ToString()).ToArray();
                        break;
                    case 1115720914: // misc tags
                        _tags2 = obj.ParamEntries.Select(e => ((StringEntry)e.Value!).ToString()).ToArray();
                        break;
                }
            }

            foreach ((string link, (string folder, string ext)) in Util.AAMP_LINK_REFS)
            {
                if (_links[link] == "Dummy")
                {
                    continue;
                }
                string filename = $"Actor/{folder}/{_links[link]}{ext}";
                _aampfiles[link] = new AampFile(sarc.Files[filename]);
                handled.Add(filename);
            }

            foreach ((string link, (string folder, string ext)) in Util.BYML_LINK_REFS)
            {
                if (_links[link] == "Dummy")
                {
                    continue;
                }
                string filename = $"Actor/{folder}/{_links[link]}{ext}";
                _bymlfiles[link] = new BymlFile(sarc.Files[filename]);
                handled.Add(filename);
            }

            foreach ((string name, byte[] data) in sarc.Files)
            {
                if (name.StartsWith("Actor/AS/"))
                {
                    _animseqfiles[name.Replace("Actor/AS/", "")] = new AampFile(data);
                    handled.Add(name);
                }
                else if (!handled.Contains(name))
                {
                    _miscfiles.Add(name, data);
                }
            }
        }

        public void SetName(string name, bool setMeta = true)
        {
            if (setMeta)
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

                if (name.Contains("Armor_") && _links["ModelUser"] == name)
                {
                    _aampfiles["ModelUser"].RootNode.Lists("ModelData")!.Lists("ModelData_0")!
                        .Objects("Base")!.Params("Folder")!.Value = string.Join("_", name.Split("_")[..^1]);
                }
            }

            _actorname = name;
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
                    _aampfiles[link] = AampFile.New(2);
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
                    _bymlfiles[link] = new(new List<BymlNode>()) { Endianness = endianness };
                }
                else if (linkref == "Dummy")
                {
                    _bymlfiles.Remove(link);
                }
            }
        }

        public byte[] GetLinkDataBytes(string link)
        {
            if (_links[link] != "Dummy")
            {
                if (Util.AAMP_LINK_REFS.ContainsKey(link))
                {
                    return _aampfiles[link].ToBinary();
                }
                if (Util.BYML_LINK_REFS.ContainsKey(link))
                {
                    return _bymlfiles[link].ToBinary();
                }
            }
            return Array.Empty<byte>();
        }
        public void SetLinkDataBytes(string link, byte[] bytes)
        {
            if (Util.AAMP_LINK_REFS.ContainsKey(link))
            {
                _aampfiles[link] = new(bytes);
            }
            else if (Util.BYML_LINK_REFS.ContainsKey(link))
            {
                _bymlfiles[link] = new(bytes);
            }
        }

        public string GetLinkDataYaml(string link)
        {
            if (_links[link] != "Dummy")
            {
                if (Util.AAMP_LINK_REFS.ContainsKey(link))
                {
                    return _aampfiles[link].ToYml();
                }
                if (Util.BYML_LINK_REFS.ContainsKey(link))
                {
                    return _bymlfiles[link].ToYaml();
                }
            }
            return "";
        }
        public void SetLinkDataYaml(string link, string data)
        {
            if (Util.AAMP_LINK_REFS.ContainsKey(link))
            {
                _aampfiles[link] = AampFile.FromYml(data);
            }
            else if (Util.BYML_LINK_REFS.ContainsKey(link))
            {
                _bymlfiles[link] = BymlFile.FromYaml(data);
            }
        }

        public string GetAnimSeq(string name) => _animseqfiles[name].ToYml();
        public void RemoveAnimSeq(string name) => _animseqfiles.Remove(name);
        public void SetAnimSeq(string name, string data) => _animseqfiles[name] = AampFile.FromYml(data);

        public AampFile GetActorLink()
        {
            AampFile actorlink = AampFile.New(2);

            byte num_objects = 1;
            bool tags = _tags.Length > 0;
            if (tags) num_objects++;
            bool tags2 = _tags2.Length > 0;
            if (tags2) num_objects++;

            actorlink.RootNode.ParamObjects = new ParamObject[num_objects];
            actorlink.RootNode.ParamObjects[0] = new()
            {
                HashString = "LinkTarget",
                ParamEntries = new ParamEntry[_links.Count],
            };
            int count = 0;
            foreach ((string link, string linkref) in _links)
            {
                actorlink.RootNode.ParamObjects[0].ParamEntries[count] = new()
                {
                    HashString = link,
                    ParamType = ParamType.StringRef,
                    Value = new StringEntry(linkref),
                };
                count++;
            }
            if (tags)
            {
                actorlink.RootNode.ParamObjects[1] = new()
                {
                    HashString = "Tags",
                    ParamEntries = new ParamEntry[_tags.Length],
                };
                count = 0;
                foreach (string tag in _tags)
                {
                    actorlink.RootNode.ParamObjects[1].ParamEntries[count] = new()
                    {
                        HashString = $"Tag{count}",
                        ParamType = ParamType.StringRef,
                        Value = new StringEntry(tag),
                    };
                    count++;
                }
            }
            if (tags2)
            {
                actorlink.RootNode.ParamObjects[2] = new()
                {
                    Hash = 1115720914,
                    ParamEntries = new ParamEntry[_tags2.Length],
                };
                count = 0;
                foreach (string tag in _tags2)
                {
                    actorlink.RootNode.ParamObjects[2].ParamEntries[count] = new()
                    {
                        HashString = $"Tag{count}",
                        ParamType = ParamType.StringRef,
                        Value = new StringEntry(tag),
                    };
                    count++;
                }
            }

            return actorlink;
        }

        public byte[] Write()
        {
            Dictionary<string, byte[]> files = new();

            string filename = $"Actor/ActorLink/{_actorname}.bxml";
            files[filename] = GetActorLink().ToBinary();

            foreach ((string link, AampFile file) in _aampfiles)
            {
                (string folder, string ext) = Util.AAMP_LINK_REFS[link];
                filename = $"Actor/{folder}/{GetLink(link)}{ext}";
                files[filename] = file.ToBinary();
            }

            foreach ((string link, BymlFile file) in _bymlfiles)
            {
                (string folder, string ext) = Util.BYML_LINK_REFS[link];
                filename = $"Actor/{folder}/{GetLink(link)}{ext}";
                files[filename] = file.ToBinary();
            }

            foreach ((string name, AampFile file) in _animseqfiles.OrderBy(p => p.Key))
            {
                filename = $"Actor/AS/{name}";
                files[filename] = file.ToBinary();
            }

            foreach ((string name, byte[] data) in _miscfiles)
            {
                files[name] = data;
            }

            return new SarcFile(files, endianness).ToBinary();
        }
    }
}
