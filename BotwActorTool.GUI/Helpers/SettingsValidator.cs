using Avalonia;
using Avalonia.Themes.Fluent;
using BotwActorTool.GUI.Extensions;
using BotwActorTool.Lib;
using BotwActorTool.Lib.Attributes;
using SharpYaml.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BotwActorTool.GUI.Helpers
{
    public class SettingsValidator
    {
        public static bool? Validate(string? path, string name)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            return name switch {
                "GameDir" => File.Exists($"{path}/Pack/Dungeon000.pack") && path.EndsWith("content"),
                "UpdateDir" => File.Exists($"{path}/Actor/Pack/ActorObserverByActorTagTag.sbactorpack") && path.EndsWith("content"),
                "DlcDir" => File.Exists($"{path}/Pack/AocMainField.pack") && path.EndsWith("content\\0010"),
                "GameDirNx" => File.Exists($"{path}/Actor/Pack/ActorObserverByActorTagTag.sbactorpack") && File.Exists($"{path}/Pack/Dungeon000.pack") && path.EndsWith("romfs"),
                "DlcDirNx" => File.Exists($"{path}/Pack/AocMainField.pack") && path.EndsWith("romfs"),
                "Lang" => File.Exists($"{Config.GetDir(BotwDir.Base)}/Pack/Bootup_{path}.pack"),
                "Theme" => ValidateTheme(path, name),
                _ => null,
            };
        }

        public static bool? ValidateTheme(string value, string _2)
        {
            Theme.Mode = value == "Dark" ? FluentThemeMode.Dark : FluentThemeMode.Light;
            Application.Current!.Styles[0] = Theme;
            return null;
        }

        public static KeyValuePair<bool, string> ValidateSave()
        {
            Dictionary<string, bool?> validator = new();
            foreach (var prop in Config.GetType().GetProperties().Where(x => x.GetCustomAttributes<SettingAttribute>(false).Any())) {
                validator.Add(prop.Name, Validate(prop.GetValue(Config) as string, prop.Name));
            }
            return ValidateSave(validator);
        }

        public static KeyValuePair<bool, string> ValidateSave(Dictionary<string, bool?> values)
        {
            if (values["GameDir"] == false) {
                return new(false, "The WiiU game path is invalid.\nPlease correct or delete it before saving.");
            }

            if (values["UpdateDir"] == false) {
                return new(false, "The WiiU update path is invalid.\nPlease correct or delete it before saving.");
            }

            if (values["DlcDir"] == false) {
                return new(false, "The WiiU DLC path is invalid.\nPlease correct or delete it before saving.");
            }

            if (values["GameDirNx"] == false && values["GameDir"] == false) {
                return new(false, "The Switch game/update path is invalid.\nPlease correct or delete it before saving.");
            }

            if (values["DlcDirNx"] == false) {
                return new(false, "The Switch DLC path is invalid.\nPlease correct or delete it before saving.");
            }

            if (values["GameDir"] == null && values["UpdateDir"] == null && values["GameDirNx"] == null) {
                return new(false, "No game path has been set for Switch or WiiU.\nPlease set one of them before saving.");
            }

            if (values["GameDir"] == true && values["UpdateDir"] == null) {
                return new(false, "The WiiU update path has not been set.\nPlease set it before saving.");
            }

            if (values["Lang"] == false) {
                return new(false, "The selected langauage/region is not abailible in your game version. Please choose another langauge/region.");
            }

            return new(true, null!);
        }

        public static async Task<string?> Setter(string title, string name)
        {
            return name switch {
                _ => await BrowserDialog.Folder.BrowseDialog(title),
            };
        }
    }
}
