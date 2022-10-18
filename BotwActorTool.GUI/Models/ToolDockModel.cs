﻿using BotwActorTool.GUI.Attributes;
using BotwActorTool.GUI.Builders;
using BotwActorTool.GUI.Extensions;
using BotwActorTool.GUI.ViewModels.Tools;
using BotwActorTool.Lib;
using Dock.Model.Core;
using Nintendo.Byml;
using Nintendo.Yaz0;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BotwActorTool.GUI.Models
{
    internal class ToolDockModel
    {
        [ToolDock(Title = "Actor")]
        public static FileBrowserViewModel Actor()
        {
            // Query build tool dock for this VM and return, else build the VM
            return ToolDockFactory.QueryTool(out FileBrowserViewModel? obj) != null ? obj! : new();
        }

        [ToolDock(Title = "Vanilla Actors")]
        public static FileBrowserViewModel VanillaActors()
        {
            // Query build tool dock for this VM and return, else build the VM
            if (ToolDockFactory.QueryTool(out FileBrowserViewModel? obj) != null) return obj!;

            FileBrowserViewModel browser = new() {
                ItemsBase = ActorInfoExt.LoadActorInfoNodes(Config.GetDir(BotwDir.Update))
            };

            return browser;
        }

        [ToolDock(Title = "Mod Actors")]
        public static FileBrowserViewModel ModActors()
        {
            // Query build tool dock for this VM and return, else build the VM
            return ToolDockFactory.QueryTool(out FileBrowserViewModel? obj) != null ? obj! : new();
        }
    }
}