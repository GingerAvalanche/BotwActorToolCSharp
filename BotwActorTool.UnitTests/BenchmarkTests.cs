using BenchmarkDotNet.Attributes;
using BotwActorTool.Lib;

namespace BotwActorTool.UnitTests
{
    public class BenchmarkTests
    {
        [Benchmark]
        public BatMod TestBatModAsync() => new(@"E:\Users\chodn\Documents\CemuShit\Remodels\Zelda\Ruby Rose\RubyRose");
        [Benchmark]
        public BatMod TestBatModSync() => new(@"E:\Users\chodn\Documents\CemuShit\Remodels\Zelda\Ruby Rose\RubyRose", false);
    }
}
