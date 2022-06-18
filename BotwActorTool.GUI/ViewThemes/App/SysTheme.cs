#pragma warning disable CS8604

using BotwActorTool.GUI.ViewResources.Helpers;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Stylet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;

namespace BotwActorTool.GUI.ViewThemes.App
{
    public static class SysTheme
    {
        private static Color Color(this string color) => (Color)ColorConverter.ConvertFromString(color);
        private static Color? GetColor(this object? color) => color is ColorPair pair ? pair.Color : color as Color?;

        private static PropertyInfo[] Elements { get; } = typeof(ITheme).GetProperties();
        private static PaletteHelper Helper { get; set; } = new();

        public static ITheme ITheme => Helper.GetTheme();
        public static string Folder { get; set; } = $"{Environment.GetEnvironmentVariable("LOCALAPPDATA")}\\BotwActorTool\\Themes";
        public static string Default { get; set; } = "System";
        public static string Last { get; set; } = "System";
        public static string Name { get; set; } = "System";

        /// <summary>
        /// Collects a BindableCollection of KeyValuePairs with the common theme brush names and color values.
        /// </summary>
        /// <returns></returns>
        public static BindableCollection<KeyValuePair<string, Color>> GetCommonBrushes()
        {
            BindableCollection<KeyValuePair<string, Color>> defaults = new();

            // Create a collection of common brush names
            List<string> common = new() {
                "PrimaryDark",
                "PrimaryMid",
                "PrimaryLight",
                "SecondaryDark",
                "SecondaryMid",
                "SecondaryLight",
                "Background",
                "Paper",
                "Body",
                "BodyLight",
                "ToolTipBackground",
                "ToolForeground",
                "ValidationError"
            };

            // Iterate the ITheme elements
            foreach (PropertyInfo prop in Elements) {

                // Add the brsuh name and color value
                Color? color = prop.GetValue(ITheme).GetColor();
                if (color != null && common.Contains(prop.Name)) {
                    defaults.Add(new(prop.Name, color ?? new()));
                }
            }

            return defaults;
        }

        /// <summary>
        /// Updates the specified keyValuePair <paramref name="brushName"/> inside the <paramref name="collection"/> and returns the updated KeyValuePair.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="brushName"></param>
        /// <param name="colorValue"></param>
        /// <returns></returns>
        public static KeyValuePair<string, Color> UpdateBrush(BindableCollection<KeyValuePair<string, Color>> collection, string brushName, Color colorValue)
        {
            for (int i = 0; i < collection.Count; i++) {
                if (collection[i].Key == brushName) {
                    collection[i] = new(brushName, colorValue);
                    return collection[i];
                }
            }

            return new();
        }

        /// <summary>
        /// Sets the <paramref name="brushName"/> theme brush to the specified <paramref name="colorValue"/>
        /// </summary>
        /// <param name="brushName"></param>
        /// <param name="colorValue"></param>
        public static void SetBrush(string brushName, Color colorValue)
        {
            foreach (PropertyInfo property in Elements) {
                if (property.Name == brushName) {

                    object? value = property.GetValue(ITheme);

                    if (value is Color) {
                        property.SetValue(ITheme, colorValue);
                    }
                    else if (value is ColorPair) {
                        property.SetValue(ITheme, new ColorPair(colorValue));
                    }
                }
            }

            Helper.SetTheme(ITheme);
        }

        /// <summary>
        /// Load the application ITheme from a JSON theme resource.
        /// </summary>
        /// <param name="themeName"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public static string Load(string? themeName = null)
        {
            themeName ??= Name;

            if (!File.Exists($"{Folder}\\{themeName}.json")) {
                Save();
            }

            byte[] bytes = File.ReadAllBytes($"{Folder}\\{themeName}.json");
            Dictionary<string, string> lTheme = JsonSerializer.Deserialize<Dictionary<string, string>>(bytes) ?? new();

            foreach (PropertyInfo property in Elements) {

                object? value = property.GetValue(ITheme);

                if (value is Color) {
                    property.SetValue(ITheme, lTheme[property.Name].Color());
                }
                else if (value is ColorPair) {
                    property.SetValue(ITheme, new ColorPair(lTheme[property.Name].Color()));
                }
            }

            Last = Name;
            Name = themeName;
            Helper.SetTheme(ITheme);

            return Name;
        }

        /// <summary>
        /// Create or overwrite a theme resource using the defined <paramref name="themeName"/>
        /// <para>The user will not be prompted to keep the original theme resource</para>
        /// </summary>
        /// <param name="themeName"></param>
        public static void Save(string? themeName = null) => Save(themeName ?? Name, false);

        /// <summary>
        /// Create or overwrite a theme resource using the defined <paramref name="themeName"/>
        /// </summary>
        /// <param name="themeName"></param>
        public static bool Save(string themeName, bool ask = true, IWindowManager? winMgr = null)
        {
            Dictionary<string, string> theme = new();

            if (!Directory.Exists(Folder)) {
                Directory.CreateDirectory(Folder);
            }

            if (File.Exists($"{Folder}\\{themeName}.json") && ask) {

                if (winMgr != null) {
                    if (!winMgr.Show($"The theme file {themeName} already exists.\nOverwrite it?", "Warning", true, width: 240))
                        return false;
                }
                else {
                    if (MessageBox.Show($"The theme file {themeName} already exists.\nOverwrite it?", "Warning", MessageBoxButton.YesNo)
                        != MessageBoxResult.Yes) return false;
                }
            }

            foreach (PropertyInfo property in Elements) {

                // Add the color as a hex value
                Color? color = property.GetValue(ITheme).GetColor();
                theme.Add(property.Name, color.ToString());
            }

            // Write the serilaized theme data
            Directory.CreateDirectory(Folder);
            File.WriteAllText($"{Folder}\\{themeName}.json", JsonSerializer.Serialize(theme, new JsonSerializerOptions { WriteIndented = true }));

            return true;
        }

        /// <summary>
        /// Get a BindableCollection of the existing theme names.
        /// </summary>
        /// <returns></returns>
        public static BindableCollection<string> GetThemes(bool adddNew = false)
        {
            BindableCollection<string> themes = new();

            if (!Directory.Exists(Folder)) {
                Directory.CreateDirectory(Folder);
            }

            foreach (var file in Directory.EnumerateFiles(Folder)) {
                FileInfo fInfo = new(file);
                if (fInfo.Extension == ".json") {
                    themes.Add(fInfo.Name.Replace(fInfo.Extension, ""));
                }
            }

            if (adddNew) {
                themes.Add("New. . .");
            }

            return themes;
        }
    }
}
