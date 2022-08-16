using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.TextMate;
using System;
using System.Collections.Generic;
using TextMateSharp.Grammars;

namespace BotwActorTool.GUI.Views
{
    public partial class ActorView : UserControl
    {
        public ActorView()
        {
            InitializeComponent();

            // First of all you need to have a reference for your TextEditor for it to be used inside AvaloniaEdit.TextMate project.
            var _textEditor = this.FindControl<TextEditor>("editor");
            _textEditor.Background = Brushes.Transparent;
            _textEditor.ShowLineNumbers = true;
            _textEditor.TextArea.Background = Background;
            _textEditor.Options.ShowBoxForControlCharacters = true;
            _textEditor.TextArea.IndentationStrategy = new AvaloniaEdit.Indentation.CSharp.CSharpIndentationStrategy(_textEditor.Options);
            RegistryOptions _registryOptions = new(ThemeName.DarkPlus);
            TextMate.Installation _textMateInstallation = _textEditor.InstallTextMate(_registryOptions);
            Language csharpLanguage = _registryOptions.GetLanguageByExtension(".yml");
            _textMateInstallation.SetGrammar(_registryOptions.GetScopeByLanguageId(csharpLanguage.Id));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
