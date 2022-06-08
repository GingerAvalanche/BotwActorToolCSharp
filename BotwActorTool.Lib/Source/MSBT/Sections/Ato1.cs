#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using JetBrains.Annotations;

namespace BotwActorTool.Lib.MSBT.Sections
{
    internal class Ato1 : ICalculatesSize
    {
        [NotNull]
        public MSBT msbt;
        public SectionHeader section;
        public byte[] _unknown;

        public Ato1(SectionHeader section, byte[] bytes)
        {
            this.section = section;
            _unknown = bytes;
        }

        public ulong CalcSize() => section.CalcSize() + (ulong)_unknown.Length;
    }
}
