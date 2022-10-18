using BotwActorTool.GUI.Models;
using BotwActorTool.Lib;
using Byml.Security.Cryptography;
using Nintendo.Byml;
using Nintendo.Yaz0;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.Extensions
{
    public enum AltActorsAction { RemoveAltActors, KeepAltActors }

    public static class ActorInfoExt
    {
        public static ObservableCollection<TreeNodeModel> LoadActorInfoNodes(string modRoot) => LoadActorInfoNodes(modRoot, null);
        public static ObservableCollection<TreeNodeModel> LoadActorInfoNodes(string modRoot, string? altModRoot, AltActorsAction action = AltActorsAction.RemoveAltActors)
        {
            BymlFile actorInfo = new(Yaz0.DecompressFast(Util.GetFile($"{modRoot}/Actor/ActorInfo.product.sbyml")));
            Func<BymlNode, bool> condition;

            if (altModRoot != null) {
                BymlFile altActorInfo = new(Yaz0.DecompressFast(Util.GetFile($"{altModRoot}/Actor/ActorInfo.product.sbyml")));
                condition = (x) => {
                    string? name = x.Hash["name"].String;
                    bool inAlt = altActorInfo.RootNode.Hash["Hashes"].Array.Where(x => x.UInt == Crc32.Compute(name)).Any();
                    return name != null && !name.EndsWith("_Far") && (action == AltActorsAction.RemoveAltActors ? !inAlt : inAlt);
                };
            }
            else {
                condition = (x) => x.Hash["name"].String is string str && !str.EndsWith("_Far");
            }

            return new(actorInfo.RootNode.Hash["Actors"].Array.Where(condition).Select(x => {
                return new TreeNodeModel(x.Hash["name"].String, x.GetActorDescription()) {
#if !DEBUG // Disable in DEBUG to speed up initialization time, removes 5-10sec
                    Meta = x.SerializeNode() ?? ""
#endif
                };
            }).OrderBy(x => x.Key));
        }
    }
}
