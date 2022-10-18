﻿using Avalonia.Input;
using BotwActorTool.GUI.Dialogs;
using BotwActorTool.GUI.Models;
using Dock.Model.ReactiveUI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BotwActorTool.GUI.ViewModels.Tools
{
    public class FileBrowserViewModel : Tool
    {
        public ObservableCollection<TreeNodeModel> ItemsBase = new();
        public bool Unloaded => ItemsBase.Count.Equals(0);

        private TreeNodeModel? selectedItem;
        public TreeNodeModel? SelectedItem {
            get => selectedItem;
            set {
                this.RaiseAndSetIfChanged(ref selectedItem, value);
            }
        }

        private ObservableCollection<TreeNodeModel>? items;
        public ObservableCollection<TreeNodeModel> Items {
            get {
                items ??= new(ItemsBase);
                return items;
            }
            set => this.RaiseAndSetIfChanged(ref items, value);
        }

        private string searchField = "";
        public string SearchField {
            get => searchField;
            set {
                this.RaiseAndSetIfChanged(ref searchField, value);
                Items = new(string.IsNullOrEmpty(searchField) ? ItemsBase : ItemsBase.Where(x => x.Key.ToLower().Contains(searchField.ToLower())).OrderBy(x => x.Key));
            }
        }

        public async void Open()
        {
            // INVOKE OPEN
            // - Report UI as working
            // - Build View (async)
            // - Inject into DocumentDock
            // - End working
            await MessageBox.Show("INVOKE OPEN");
        }

        public async void CreateCopy()
        {
            // INPUT DIALOG
            var name = (await InputDialog.Show(new Dictionary<string, string>() {
                { "Name", SelectedItem!.Key }
            }, $"Copy {SelectedItem!.Key}"))?["Name"];

            if (name != null) {
                // INVOKE OPEN
                // RENAME
            }
        }

        public async void ActorInfo()
        {
            await MessageBox.Show(SelectedItem!.Meta, $"{SelectedItem!.Key} - Actor Info");
        }
    }
}
