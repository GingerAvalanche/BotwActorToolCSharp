using Avalonia.Controls;
using BotwActorTool.GUI.Controls;
using BotwActorTool.GUI.Extensions;
using BotwActorTool.GUI.Views.Editors;
using BotwActorTool.Lib;
using Dock.Model.ReactiveUI.Controls;
using Material.Icons;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.ViewModels
{
    public partial class AppViewModel : ReactiveObject
    {
        //
        // File

        public void OpenVanillaActor() => ToolDock.SetActive("VanillaFiles");

        public async void OpenMod()
        {
            var result = await new OpenFolderDialog() {
                Title = $"Select a mod 'content' folder"
            }.ShowAsync(View);

            if (result == null)
                return;

            bool found = true;
            foreach (var ending in new string[] { "content", "romfs" }) {
                found = result.EndsWith(ending);

                if (Directory.Exists($"{result}\\{ending}".ToSystemPath())) {
                    found = true;
                    result = $"{result}\\{ending}";
                }

                if (found) {
                    break;
                }
            }

            if (!found) {
                await View.ShowMessageBox("The selected folder is not a valid mod folder.", "Error");
                OpenMod();
                return;
            }

            SetStatus("Opening mod", MaterialIconKind.ArrowDecisionAuto);

            await Task.Run(() => SetModContext(result));
            ToolDock.SetActive("ModFiles");

            SetStatus();
        }

        public async void SaveActor()
        {
            if (CurrentActor == null) {
                return;
            }

            string? output = null;

            if (ModContext != null) {
                var result = await View.ShowMessageBox($"A mod is open in the current session. Would you like to save to '{ModContext}'?", "Notice", MessageBoxButtons.YesNoCancel);

                if (result == MessageBoxResult.Cancel) {
                    return;
                }
                else if (result == MessageBoxResult.Yes) {
                    output = ModContext;
                }
            }

            if (output == null) {
                var result = await new OpenFolderDialog() {
                    Title = $"Select a mod 'content' folder"
                }.ShowAsync(View);

                if (result == null)
                    return;

                if ((await View.ShowMessageBox($"Open this mod for future saving?", "Notice", MessageBoxButtons.YesNo)) == MessageBoxResult.Yes) {
                    SetModContext(result);
                }

                output = result;
            }

            SetStatus("Saving actor", MaterialIconKind.ContentSaveMoveOutline);

            await Task.Run(() => CurrentActor.Actor.Write(output!));

            SetStatus();
        }

        public async void Quit()
        {
            if (IsEdited) {
                var results = await View.ShowMessageBox("You have unsaved changes. Are you sure you wish to exit?", "Warning", MessageBoxButtons.YesNoCancel);
                if (results != MessageBoxResult.Yes) {
                    return;
                }
            }

            Environment.Exit(1);
        }


        // 
        // Tools

        public void Temp_PlaceHolder() => View.ShowMessageBox("A placeholder action was executed!", "Notice");
        public void Settings() => SettingsView = new(View);


        // 
        // About

        public void Help()
        {
            // open help wiki / help messgae (?)
        }

        public async void Credits()
        {
            await View.ShowMessageBox(File.ReadAllText("./Assets/Credits.md"), "Credits", formatting: Formatting.Markdown);
        }


        //
        // Helpers

        public void SetModContext(string folder)
        {
            ModContext = folder;
            (ToolDock.Get("ModFiles") as BrowserViewModel)!.SetRoot(folder, true);
        }

        public void SetActorFileContext(ActorViewModel doc)
        {
            Dictionary<string, string> root = new();
            var linkInfo = Resource.GetDynamic<Dictionary<string, Dictionary<string, dynamic>>>("LinkInfo")!;

            foreach ((var link, var data) in linkInfo) {
                try {
                    Dictionary<string, string>? editors = JsonSerializer.Deserialize<Dictionary<string, string>?>(data["Editors"]);
                    if (editors != null) {

                        string linkName = ((JsonElement)data["Name"]).GetString()!;
                        string tip = data["Tip"].ToString();

                        bool? isNotDummy = null;
                        try {
                            isNotDummy = doc.Actor.GetLink(link) != "Dummy";
                        } catch { }

                        if (isNotDummy == null || isNotDummy == true) {
                            doc.ActorFiles.Add(linkName);
                        }

                        root.Add(linkName + (isNotDummy == null || isNotDummy == true ? "" : " (Dummy)"), string.IsNullOrEmpty(tip) ? link : tip); 
                        doc.AllEditors.Add(linkName, new());

                        foreach ((var name, var editor) in editors) {
                            doc.AllEditors[linkName].Add(name, (ActorEditor)Type.GetType("BotwActorTool.GUI.Views.Editors." + editor)!.GetConstructor(Type.EmptyTypes)!.Invoke(Array.Empty<object>()));
                        }

                        doc.LinkKeys.Add(linkName, link);
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine($"Failed to add editor to {link} - {ex}");
                }
            }

            (ToolDock.Get("Actor") as BrowserViewModel)!.SetRoot(root, true);
        }
    }
}
