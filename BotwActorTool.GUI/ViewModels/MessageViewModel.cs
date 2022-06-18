#pragma warning disable CS8600
#pragma warning disable CS8601

using BotwActorTool.GUI.ViewThemes.App;
using MaterialDesignThemes.Wpf;
using Stylet;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace BotwActorTool.GUI.ViewModels
{
    public class MessageViewModel : Screen, INotifyPropertyChanged
    {
        #region Actions

        public void Yes() => RequestClose(false);

        public void No() => RequestClose(true);

        public void Copy() => Clipboard.SetText($"**{Title}**\n> {Message}");

        #endregion

        #region Props

        private string _title = "Notice";
        public string Title
        {
            get => _title;
            set => SetAndNotify(ref _title, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetAndNotify(ref _message, value);
        }

        private Brush _foreground = new SolidColorBrush(SysTheme.ITheme.Body);
        public Brush Foreground
        {
            get => _foreground;
            set => SetAndNotify(ref _foreground, value);
        }

        private string _buttonRight = "Ok";
        public string ButtonRight
        {
            get { return _buttonRight; }
            set => SetAndNotify(ref _buttonRight, value);
        }

        private string _buttonLeft = "Yes";
        public string ButtonLeft
        {
            get { return _buttonLeft; }
            set => SetAndNotify(ref _buttonLeft, value);
        }

        private Visibility _buttonLeftVisibility = Visibility.Collapsed;
        public Visibility ButtonLeftVisibility
        {
            get => _buttonLeftVisibility;
            set => SetAndNotify(ref _buttonLeftVisibility, value);
        }

        private double _width = 220;
        public double Width
        {
            get { return _width; }
            set => SetAndNotify(ref _width, value);
        }

        #endregion

        public MessageViewModel(string message, string title = "Notice", bool isOption = false, string? messageColor = null, double width = 220, string yesButtonText = "Yes", string noButtonText = "Auto")
        {
            Message = message;
            Title = title;
            Width = width;
            ButtonRight = noButtonText == "Auto" ? "Ok" : noButtonText;
            ButtonLeft = yesButtonText;

            if (isOption)
            {
                ButtonLeftVisibility = Visibility.Visible;
                ButtonRight = noButtonText == "Auto" ? "No" : noButtonText;
            }

            if (messageColor != null)
                Foreground = (Brush)new BrushConverter().ConvertFromString(messageColor);
        }
    }
}
