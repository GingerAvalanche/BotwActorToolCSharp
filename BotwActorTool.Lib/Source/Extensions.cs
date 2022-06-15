using Nintendo.Sarc;
using System.Text;
using Yaz0Library;

namespace BotwActorTool.Lib
{
    static class Extensions
    {
        public static bool SetFileData(this SarcFile sarc, string RelPath, byte[] data)
        {
            byte[] newData = data;
            string[] parts = RelPath.Split(new[] { "//" }, 2, StringSplitOptions.None);

            if (parts.Length == 2) {
                byte[] bytes = sarc.Files[parts[0]];
                bool yazd = Util.UnYazIfNeeded(ref bytes);
                SarcFile nestedSarc = new(bytes);

                nestedSarc.SetFileData(parts[1], data);
                newData = yazd ? Yaz0.CompressFast(nestedSarc.ToBinary(), 7) : nestedSarc.ToBinary();

                // // Not sure what this did
                // 
                // nestedSarc.Unload();
            }

            // // I think the new library handles hashing automaticly during serialization, should be tested
            // 
            // return sarc.AddFile(sarc.SetupFileEntry(parts[0], newData, NameHash(parts[0]).ToString("X8")));
            // 
            // static uint NameHash(string name)
            // {
            //     uint result = 0;
            //     for (int i = 0; i < name.Length; i++) {
            //         result = name[i] + result * 0x00000065;
            //     }
            //     return result;
            // }

            sarc.Files.Add(parts[0], newData);
            return true;
        }

        public static byte[] ToArray(this Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
