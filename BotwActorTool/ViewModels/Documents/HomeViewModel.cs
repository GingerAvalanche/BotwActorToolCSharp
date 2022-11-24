using Dock.Model.ReactiveUI.Controls;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BotwActorTool.ViewModels.Documents
{
    public class HomeViewModel : Document
    {
        public HomeViewModel()
        {
            Title = "Home";
            CanClose = false;
            CanFloat = false;
            CanPin = false;
        }

        public void VersionLink()
        {
            ProcessStartInfo info = new() {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                info.FileName = "cmd";
                info.Arguments = $"/c start https://github.com/GingerAvalanche/BotwActorToolCSharp/releases/{Meta.Version}";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                info.FileName = "xdg-open";
                info.Arguments = $"https://github.com/GingerAvalanche/BotwActorToolCSharp/releases/{Meta.Version}";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                info.FileName = "open";
                info.Arguments = $"https://github.com/GingerAvalanche/BotwActorToolCSharp/releases/{Meta.Version}";
            }
            else {
                throw new Exception("Unknown operating system");
            }

            Process.Start(info);
        }
    }
}
