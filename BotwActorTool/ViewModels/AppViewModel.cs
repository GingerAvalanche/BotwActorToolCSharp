using Dock.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.ViewModels
{
    public class AppViewModel : ReactiveObject
    {
        private IDock layout;
        public IDock Layout {
            get => layout;
            set => this.RaiseAndSetIfChanged(ref layout, value);
        }

        private IFactory factory;
        public IFactory Factory {
            get => factory;
            set => this.RaiseAndSetIfChanged(ref factory, value);
        }
    }
}
