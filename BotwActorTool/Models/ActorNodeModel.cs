using Nintendo.Byml;

namespace BotwActorTool.Models
{
    public class ActorNodeModel : ReactiveObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BymlNode Meta { get; set; } = new("null");

        private bool _visible = true;
        public bool Visible {
            get => _visible;
            set => this.RaiseAndSetIfChanged(ref _visible, value);
        }

        public ActorNodeModel(string name, string tooltip = "")
        {
            Name = name;
            Description = tooltip;
        }
    }
}
