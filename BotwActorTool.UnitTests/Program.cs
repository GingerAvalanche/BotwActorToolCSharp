using BenchmarkDotNet.Running;
using BotwActorTool.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.UnitTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BatMod mod = new(@"E:\Users\chodn\Documents\CemuShit\Remodels\Zelda\Ruby Rose\RubyRose");
            BenchmarkRunner.Run<BenchmarkTests>();
        }
    }
}
