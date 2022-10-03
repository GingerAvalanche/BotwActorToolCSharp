using Avalonia.Controls.Primitives;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotwActorTool.GUI.ViewModels;
using BotwActorTool.Lib.Attributes;
using System.Reflection;
using BotwActorTool.GUI.Dialogs;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia;
using BotwActorTool.GUI.Extensions;
using System.IO;
using BotwActorTool.Lib;

namespace BotwActorTool.GUI.Helpers
{
    public class SettingsFactory
    {
        public static SettingsFactory Generate(UserControl view) => new(view);

        public UserControl View { get; set; }

        private readonly Dictionary<string, Control> Settings = new();
        private readonly Dictionary<string, StackPanel> Panels = new();
        private readonly Dictionary<string, StackPanel> Folders = new();
        private readonly List<string> Categories = new();
        private StackPanel Root = new();
        private ToggleButton? DefaultButton = null;
        private ToggleButton? LastButton = null;

        private SettingsViewModel Binding => (View.DataContext as SettingsViewModel)!;
        public SettingsFactory(UserControl view)
        {
            View = view;

            Root = View.FindControl<StackPanel>("Root")!;

            var settings = Config.GetType()!.GetProperties().Where(x => x.GetCustomAttributes<SettingAttribute>(false).Any());

            foreach (var prop in settings) {
                CreateElement(prop);
            }

            View.FindControl<Button>("Save")!.Click += SaveClick;
            View.FindControl<Button>("Cancel")!.Click += CancelClick;
        }

        public async void SaveClick(object? sender, EventArgs e)
        {
            Dictionary<string, bool?> validator = new();
            foreach ((var name, var value) in Settings) {
                validator.Add(name, SettingsValidator.Validate(GetElement(value)?.ToString()!, name));
            }

            var check = SettingsValidator.ValidateSave(validator);
            if (!check.Key) {
                await MessageBox.Show(check.Value, "Error");
                return;
            }
            else {
                foreach ((var name, var value) in Settings) {
                    Config.GetType().GetProperty(name)?.SetValue(Config, GetElement(value));
                }
            }

            Config.RequiresInput = false;
            Config.Save();
            ViewModel.SettingsView = null;
            ViewModel.SetStatus(isLoading: false);
        }

        public void CancelClick(object? sender, EventArgs e) => ViewModel.SettingsView = null;

        public object? GetElement(Control control)
        {
            if (control is TextBox textBox) {
                return textBox.Text;
            }
            else if (control is ComboBox comboBox) {
                return (comboBox.SelectedItem as ComboBoxItem)?.Tag;
            }
            else if (control is ToggleSwitch toggle) {
                return toggle.IsChecked;
            }
            else {
                throw new NotImplementedException($"Parsing controls of type '{control.GetType().Name}' are not supported.");
            }
        }

        private void CreateElement(PropertyInfo property)
        {
            var setting = property.GetCustomAttribute<SettingAttribute>()!;
            var folder = setting.Folder.Replace(" ", "_");
            var category = setting.Category.Replace(" ", "_");

            // Add panel
            string key = $"{folder}.{category}";
            if (!Panels.ContainsKey(key)) {
                Panels.Add(key, CreatePanel(category));
            }

            // Add folder
            var folderPanel = Folders.ContainsKey(folder) ? Folders[folder] : null;
            if (folderPanel == null) {

                folderPanel = new();

                TextBlock titleBlock = new() {
                    Text = setting.Folder,
                    FontWeight = FontWeight.Bold,
                    Foreground = "#afafaf".ToBrush(), // Load dynamic resource
                    Margin = new(8, 30, 0, 12)
                };

                Border splitter = new() {
                    Background = "#5f5f5f".ToBrush(), // Load dynamic resource
                    Height = 1,
                    Margin = new(5, 0, 5, 5)
                };

                folderPanel.Children.Add(titleBlock);
                folderPanel.Children.Add(splitter);
                Root.Children.Add(folderPanel);
                Folders.Add(folder, folderPanel);
            }

            // Add category
            if (!Categories.Contains(category)) {

                ToggleButton categoryButton = new() {
                    Name = category,
                    Content = category,
                    Margin = new(0, 2, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = "#00000000".ToBrush()
                };

                categoryButton.Click += (s, e) => {
                    if (categoryButton.IsChecked == false) {
                        categoryButton.IsChecked = !categoryButton.IsChecked;
                    }
                    else {
                        if (LastButton != null) LastButton.IsChecked = false;
                        LastButton = categoryButton;
                        Binding.ActiveElement = Panels[$"{folder}.{category}"];
                    }
                };

                LastButton ??= categoryButton;
                if (DefaultButton == null) {
                    DefaultButton = categoryButton;
                    DefaultButton.IsChecked = true;
                    Binding.ActiveElement = Panels[$"{folder}.{category}"];
                }

                folderPanel.Children.Add(categoryButton);
                Categories.Add(category);
            }

            var element = setting.UiType switch {
                UiType.Default =>
                    CreateElement(property.Name, property.GetValue(Config)!, setting.Name, setting.Description),
                UiType.TextBox =>
                    CreateTextElement(property.Name, property.GetValue(Config) as string, setting.Name ?? property.Name, setting.Description),
                UiType.Dropdown =>
                    CreateDropdownElement(property.Name, property.GetValue(Config) as string, setting.Name ?? property.Name, setting.Description, setting.DropdownElements),
                UiType.Toggle =>
                    CreateToggleElement(property.Name, (bool)property.GetValue(Config)!, setting.Name ?? property.Name, setting.Description),
                _ => throw new NotImplementedException()
            };

            Panels[key].Children.Add(element);
        }

        private Border CreateElement(string propertyName, object property, string? name, string? description)
        {
            if (property is bool boolean) {
                return CreateToggleElement(propertyName, boolean, name ?? propertyName, description);
            }
            else {
                return CreateTextElement(propertyName, property.ToString(), name ?? propertyName, description);
            }
        }

        private Border CreateTextElement(string propertyName, string? value, string name, string? description)
        {
            Border root = CreateGridElement(name, description);
            Grid grid = (Grid)root.Child!;

            TextBox element = new() {
                VerticalAlignment = VerticalAlignment.Top
            };
            element.GetObservable(TextBox.TextProperty).Subscribe(text => ((Border)grid.Children[0]).Background = SettingsValidator.Validate(text, propertyName).ToBrush());
            element.Text = value;
            Grid.SetColumn(element, 3);

            Button browse = new() {
                Content = "...",
                Height = 32,
                Width = 32,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top
            };
            browse.Click += async (s, e) => element.Text = await SettingsValidator.Setter(name, propertyName) ?? element.Text;
            Grid.SetColumn(browse, 4);

            grid.Children.Add(element);
            grid.Children.Add(browse);
            Settings.Add(propertyName, element);
            return root;
        }

        private Border CreateDropdownElement(string propertyName, string? value, string name, string? description, string[]? elements)
        {
            List<ComboBoxItem> items = new();
            int index = 0;

            if (elements != null) {
                for (int i = 0; i < elements.Length; i++) {

                    if (elements[i].Contains(':')) {
                        var resource = Resource.GetDynamic<Dictionary<string, string>>(elements[i].Split(':')[1]) ?? new();
                        int pos = 0;
                        foreach ((var _tag, var _value) in resource) {
                            items.Add(new() { Content = _value, Tag = _tag });
                            if (_tag == value) index = pos;
                            pos++;
                        }
                    }
                    else {
                        items.Add(new() { Content = elements[i], Tag = elements[i] });
                        if (elements[i] == value) {
                            index = i;
                        }
                    }
                }
            }

            Border root = CreateGridElement(name, description);
            Grid grid = (Grid)root.Child!;

            ComboBox element = new() {
                Items = items,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Padding = new Thickness(10,1,0,0),
            };
            element.SelectionChanged += (s, e) => ((Border)grid.Children[0]).Background = SettingsValidator.Validate((element.SelectedItem as ComboBoxItem)?.Tag!.ToString(), propertyName).ToBrush();
            element.SelectedIndex = index;
            Grid.SetColumn(element, 3);
            Grid.SetColumnSpan(element, 2);

            grid.Children.Add(element);
            Settings.Add(propertyName, element);
            return root;
        }

        private Border CreateToggleElement(string propertyName, bool value, string name, string? description)
        {
            ToggleSwitch element = new() {
                IsChecked = value,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                OnContent = "",
                OffContent = ""
            };
            Grid.SetColumn(element, 3);
            Grid.SetColumnSpan(element, 2);

            Border root = CreateGridElement(name, description);
            Grid grid = (Grid)root.Child!;

            grid.Children.Add(element);
            Settings.Add(propertyName, element);
            return root;
        }

        private StackPanel CreatePanel(string title)
        {
            StackPanel panel = new() {
                Margin = new Thickness(40, 30)
            };

            TextBlock titleBlock = new() {
                Margin = new(0, 0, 0, 25),
                FontSize = 20,
                FontWeight = FontWeight.Medium,
                Text = title
            };

            panel.Children.Add(titleBlock);
            return panel;
        }

        private Border CreateGridElement(string name, string? description)
        {
            Border root = new() {
                CornerRadius = new(5),
                Background = "#403f3f3f".ToBrush(), // Load dynamic resource
                Padding = new(10),
                Margin = new(0, 0, 0, 10)
            };

            Grid child = new() {
                ColumnDefinitions = new("6,*,10,1.2*,37")
            };

            StackPanel texts = new();
            Grid.SetColumn(texts, 1);

            Border validation = new() {
                CornerRadius = new(1),
                Width = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new(-2, 0, 0, 0),
                Background = "#00000000".ToBrush()
            };

            TextBlock nameBlock = new() {
                Text = name,
                MaxWidth = 880,
                TextWrapping = TextWrapping.WrapWithOverflow,
                Margin = new(0, 0, 0, 5)
            };

            TextBlock descBlock = new() {
                Text = description,
                MaxWidth = 880,
                TextWrapping = TextWrapping.WrapWithOverflow,
                VerticalAlignment = VerticalAlignment.Bottom,
                Opacity = 0.5,
                FontWeight = FontWeight.Light,
                FontSize = 11
            };

            texts.Children.Add(nameBlock);
            texts.Children.Add(descBlock);
            child.Children.Add(validation);
            child.Children.Add(texts);
            root.Child = child;

            return root;
        }
    }
}
