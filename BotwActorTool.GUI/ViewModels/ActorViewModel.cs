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
        public ActorViewModel(string actorpack)
        {
            Actor actor = new(actorpack);
        }
    }
}
