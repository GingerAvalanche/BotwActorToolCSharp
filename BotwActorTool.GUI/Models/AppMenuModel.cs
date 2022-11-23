using Avalonia.Controls;
using Avalonia.Generics.Dialogs;
using Avalonia.MenuFactory.Attributes;
using BotwActorTool.GUI.Builders;
using BotwActorTool.GUI.Extensions;
using BotwActorTool.GUI.ViewModels.Tools;
using BotwActorTool.GUI.Views;
using BotwActorTool.Lib;
using Dock.Serializer;
using Material.Icons;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.Models
{
    public class AppMenuModel
    {
        //
        // File

        [Menu("Open Mod", "_File", Icon = MaterialIconKind.FolderOpen, HotKey = "Ctrl + N")]
        public static async void OpenMod()
        {
            string? result = await new BrowserDialog(BrowserMode.OpenFolder, "Select a mod 'content' folder", instanceBrowserKey: "OpenMod").ShowDialog();

            if (result != null) {
                bool found = true;
                foreach (var contentFolder in new string[] { "content", "romfs" }) {
                    found = result.EndsWith(contentFolder);
                    if (Directory.Exists($"{result}/{contentFolder}")) {
                        result += $"/{contentFolder}";
                        found = true;
                    }

                    if (found) break;
                }

                if (!found) {
                    await MessageBox.ShowDialog("The selected folder is not a valid mod folder.", "Error");
                    OpenMod();
                    return;
                }

                SetStatus("Opening Mod", MaterialIconKind.FolderArrowUpOutline);

                try {
                    FileBrowserViewModel browser = null!;
                    await Task.Run(() => browser = new FileBrowserViewModel() {
                        ItemsBase = ActorInfoExtension.LoadActorInfoNodes(result, Config.GetDir(BotwDir.Update))
                    });
                    ToolDockFactory.SetDock("ModActors", browser);
                    ToolDockFactory.SetActive("ModActors");
                    ShellViewModel.ModContext = result;
                }
                catch (Exception ex) {
                    await MessageBox.ShowDialog($"An error occured while opening the mod '{result}':\n\n{ex}", "Error");
                }
                finally {
                    SetStatus("Ready");
                }
            }
        }

        [Menu("Open Actor", "_File", Icon = MaterialIconKind.BoxOutline, HotKey = "Ctrl + O")]
        public static void OpenVanillaActor()
        {
            SetStatus("Loading");
            // ToolDock.SetActive("VanillaFiles");
        }

        [Menu("Save Actor", "_File", Icon = MaterialIconKind.ContentSave, HotKey = "Ctrl + S", IsSeparator = true)]
        public static async void SaveActor()
        {
            // if (CurrentActor == null) {
            //     return;
            // }

            // string? output = null;

            // if (ModContext != null) {
            //     var result = await View.ShowMessageBox($"A mod is open in the current session. Would you like to save to '{ModContext}'?", "Notice", MessageBoxButtons.YesNoCancel);

            //     if (result == MessageBoxResult.Cancel) {
            //         return;
            //     }
            //     else if (result == MessageBoxResult.Yes) {
            //         output = ModContext;
            //     }
            // }

            // if (output == null) {
            //     var result = await BrowserDialog.Folder.BrowseDialog("Select a mod 'content' folder");

            //     if (result == null)
            //         return;

            //     if ((await View.ShowMessageBox($"Open this mod for future saving?", "Notice", MessageBoxButtons.YesNo)) == MessageBoxResult.Yes) {
            //         SetModContext(result);
            //     }
            //     output = result;
            // }

            // SetStatus("Saving actor", MaterialIconKind.ContentSaveMoveOutline);
            // await Task.Run(() => CurrentActor.Actor.Write(output!));
            // SetStatus();
        }

        [Menu("Quit", "_File", Icon = MaterialIconKind.ExitToApp, HotKey = "Ctrl + Q", IsSeparator = true)]
        public static async void Quit()
        {
            if (ShellViewModel.IsEdited) {
                if (await MessageBox.ShowDialog("You may have unsaved changes. Are you sure you wish to exit?", "Warning", DialogButtons.YesNo) != DialogResult.Yes) {
                    return;
                }
            }

            new DockSerializer(typeof(ObservableCollection<>)).Save($"{Config.DataFolder}/layout.idock", ViewModel.Layout);
            Environment.Exit(1);
        }

        // 
        // Tools

        [Menu("Reset Layout", "_Tools", Icon = MaterialIconKind.Restart)]
        public static void ResetLayout()
        {
            if (File.Exists($"{Config.DataFolder}/layout.idock")) {
                File.Delete($"{Config.DataFolder}/layout.idock");
            }

            DockFactory factory = new(ViewModel);
            factory.CreateLayout();
            factory.InitLayout(ViewModel.Layout);
        }

        [Menu("Settings", "_Tools", Icon = MaterialIconKind.CogBox, IsSeparator = true)]
        public static void Settings() => ShellViewModel.Content = new SettingsView(canCancel: true);

        // 
        // About

        [Menu("Help", "_About", Icon = MaterialIconKind.HelpOutline)]
        public static void Help()
        {
            // open help wiki / help messgae (?)
            Debug.WriteLine(ToolDockModel.VanillaActors().Title);
        }

        [Menu("Credits", "_About", Icon = MaterialIconKind.PersonCheck)]
        public static async void Credits()
        {
            await MessageBox.ShowDialog(File.ReadAllText("./Assets/Credits.md"), "Credits", formatting: Formatting.Markdown);
        }

        //
        // Helpers (Shouldn't really be in this file...)

        //public void SetModContext(string folder)
        //{
        //    ModContext = folder;
        //    (ToolDock.Get("ModFiles") as BrowserViewModel)!.SetRoot(folder, true);
        //}

        //public void SetActorFileContext(ActorViewModel doc)
        //{
        //    Dictionary<string, string> root = new();
        //    var linkInfo = Resource.GetDynamic<Dictionary<string, Dictionary<string, dynamic>>>("LinkInfo")!;

        //    foreach ((var link, var data) in linkInfo) {
        //        try {
        //            Dictionary<string, string>? editors = JsonSerializer.Deserialize<Dictionary<string, string>?>(data["Editors"]);
        //            if (editors != null) {

        //                string linkName = ((JsonElement)data["Name"]).GetString()!;
        //                string tip = data["Tip"].ToString();

        //                bool? isNotDummy = null;
        //                try {
        //                    isNotDummy = doc.Actor.GetLink(link) != "Dummy";
        //                }
        //                catch { }

        //                if (isNotDummy == null || isNotDummy == true) {
        //                    doc.ActorFiles.Add(linkName);
        //                }

        //                root.Add(linkName + (isNotDummy == null || isNotDummy == true ? "" : " (Dummy)"), string.IsNullOrEmpty(tip) ? link : tip);
        //                doc.AllEditors.Add(linkName, new());

        //                foreach ((var name, var editor) in editors) {
        //                    doc.AllEditors[linkName].Add(name, (ActorEditor)Type.GetType("BotwActorTool.GUI.Views.Editors." + editor)!.GetConstructor(Type.EmptyTypes)!.Invoke(Array.Empty<object>()));
        //                }

        //                doc.LinkKeys.Add(linkName, link);
        //            }
        //        }
        //        catch (Exception ex) {
        //            Debug.WriteLine($"Failed to add editor to {link} - {ex}");
        //        }
        //    }

        //    (ToolDock.Get("Actor") as BrowserViewModel)!.SetRoot(root, true);
        //}
    }
}
