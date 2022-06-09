using BotwActorTool.Lib.MSBT;
using System.Diagnostics;

namespace MSBTTests
{
    [TestClass]
    public class MSBTTest
    {
        [TestMethod]
        public void ReadAndLogMSBT_NoChanges()
        {
            MSBT msbt = new(File.Open("E:\\Users\\chodn\\Documents\\CemuShit\\ArmorHead.msbt", FileMode.Open, FileAccess.Read));
            Dictionary<string, string> texts = msbt.GetTexts();
            Assert.IsNotNull(texts);
            Assert.AreEqual(334, texts.Count);
            foreach (KeyValuePair<string, string> kvp in texts)
            {
                Trace.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}