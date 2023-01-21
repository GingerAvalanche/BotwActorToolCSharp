using Avalonia.Generics.Dialogs;
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

        public async Task Open()
        {
            // INVOKE OPEN
            // - Report UI as working
            // - Build View (async)
            // - Inject into DocumentDock
            // - End working
            await MessageBox.ShowDialog("INVOKE OPEN");
        }

        public async Task CreateCopy()
        {
            // INPUT DIALOG
            var name = (await InputDialog.ShowDialog(new Dictionary<string, string>() {
                { "Name", Name }
            }, $"Copy {Name}"))?["Name"];

            if (name != null) {
                // INVOKE OPEN
                // RENAME
            }
        }

        public async Task ActorInfo()
        {
            await MessageBox.ShowDialog(Meta.SerializeNode(), $"Actor Info");
        }
    }
}
