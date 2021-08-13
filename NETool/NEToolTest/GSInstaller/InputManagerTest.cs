using GSInstaller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEToolTest.GSInstaller
{
    [TestClass]
    public class InputManagerTest
    {
        [TestMethod]
        public void TestSubsystemCheck()
        {
            installation_infor.openfile = new string[]{"C:\\Working\\TestData\\DIS_v1.0.0.zip"};

            InputManager inputmanager= new InputManager();
            int result = inputmanager.SubsystemCheck();
            Assert.AreEqual(true, result);
        }
    }
}
