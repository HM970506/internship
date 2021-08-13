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
    public class InstallManagerTest
    {
        [TestMethod]
        public void TestHashMake()
        {
            List<List<string>> hashlist = new List<List<string>>();
            //각 subsystem별 파일 목록을 담은 List의 List
             
            bool before = false; //install 전 해쉬목록임을 나타내는 BOOLEAN
            bool after = false; //install 후 해쉬목록임을 나타내는 BOOLEAN
            //해당 BOOLEAN표시에 따라 Installation_Info.before_hash 또는 Installation_Info.after_hash에 저장 여부 결정

            HashManager hashmanager = new HashManager();
            List<List<string>> result = hashmanager.HashMake(hashlist, before, after);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestHashCheck()
        {
            HashManager hashmanager = new HashManager();

            installation_infor.before_hash = null;//혹은 hashmanager.HashMake(hashlist,before, after);
            installation_infor.after_hash = null;

            bool result= hashmanager.HashCheck();
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestFolderCheck()
        {
            InstallManager installmanager = new InstallManager();

            List<string> result = installmanager.FolderCheck();
            Assert.AreEqual(true, result); 

        }

        [TestMethod]
        public void TestCheckandMakeBakupfolder()
        {
            InstallManager installmanager = new InstallManager();

            bool result = installmanager.CheckandMakeBackupfolder();
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestBackup()
        {
            InstallManager installmanager = new InstallManager();

            string now_subsystem="subsystemClient";
            string now_path="설치된 서브시스템 경로";

            bool result = installmanager.Backup(now_subsystem, now_path);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Testinstall()
        {
            InstallManager installmanager = new InstallManager();

            bool result = installmanager.Install();
            Assert.AreEqual(true, result);
        }

    }


}
