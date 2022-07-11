using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Layout;
using System.Threading.Tasks;
using BotwActorTool.GUI.Extensions;

namespace BotwActorTool.GUI.Views
{
    public partial class MessageBox : Window
    {
        public MessageBox() => AvaloniaXamlLoader.Load(this);
        public MessageBox(string title, string text)
        {
            AvaloniaXamlLoader.Load(this);

            this.FindControl<TextBlock>("Text").Text = text;
            this.FindControl<TextBlock>("TitleBox").Text = title;
            this.FindControl<Button>("Copy").Click += async (_, _) => {
                if (Application.Current != null && Application.Current.Clipboard != null)
                    await Application.Current.Clipboard.SetTextAsync($"**{title}**\n```\n{text}\n```");
            };
        }

        public static Task<MessageBoxResult> Show(string text, string title = "Notice", MessageBoxButtons buttons = MessageBoxButtons.Ok, Window? parent = null)
        {
            MessageBox msgbox = new(title, text);
            var res = MessageBoxResult.Ok;

            var buttonPanel = msgbox.FindControl<StackPanel>("Buttons");

            var close = msgbox.FindControl<Button>("Close");
            close.Click += (_, __) => {
                res = MessageBoxResult.Cancel;
                msgbox.Close();
            };

            void AddBtn(string caption, MessageBoxResult r, bool def = false)
            {
                var btn = new Button {
                    Content = caption,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(10,0,0,0),
                    MinWidth = 67
                };

                btn.Click += (_, __) => {
                    res = r;
                    msgbox.Close();
                };

                buttonPanel.Children.Add(btn);
                if (def) res = r;
            }

            if (buttons == MessageBoxButtons.Ok || buttons == MessageBoxButtons.OkCancel)
                AddBtn("Ok", MessageBoxResult.Ok, true);

            if (buttons == MessageBoxButtons.YesNo || buttons == MessageBoxButtons.YesNoCancel) {
                AddBtn("Yes", MessageBoxResult.Yes);
                AddBtn("No", MessageBoxResult.No, true);
            }

            if (buttons == MessageBoxButtons.OkCancel || buttons == MessageBoxButtons.YesNoCancel)
                AddBtn("Cancel", MessageBoxResult.Cancel, true);


            var tcs = new TaskCompletionSource<MessageBoxResult>();
            msgbox.Closed += delegate { tcs.TrySetResult(res); };

            if (parent != null)
                msgbox.ShowDialog(parent);
            else msgbox.Show();

            return tcs.Task;
        }
    }
}
