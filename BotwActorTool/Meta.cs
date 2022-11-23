using System.Runtime.InteropServices;

namespace BotwActorTool
{
    public static class Meta
    {
        public static string Name { get; } = "Botw Actor Tool";
        public static string? Version { get; } = typeof(Meta).Assembly.GetName().Version?.ToString(3);
        public static string Footer { get; } = $"{Name} (C#) - v{Version}";
        public static string BaseUrl { get; } = "https://raw.githubusercontent.com/GingerAvalanche/BotwActorToolCSharp/master";
        public static bool IsWindows { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}
