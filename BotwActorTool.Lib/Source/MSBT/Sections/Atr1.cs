﻿#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using JetBrains.Annotations;
using System.Text;

namespace BotwActorTool.Lib.MSBT.Sections
{
    internal class Atr1 : ICalculatesSize, IUpdates
    {
        [NotNull]
        public MSBT msbt;
        public SectionHeader section;
        public uint string_count;
        public uint _unknown_1;
        public List<List<byte>> raw_strings;

        public Atr1(SectionHeader section, uint string_count, uint _unknown_1, IEnumerable<string> strings)
        {
            this.section = section;
            this.string_count = string_count;
            this._unknown_1 = _unknown_1;
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

        public void Update()
        {
            string_count = (uint)Strings().Count;
            section.size = (uint)(sizeof(uint) // Marshal.SizeOf(string_count)
                + sizeof(uint) // Marshal.SizeOf(_unknown_1)
                + sizeof(uint) * string_count // offsets
                + raw_strings.Select(c => c.Count).Sum());
        }

        public ulong CalcSize() => section.CalcSize()
                + (ulong)(sizeof(uint) // Marshal.SizeOf(string_count)
                + sizeof(uint) // Marshal.SizeOf(_unknown_1)
                + sizeof(uint) * string_count // offsets
                + raw_strings.Select(c => c.Count).Sum());
    }
}