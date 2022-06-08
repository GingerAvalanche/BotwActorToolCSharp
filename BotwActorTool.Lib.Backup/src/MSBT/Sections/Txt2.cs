using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotwActorToolLib.MSBT.Sections
{
    internal class Txt2 : ICalculatesSize, IUpdates
    {
        [NotNull]
        public MSBT msbt;
        public SectionHeader section;
        public uint string_count;
        public List<List<byte>> raw_strings;
        public Txt2(SectionHeader section, uint string_count, IEnumerable<string> strings)
        {
            this.section = section;
            this.string_count = string_count;
            SetStrings(strings);
        }
        public List<string> Strings()
        {
            return msbt.header.encoding switch
            {
                UTFEncoding.UTF16 => raw_strings
                    .Select(s => s.Chunk(2).Select(s => s.ToArray()))
                    .SelectMany(s => s.Select(s => Encoding.Unicode.GetString(s)))
                    .ToList(),
                _ => raw_strings
                    .Select(r => Encoding.UTF8.GetString(r.ToArray()))
                    .ToList(),
            };
        }
        public void SetStrings(IEnumerable<string> strings)
        {
            switch (msbt.header.encoding)
            {
                case UTFEncoding.UTF16:
                    raw_strings = strings
                        .Select(s => Encoding.Unicode.GetBytes(s).ToList())
                        .ToList();
                    break;
                case UTFEncoding.UTF8:
                    raw_strings = strings
                        .Select(r => Encoding.UTF8.GetBytes(r).ToList())
                        .ToList();
                    break;
            }
        }
        public ulong CalcSize() => (ulong)((int)section.CalcSize()
            + sizeof(uint) // Marshal.SizeOf(string_count)
            + sizeof(uint) * raw_strings.Count
            + raw_strings.Select(s => s.Count).Sum());
        public void Update()
        {
            section.size = (uint)(sizeof(uint) // Marshal.SizeOf(string_count)
                + sizeof(uint) * raw_strings.Count
                + raw_strings.Select(c => c.Count).Sum());
        }
    }
}
