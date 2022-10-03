using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.TextMate;
using BotwActorTool.GUI.Controls;
using BotwActorTool.GUI.Dialogs;
using BotwActorTool.GUI.ViewModels;
using System;
using System.Text;
using TextMateSharp.Grammars;

namespace BotwActorTool.GUI.Views.Editors
{
    public partial class YamlEditor : ActorEditor
    {
        public YamlEditor()
        {
            AvaloniaXamlLoader.Load(this);

            base.editor = this.FindControl<TextEditor>("editor");
            base.editor.Background = Brushes.Transparent;
            base.editor.ShowLineNumbers = true;
            base.editor.TextArea.Background = Background;
            base.editor.Options.ShowBoxForControlCharacters = true;
            base.editor.TextArea.IndentationStrategy = new AvaloniaEdit.Indentation.CSharp.CSharpIndentationStrategy(base.editor.Options);
            base.editor.Encoding = Encoding.UTF8;
            base.editor.Document = new TextDocument("404 not found");

            RegistryOptions registryOptions = Config.Theme == "Dark" ? new(ThemeName.DarkPlus) : new(ThemeName.LightPlus);
            TextMate.Installation textMateInstallation = ((TextEditor)base.editor).InstallTextMate(registryOptions);
            Language csharpLanguage = registryOptions.GetLanguageByExtension(".yml");
            textMateInstallation.SetGrammar(registryOptions.GetScopeByLanguageId(csharpLanguage.Id));
        }

        public async void SetEditorMode(bool fast)
        {
            try {
                if (fast) {
                    RegistryOptions registryOptions = Config.Theme == "Dark" ? new(ThemeName.DarkPlus) : new(ThemeName.LightPlus);
                    TextMate.Installation textMateInstallation = ((TextEditor)base.editor!).InstallTextMate(registryOptions);
                    textMateInstallation.SetGrammar(null);
                }
                else {
                    RegistryOptions registryOptions = Config.Theme == "Dark" ? new(ThemeName.DarkPlus) : new(ThemeName.LightPlus);
                    TextMate.Installation textMateInstallation = ((TextEditor)base.editor!).InstallTextMate(registryOptions);
                    Language csharpLanguage = registryOptions.GetLanguageByExtension(".yml");
                    textMateInstallation.SetGrammar(registryOptions.GetScopeByLanguageId(csharpLanguage.Id));
                }
            }
            catch (Exception ex) {
                await MessageBox.Show($"{ex}", "Error Setting Editor Mode");
            }
        }
    }
}
