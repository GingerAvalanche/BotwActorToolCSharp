using BotwActorTool.Lib;
using Dock.Model.ReactiveUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.ViewModels
{
    public class ActorViewModel : Document
    {
        public Actor Actor { get; set; }
        public ActorViewModel(string actorpack)
        {
            Actor = new(actorpack);
        }
    }
}
