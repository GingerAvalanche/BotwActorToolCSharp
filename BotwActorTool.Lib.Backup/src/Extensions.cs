using FirstPlugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BotwActorToolLib
{
    static class Extensions
    {
        public static byte[] Write(this SARC sarc)
        {
            using (MemoryStream stream = new())
            {
                sarc.Save(stream);
                return stream.ToArray();
            }
        }
        public static bool SetFileData(this SARC sarc, string RelPath, byte[] data)
        {
            byte[] NewData = data;
            string[] parts = RelPath.Split(new[] { "//" }, 2, System.StringSplitOptions.None);
            if (parts.Length == 2)
            {
                SARC NestedSarc = new();
                Stream stream = sarc.FileLookup[parts[0]].FileDataStream;
                bool yazd = Util.UnYazIfNeeded(ref stream);
                NestedSarc.Load(stream);
                NestedSarc.SetFileData(parts[1], data);
                NewData = NestedSarc.Write();
                NestedSarc.Unload();
                if (yazd)
                {
                    NewData = EveryFileExplorer.YAZ0.Compress(NewData, 7);
                }
            }
            return sarc.AddFile(sarc.SetupFileEntry(parts[0], NewData, NameHash(parts[0]).ToString("X8")));

            static uint NameHash(string name)
            {
                uint result = 0;
                for (int i = 0; i < name.Length; i++)
                {
                    result = name[i] + result * 0x00000065;
                }
                return result;
            }
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
            foreach (var item in enumerable)
            {
                batch.Add(item);
                if (++c % batchSize == 0)
                {
                    yield return batch;
                    batch = new List<T>();
                }
            }
            if (batch.Count != 0)
            {
                yield return batch;
            }
        }
        public static IEnumerable<byte> ToEndianness(this IEnumerable<byte> bytes, MSBT.Endianness endianness)
        {
            return endianness switch
            {
                MSBT.Endianness.Big => bytes.Reverse(),
                _ => bytes,
            };
        }
        public static string ToStringEncoding(this IEnumerable<byte> chars, MSBT.UTFEncoding encoding)
        {
            return encoding switch
            {
                MSBT.UTFEncoding.UTF16 => Encoding.Unicode.GetString(chars.ToArray()),
                _ => Encoding.UTF8.GetString(chars.ToArray()),
            };
        }
        public static T[] Fill<T>(this T[] array, T val)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = val;
            }
            return array;
        }
    }
}
