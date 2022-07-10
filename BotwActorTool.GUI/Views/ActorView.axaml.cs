using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BotwActorTool.GUI.Views
{
    public partial class ActorView : UserControl
    {
        public ActorView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
