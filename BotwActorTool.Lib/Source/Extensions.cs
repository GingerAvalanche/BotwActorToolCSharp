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

            // // I think the new library handles this automaticly during serialization, should be tested
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

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> enumerable, int batchSize)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (batchSize <= 0) throw new ArgumentOutOfRangeException(nameof(batchSize));
            return enumerable.ChunkCore(batchSize);
        }

        private static IEnumerable<IEnumerable<T>> ChunkCore<T>(this IEnumerable<T> enumerable, int batchSize)
        {
            var c = 0;
            var batch = new List<T>();
            foreach (var item in enumerable) {
                batch.Add(item);
                if (++c % batchSize == 0) {
                    yield return batch;
                    batch = new List<T>();
                }
            }
            if (batch.Count != 0) {
                yield return batch;
            }
        }

        public static IEnumerable<byte> ToEndianness(this IEnumerable<byte> bytes, MSBT.Endianness endianness)
        {
            return endianness switch {
                MSBT.Endianness.Big => bytes.Reverse(),
                _ => bytes,
            };
        }

        public static string ToStringEncoding(this IEnumerable<byte> chars, MSBT.UTFEncoding encoding)
        {
            return encoding switch {
                MSBT.UTFEncoding.UTF16 => Encoding.Unicode.GetString(chars.ToArray()),
                _ => Encoding.UTF8.GetString(chars.ToArray()),
            };
        }

        public static T[] Fill<T>(this T[] array, T val)
        {
            for (int i = 0; i < array.Length; i++) {
                array[i] = val;
            }
            return array;
        }

        public static byte[] ToArray(this Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
