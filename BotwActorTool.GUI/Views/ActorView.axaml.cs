using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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

            var ribbon = this.FindControl<StackPanel>("RIBBON");

            foreach (Control _child in ribbon.Children) {
                if (_child.Name != null) {
                    var desc = _child.Name
                        .Replace("__", " ")
                        .Replace("b_0", "(")
                        .Replace("b_1", ")")
                        .Replace("n_l", "\n")
                        .Replace("p_0", "+");

                    if (_child is Button child_0) {
                        ToolTip.SetTip(_child, $"{desc}\n{child_0.HotKey}");
                    }
                    else if (_child is ToggleButton child_1) {
                        ToolTip.SetTip(_child, $"{desc}\n{child_1.HotKey}");
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
