using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool
{
    public static class Meta
    {
        public static string Name { get; } = "Botw Actor Tool";
        public static string Version { get; } = "0.1.0-alpha";
        public static string Footer { get; } = $"{Name} (C#) - v{Version}";
        public static string BaseUrl { get; } = "https://raw.githubusercontent.com/GingerAvalanche/BotwActorToolCSharp/master";
        public static bool IsWindows { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}
