using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BotwActorToolUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private BindableCollection<ActorViewModel> _actors;
        public BindableCollection<ActorViewModel> Actors
        {
            get => _actors;
            set => SetAndNotify(ref _actors, value);
        }

        private ActorViewModel _selectedActor;
        public ActorViewModel SelectedActor
        {
            get => _selectedActor;
            set => SetAndNotify(ref _selectedActor, value);
        }

        public ShellViewModel()
        {
            var Weapon_Sword_001 = new ActorViewModel("Weapon_Sword_001", "Travelers Sword", new List<string> { "TagA1", "TagA2", "TagA3"});
            var Weapon_Sword_002 = new ActorViewModel("Weapon_Sword_002", "Soilders Broadword", new List<string> { "TagB1", "TagB2", "TagB3" });
            var Weapon_Sword_003 = new ActorViewModel("Weapon_Sword_003", "I forgot wich one...", new List<string> { "TagC1", "TagC2", "TagC3" });

            Actors = new BindableCollection<ActorViewModel>();
            Actors.Add(Weapon_Sword_001);
            Actors.Add(Weapon_Sword_002);
            Actors.Add(Weapon_Sword_003);
        }

        public void RemoveActor(ActorViewModel model)
        {
            Actors.Remove(model);
        }
    }
}
