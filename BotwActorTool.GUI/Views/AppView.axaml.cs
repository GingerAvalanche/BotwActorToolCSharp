using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BotwActorTool.GUI.Views
{
    public partial class AppView : UserControl
    {
        public AppView()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
