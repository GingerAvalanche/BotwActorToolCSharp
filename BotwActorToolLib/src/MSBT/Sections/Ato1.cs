using JetBrains.Annotations;

namespace BotwActorToolLib.MSBT.Sections
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
