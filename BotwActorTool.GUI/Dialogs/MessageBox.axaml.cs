using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Layout;
using System.Threading.Tasks;
using BotwActorTool.GUI.Extensions;
using Markdown.Avalonia;
using Microsoft.VisualBasic;
using BotwActorTool.GUI.Views;
using Avalonia.Threading;
using System.Threading;

namespace BotwActorTool.GUI.Dialogs
{
    public partial class MessageBox : Window
    {
        public MessageBox() => AvaloniaXamlLoader.Load(this);
        public MessageBox(string title, string text, Formatting formatting)
        {
            AvaloniaXamlLoader.Load(this);

            var tb = this.FindControl<TextBox>("Text")!;
            if (formatting == Formatting.Markdown) {
                tb.IsVisible = false;

                var mdViewer = this.FindControl<MarkdownScrollViewer>("Markdown")!;
                mdViewer.IsVisible = true;
                mdViewer.Markdown = text;
            }
            else {
                tb.Text = text;
            }

            this.FindControl<TextBlock>("TitleBox")!.Text = title;
            this.FindControl<Button>("Copy")!.Click += async (_, _) => {
                if (formatting == Formatting.Markdown) {
                    await Application.Current!.Clipboard!.SetTextAsync($"**{title}**\n\n{text}");
                    return;
                }

                await Application.Current!.Clipboard!.SetTextAsync($"**{title}**\n```\n{text}\n```");
            };
        }

        public static void ShowSync(string text, string title = "Notice", MessageBoxButtons buttons = MessageBoxButtons.Ok, Formatting formatting = Formatting.None)
        {
            using var source = new CancellationTokenSource();
            Show(text, title, buttons, formatting).ContinueWith(t => source.Cancel(), TaskScheduler.FromCurrentSynchronizationContext());
            Dispatcher.UIThread.MainLoop(source.Token);
        }

        public static async Task<MessageBoxResult> Show(string text, string title = "Notice", MessageBoxButtons buttons = MessageBoxButtons.Ok, Formatting formatting = Formatting.None)
        {
            MessageBox msgbox = new(title, text, formatting);
            var res = MessageBoxResult.Ok;

            var buttonPanel = msgbox.FindControl<StackPanel>("Buttons")!;
            var close = msgbox.FindControl<Button>("Close")!;

            close.Click += (_, __) => {
                res = MessageBoxResult.Cancel;
                msgbox.Close();
            };

            void AddBtn(string caption, MessageBoxResult r, bool def = false, int mode = 0)
            {
                var btn = new Button {
                    Content = caption,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0),
                    MinWidth = 67,
                    IsDefault = mode == 1,
                    IsCancel = mode == 2
                };

                btn.Click += (_, __) => {
                    res = r;
                    msgbox.Close();
                };

                buttonPanel.Children.Add(btn);
                if (def) res = r;
            }

            if (buttons == MessageBoxButtons.Ok || buttons == MessageBoxButtons.OkCancel)
                AddBtn("Ok", MessageBoxResult.Ok, true, 1);

            if (buttons == MessageBoxButtons.YesNo || buttons == MessageBoxButtons.YesNoCancel) {
                AddBtn("Yes", MessageBoxResult.Yes, mode: 1);
                AddBtn("No", MessageBoxResult.No, true);
            }

            if (buttons == MessageBoxButtons.OkCancel || buttons == MessageBoxButtons.YesNoCancel)
                AddBtn("Cancel", MessageBoxResult.Cancel, true, 2);


            var tcs = new TaskCompletionSource<MessageBoxResult>();
            msgbox.Closed += delegate { tcs.TrySetResult(res); };

            if (Shell != null) {
                await msgbox.ShowDialog(Shell);
            }
            else {
                msgbox.Show();
            }

            return await tcs.Task;
        }
    }
}
