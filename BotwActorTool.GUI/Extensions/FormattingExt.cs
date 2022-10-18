using Nintendo.Byml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.Extensions
{
    public static class FormattingExt
    {
        public static string GetActorDescription(this BymlNode actor)
        {
            Dictionary<string, string> schema = new() {
                { "Name", "WIP" },
                { "Bfres", "#bfres" },
                { "Model", "#mainModel" },
                { "Profile", "#profile" },
            };

            StringBuilder str = new();
            foreach ((var name, var value) in schema) {
                string nl = name == schema.LastOrDefault().Key ? "" : "\n";
                if (value.StartsWith('#')) {
                    var key = value.Remove(0, 1);
                    str.Append($"{name}: {(actor.Hash.ContainsKey(key) ? actor!.Hash[key].String : "(-)")}{nl}");
                }
                else {
                    str.Append($"{name}: {value}{nl}");
                }
            }

            return str.ToString();
        }
    }
}
