using Material.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MenuAttribute : Attribute
    {
        public string? Name { get; set; }
        public string? Path { get; set; }
        public string HotKey { get; set; } = "";
        public MaterialIconKind Icon { get; set; }
        public bool IsSeparator { get; set; } = false;

        public MenuAttribute(string name, string path, string hotkey = "")
        {
            Name = name;
            Path = path;
            HotKey = hotkey;
        }
    }
}
