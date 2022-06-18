using BotwActorTool.GUI.ViewThemes.App;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.IO;
using System.Xml;

namespace BotwActorTool.GUI.ViewResources.Helpers
{
    public class TextEditorActions
    {
        public static void ChangeSyntaxHighlighting(ICSharpCode.AvalonEdit.TextEditor textEditor, string name)
        {
            using FileStream stream = File.OpenRead($"ViewThemes\\Styles\\TextEditor\\SyntaxHighlighting\\{name}{SysTheme.Name}.xshd");
            using XmlReader reader = XmlReader.Create(stream);
            textEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
        }
    }
}
