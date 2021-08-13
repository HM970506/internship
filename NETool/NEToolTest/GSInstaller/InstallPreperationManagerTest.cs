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
    public class InstallPreperationManagerTest
    {
        [TestMethod]
        public void TestUserConfirm()
        {
            InstallPreperationManager installpreperation = new InstallPreperationManager();
            bool result = installpreperation.GridviewSetting();
            Assert.AreEqual(true, result);
        }
    }
}
