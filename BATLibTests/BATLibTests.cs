using BotwActorTool.Lib;
using Nintendo.Aamp;
using System.Diagnostics;

namespace BATLibTests
{
    [TestClass]
    public class BATLibTests
    {
        [TestMethod]
        public void ModRoot()
        {
            Assert.AreEqual("C:/mod/content", Util.GetModRoot("C:/mod/content/Actor/ActorInfo.product.sbyml"));
            Assert.AreEqual("C:/mod/romfs", Util.GetModRoot("C:/mod/romfs/Actor/ActorInfo.product.sbyml"));
        }
        [TestMethod]
        public void ReadAamp()
        {
            AampFile aamp = new(@"E:\Users\chodn\Documents\CemuShit\Remodels\Zelda\Byleth\mod content_unbuilt\content\Actor\ActorLink\Armor_001_G_Upper.bxml");
            Trace.WriteLine(aamp.ToYml());
        }
    }
}