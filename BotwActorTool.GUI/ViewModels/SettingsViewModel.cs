#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Themes.Fluent;
using BotwActorTool.GUI.Extensions;
using BotwActorTool.GUI.Views;
using BotwActorTool.Lib;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BotwActorTool.GUI.ViewModels
{
    public class SettingsViewModel : ReactiveObject
    {
        private StackPanel? activeElement;
        public StackPanel? ActiveElement {
            get => activeElement;
            set => this.RaiseAndSetIfChanged(ref activeElement, value);
        }

        private bool canClose;
        public bool CanClose {
            get => canClose;
            set => this.RaiseAndSetIfChanged(ref canClose, value);
        }

        public SettingsViewModel(bool canClose) => this.canClose = canClose;
    }
}
