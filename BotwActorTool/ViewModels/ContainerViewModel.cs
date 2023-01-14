using Dock.Model.ReactiveUI.Controls;

namespace BotwActorTool.ViewModels
{
    public class ContainerViewModel : Document
    {
        private object? _content = null;
        public object? Content {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }
    }
}
