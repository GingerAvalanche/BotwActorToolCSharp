using BotwActorTool.GUI.Models;
using BotwActorTool.Lib;
using Byml.Security.Cryptography;
using Nintendo.Byml;
using Nintendo.Yaz0;
using SharpYaml.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BotwActorTool.GUI.Extensions
{
    public enum AltActorsAction { RemoveAltActors, KeepAltActors }

    public static class ActorInfoExtension
    {
        /// <inheritdoc cref="LoadActorInfoNodes(string, string, AltActorsAction)"/>
        public static ObservableCollection<TreeNodeModel> LoadActorInfoNodes(string modRoot) => LoadActorInfoNodes(modRoot, null);

        /// <summary>
        /// Loads a <see cref="ObservableCollection{TreeNodeModel}"/> (node tree) from the target mod's ActorInfo
        /// </summary>
        /// <param name="altModRoot">Specify an alternative mod root to compare against</param>
        /// <exception cref="NullReferenceException"></exception>
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
                    Meta = x
                };
            }).OrderBy(x => x.Key));
        }

        public static string GetActorDescription(this BymlNode actor)
        {
            Dictionary<string, string> schema = new() {
                { "Name", "name" }, // WIP implementation, in future this will be loaded from the MSBT files for the in-game name
                { "Bfres", "bfres" },
                { "Model", "mainModel" },
                { "Profile", "profile" },
            };

            return string.Join("\n", schema.Select(x => {
                return $"{x.Key}: {(actor.Hash.TryGetValue(x.Value, out BymlNode? node) ? node : "(-)")}";
            }));
        }
    }
}
