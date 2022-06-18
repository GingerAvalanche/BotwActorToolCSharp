using BotwActorTool.GUI.ViewResources.Helpers;
using BotwActorTool.GUI.ViewResources.Vector;
using BotwActorTool.GUI.ViewThemes.App;
using Stylet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BotwActorTool.GUI.ViewModels
{
    public class ThemeViewModel : Screen
    {
        private bool brushSaved = false;

        public LinkHW LinkHWVector { get; set; } = new();

        private SettingsViewModel Settings { get; set; }
        private IWindowManager? WindowManager { get; set; }

        private Visibility _canEdit = Visibility.Collapsed;
        public Visibility CanEdit {
            get => _canEdit;
            set => SetAndNotify(ref _canEdit, value);
        }

        private BindableCollection<KeyValuePair<string, Color>> _themeItems = SysTheme.GetCommonBrushes();
        public BindableCollection<KeyValuePair<string, Color>> ThemeItems {
            get => _themeItems;
            set => SetAndNotify(ref _themeItems, value);
        }

        private KeyValuePair<string, Color> _themeItem;
        public KeyValuePair<string, Color> ThemeItem {
            get => _themeItem;
            set {
                SetAndNotify(ref _themeItem, value);
                CurrentColor = value.Value;
                CanEdit = Visibility.Visible;
            }
        }

        private Color _currentColor;
        public Color CurrentColor {
            get => _currentColor;
            set {
                SetAndNotify(ref _currentColor, value);
                brushSaved = false;
                LinkHWVector.SetFill(value);
            }
        }

        private string _themeName = "My Theme";
        public string ThemeName {
            get => _themeName;
            set => SetAndNotify(ref _themeName, value);
        }

        public void SaveBrush()
        {
            SysTheme.SetBrush(ThemeItem.Key, CurrentColor);
            ThemeItem = SysTheme.UpdateBrush(ThemeItems, ThemeItem.Key, CurrentColor);
            brushSaved = true;
        }

        public void SaveTheme()
        {
            // Save the current brush?
            if (WindowManager.Show("The current brush in not saved.\nWould you like to save before proceeding?", "Warning", true, width:240)) {
                SaveBrush();
            }

            // Save a theme resource
            if (SysTheme.Save(ThemeName, winMgr: WindowManager)) {

                // Update theme list
                Settings.Themes = SysTheme.GetThemes(true);

                // Update SysTheme
                Settings.Theme = SysTheme.Load(ThemeName);

                // Close editor form
                Settings.ThemeViewModel = null;
            }

        }

        public void Close()
        {
            if (WindowManager.Show("Discard changes? This cannot be undone.", "Warning", true, width: 240)) {

                // Update SysTheme
                Settings.Theme = SysTheme.Load();

                // Close editor form
                Settings.ThemeViewModel = null;
            }
        }

        public ThemeViewModel(IWindowManager? winMgr, SettingsViewModel settingsVM)
        {
            WindowManager = winMgr;
            Settings = settingsVM;
        }
    }
}
