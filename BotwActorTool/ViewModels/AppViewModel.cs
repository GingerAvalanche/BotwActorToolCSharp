using Dock.Model.Core;

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
