using GSIntaller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEToolTest.GSInstaller
{
    [TestClass]
    public class UnzipManagerTest
    {
        [TestMethod]
        public void TestMakeTempUnzip()
        {
            UnzipManager unzipmanager = new UnzipManager();
            bool result = unzipmanager.MakeTempAndUnzip();
            Assert.AreEqual(true, result);
        }
    }
}
