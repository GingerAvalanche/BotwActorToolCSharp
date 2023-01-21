using BotwActorTool.Lib;
using BotwActorTool.Models;
using Byml.Security.Cryptography;
using Nintendo.Byml;
using Nintendo.Yaz0;
using System.Collections.ObjectModel;
using System.Text;

namespace BotwActorTool.Extensions
{
    public enum AltActorsAction { RemoveAltActors, KeepAltActors }

    public static class ActorInfoExtension
    {
        /// <inheritdoc cref="LoadActorInfoNodes(string, string, AltActorsAction)"/>
        public static ObservableCollection<ActorNodeModel> LoadActorInfoNodes(string modRoot) => LoadActorInfoNodes(modRoot, null);

        /// <summary>
        /// Loads a <see cref="ObservableCollection{TreeNodeModel}"/> (node tree) from the target mod's ActorInfo
        /// </summary>
        /// <param name="altModRoot">Specify an alternative mod root to compare against</param>
        /// <exception cref="NullReferenceException"></exception>
        public static ObservableCollection<ActorNodeModel> LoadActorInfoNodes(string modRoot, string? altModRoot, AltActorsAction action = AltActorsAction.RemoveAltActors)
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
                return new ActorNodeModel(x.Hash["name"].String, x.GetActorDescription()) {
                    Meta = x
                };
            }).OrderBy(x => x.Name));
        }

        public static string GetActorDescription(this BymlNode actor)
        {
            (string Key, string Value)[] schema = new (string, string)[] {
                new("name", "Name"),
                new("bfres", "Bfres"),
                new("mainModel", "Model"),
                new("profile", "Profile")
            };

            StringBuilder sb = new();
            for (int i = 0; i < schema.Length; i++) {
                (string key, string value) item = schema[i];
                sb.Append(item.value);
                sb.Append(": ");
                if (item.key == "name") {
                    // Load name from MSBT
                    sb.Append(actor.Hash.TryGetValue(schema[i].Key, out BymlNode? node) ? node : "(-)");
                }
                else {
                    sb.Append(actor.Hash.TryGetValue(schema[i].Key, out BymlNode? node) ? node : "(-)");
                }

                if (i + 1 < schema.Length) {
                    sb.Append('\n');
                }
            }

            return sb.ToString();
        }
    }
}
