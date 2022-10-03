#pragma warning disable CA1826 // Do not use Enumerable methods on indexable collections

using Avalonia.Controls;
using Avalonia.Dialogs;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;
using BotwActorTool.GUI.Dialogs;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.GUI.Views;
using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;
using Dock.Model.ReactiveUI.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.Extensions
{

    public enum MessageBoxButtons { Ok, OkCancel, YesNo, YesNoCancel }
    public enum MessageBoxResult { Cancel, No, Ok, Yes }
    public enum Formatting { None, Markdown }
    public enum BrowserDialog { File, Folder, SaveFile }

    public static class ViewExt
    {
        private static IStorageFolder? LastSelectedDirectory;
        private static IStorageFolder? LastSaveDirectory;

        public static Task<MessageBoxResult> ShowMessageBox(this Window _, string text, string title = "Warning", MessageBoxButtons buttons = MessageBoxButtons.Ok,
            Formatting formatting = Formatting.None) => MessageBox.Show(text, title, buttons, formatting);

        public static async Task<string?> BrowseDialog(this BrowserDialog browser, string title = "")
        {
            string? path = null;

            if (browser == BrowserDialog.Folder) {
                var result = await (View.GetVisualRoot() as TopLevel)!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions() {
                    Title = title,
                    SuggestedStartLocation = LastSelectedDirectory
                });

                IStorageItem? item = result.FirstOrDefault() is IStorageItem _item ? _item : null;
                if (item != null) {
                    path = item.TryGetUri(out Uri? uri) ? uri.ToString() : item.Name;
                    LastSelectedDirectory = item as IStorageFolder;
                }
            }
            else if (browser == BrowserDialog.File) {
                var result = await (View.GetVisualRoot() as TopLevel)!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions() {
                    Title = title,
                    SuggestedStartLocation = LastSelectedDirectory
                });

                IStorageItem? item = result.FirstOrDefault() is IStorageItem _item ? _item : null;
                if (item != null) {
                    path = item.TryGetUri(out Uri? uri) ? uri.ToString() : item.Name;
                    LastSelectedDirectory = await item.GetParentAsync();
                }
            }
            else {
                var result = await (View.GetVisualRoot() as TopLevel)!.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions() {
                    Title = title,
                    SuggestedStartLocation = LastSaveDirectory
                });
                path = result != null ? result.TryGetUri(out Uri? uri) ? uri.ToString() : result.Name : null;
                LastSaveDirectory = result != null ? await result.GetParentAsync() : null;
            }

            return path;
        }

        public static void SetActive(this DockBase dock, string id)
        {
            var doc = dock.VisibleDockables?.Where(x => x.Id == id).FirstOrDefault();
            dock.ActiveDockable = doc;
        }

        public static IDockable? Get(this DockBase dock, string id)
        {
            return dock.VisibleDockables?.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
