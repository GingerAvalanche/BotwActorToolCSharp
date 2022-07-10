using BotwActorTool.GUI.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.Extensions
{
    public static class CollectionExt
    {
        public static Node Add(this ObservableCollection<Node> list, string item)
        {
            Node node = new(item);
            list.Add(node);
            return node;
        }
    }
}
