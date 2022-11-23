using Avalonia.Controls;

namespace BotwActorTool.GUI.Views
{
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
            ShellViewModel = new();
            DataRoot.DataContext = ShellViewModel;
        }
    }
}
