using Avalonia.Controls;
using BotwActorTool.Builders;
using BotwActorTool.ViewModels.Documents;

namespace BotwActorTool.Views.Documents
{
    public partial class ActorEditorView : UserControl
    {
        public ActorEditorView()
        {
            InitializeComponent();
            ActorEditorViewModel doc = new();
            DataContext = doc;

            ToolbarFactory factory = new(doc, Toolbar);
            factory.Initialize();
        }
    }
}
