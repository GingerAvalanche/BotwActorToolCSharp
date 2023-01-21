using System.Collections.ObjectModel;

namespace BotwActorTool.Models
{
    public class ModNodeModel : ReactiveObject
    {
        public ModNodeModel(string name, string? description)
        {
            Name = name;
            Description = description;

            // Populate Actors from mod source (backend)
            Actors = new();
        }

        public string Name { get; set; }
        public string? Description { get; set; }

        private bool _visible = true;
        public bool Visible {
            get => _visible;
            set => this.RaiseAndSetIfChanged(ref _visible, value);
        }

        private ObservableCollection<ActorNodeModel> _actors;
        public ObservableCollection<ActorNodeModel> Actors {
            get => _actors;
            set => this.RaiseAndSetIfChanged(ref _actors, value);
        }

        public bool Search(string str)
        {
            bool any = false;
            foreach (var actor in Actors) {
                actor.Visible = string.IsNullOrEmpty(str) || str.Contains(actor.Name.ToLower()) || actor.Name.ToLower().Contains(str);
                any = actor.Visible || any;
            }

            return any;
        }
    }
}
