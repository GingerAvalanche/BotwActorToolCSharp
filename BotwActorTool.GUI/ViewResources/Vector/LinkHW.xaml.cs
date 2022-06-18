using System.Windows.Controls;
using System.Windows.Media;

namespace BotwActorTool.GUI.ViewResources.Vector
{
    /// <summary>
    /// Interaction logic for Botw2.xaml
    /// </summary>
    public partial class LinkHW : UserControl
    {
        public LinkHW()
        {
            InitializeComponent();
        }

        public void SetFill(Color color) => vector.Fill = new SolidColorBrush(color);
    }
}
