using BotwActorTool.Lib;

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
    }
}