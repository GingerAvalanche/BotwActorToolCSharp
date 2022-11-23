using System.Collections.ObjectModel;

namespace BotwActorTool.Models
{
    public class TreeNodeModel : ReactiveObject
    {
        public string Key { get; set; }
        public string Tooltip { get; set; }
        public ObservableCollection<TreeNodeModel>? Nodes { get; set; }
        // public Dictionary<string, string> Meta { get; set; } = new();

        public object Meta { get; set; } = "";

        public TreeNodeModel(string key, string tooltip = "")
        {
            Key = key;
            Tooltip = tooltip;
        }

        public TreeNodeModel(string key, ObservableCollection<TreeNodeModel> nodes, string tooltip = "")
        {
            Key = key;
            Tooltip = tooltip;
            Nodes = nodes;
        }

        public TreeNodeModel(string key, string tooltip = "", params TreeNodeModel[] nodes)
        {
            Key = key;
            Tooltip = tooltip;
            Nodes = new();

            foreach (TreeNodeModel node in nodes) {
                Nodes.Add(node);
            }
        }
    }
}
