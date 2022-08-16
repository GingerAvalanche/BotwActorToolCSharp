using BotwActorTool.Lib;
using Nintendo.Aamp;
using Nintendo.Byml;
using System.Diagnostics;
using Nintendo.Yaz0;

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
        [TestMethod]
        public void CreateActor()
        {
            Actor a = new("E:/Users/chodn/Documents/ISOs - WiiU/The Legend of Zelda Breath of the Wild (UPDATE DATA) (v208) (USA)/content/Actor/Pack/Armor_114_Head.sbactorpack");
            a.Update();
            Trace.WriteLine(new BymlFile(a.GetInfo().Hash).ToYaml());
        }
        [TestMethod]
        public void ReadWriteActorInfo()
        {
            BymlFile file = new(Yaz0.Decompress(@"E:\Users\chodn\Documents\ISOs - WiiU\The Legend of Zelda Breath of the Wild (UPDATE DATA) (v208) (USA)\content\Actor\ActorInfo.product.sbyml"));
            for (int i = 0; i < 5; i++)
            {
                File.WriteAllBytes($@"E:\Users\chodn\Documents\ISOs - WiiU\The Legend of Zelda Breath of the Wild (UPDATE DATA) (v208) (USA)\content\Actor\ActorInfo.test.{i}.product.byml", file.ToBinary());
                file = new($@"E:\Users\chodn\Documents\ISOs - WiiU\The Legend of Zelda Breath of the Wild (UPDATE DATA) (v208) (USA)\content\Actor\ActorInfo.test.{i}.product.byml");
            }
        }
        [TestMethod]
        public void TestByml()
        {
            BymlFile file = new(@"C:\Users\chodn\source\repos\NCF-Library\IOTests\Data\IO.byml");
            Trace.WriteLine(file.ToYaml());
            //File.WriteAllBytes(@"E:\Users\chodn\Documents\ISOs - WiiU\The Legend of Zelda Breath of the Wild (UPDATE DATA) (v208) (USA)\content\Actor\ActorInfo.test.product.sbyml", Yaz0.Compress(file.ToBinary()));
        }
        [TestMethod]
        public void TestSaveFormatFiles()
        {
            BymlFile[] files = Util.GetAccountSaveFormatFiles(@"E:/Users/chodn/Documents/ISOs - WiiU/The Legend of Zelda Breath of the Wild (UPDATE DATA) (v208) (USA)/content");
            Trace.WriteLine(files[0].RootNode.Hash["save_info"].Array[0].Hash["directory_num"].Type);
            //File.WriteAllBytes(@"E:\Users\chodn\Documents\ISOs - WiiU\The Legend of Zelda Breath of the Wild (UPDATE DATA) (v208) (USA)\content\Actor\ActorInfo.test.product.sbyml", Yaz0.Compress(file.ToBinary()));
        }
    }
}