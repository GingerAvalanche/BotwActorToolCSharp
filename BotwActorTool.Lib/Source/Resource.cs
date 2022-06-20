using System.Reflection;
using System.Text.Json;

namespace BotwActorTool.Lib
{
    public class Resource
    {
        public Dictionary<string, dynamic> Data { get; set; } = new();
        public Resource(string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream? stream = assembly.GetManifestResourceStream("BotwActorTool.Lib." + resourceName))
            {
                if (stream != null)
                {
                    Data = (Dictionary<string, dynamic>)JsonSerializer.Deserialize(stream, typeof(Dictionary<string, dynamic>))!;
                }
            }
        }
    }
}
