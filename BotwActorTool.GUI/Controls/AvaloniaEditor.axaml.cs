using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;
using AvaloniaEdit.Indentation.CSharp;

namespace BotwActorTool.GUI.Controls
{
    public partial class AvaloniaEditor : UserControl
    {
        private readonly TextEditor _textEditor;

        public AvaloniaEditor()
        {
            InitializeComponent();
            _textEditor = this.FindControl<TextEditor>("editor");
            _textEditor.ShowLineNumbers = true;
            _textEditor.TextArea.IndentationStrategy = new CSharpIndentationStrategy();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
