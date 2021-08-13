using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Data;
using System.Threading;
using log4net;
using SI.NE.CMM.Logging;


namespace GSIntaller
{

    public class UnzipManager
    {
        public event EventHandler<string> UpdateProgressLog;
        public event EventHandler<bool> evtInstallprepertaionstart_unzipmanager;
        String[] input;
        private ILog logger = LogManager.GetLogger(typeof(UnzipManager)); //모든 파일이 하나씩 들고 있어야 함

        public UnzipManager()
        {
        }

        /// <summary>
        /// temp폴더를 만들고 그곳에 subsystem을 unzip하는 함수
        /// temp폴더는 step4 직전에 삭제된다
        /// </summary>
        public bool MakeTempAndUnzip()
        {

            input = installation_infor.openfile;
            DirectoryInfo di;
            string unzip_path;

            unzip_path = Path.GetDirectoryName(input[0]) + "\\temp";
            di = new DirectoryInfo(unzip_path);

            //temp가 기존에 존재하면 삭제하고 새 temp폴더 만듦
            if (di.Exists) di.Delete(true);
            di.Create();
            foreach (string input_file in input)
            {
                try
                {
                    //해당 zip명 폴더를 만들고 그곳에 unzip
                    string input_file_name = Path.GetFileNameWithoutExtension(input_file);

                    unzip_path = Path.GetDirectoryName(input_file) + "\\temp\\" + input_file_name;

                    UpdateProgressLog.Invoke(this, input_file_name + " is unzipping..");

                    if (installation_infor.purpose == 3)
                    {

                        //바깥쪽 폴더 ex)FDS_v.0.0.1
                        string outfolder_path = Path.GetDirectoryName(input_file) + "\\temp\\"
                            + installation_infor.Getsubsystemname(installation_infor.Getsubsystem(input_file_name)) + "_v."
                            + installation_infor.Getversion(input_file_name);

                        di = new DirectoryInfo(outfolder_path);
                        if (!di.Exists) di.Create();

                        //안쪽 폴더 ex)FDSClient 
                        string infolder_path = outfolder_path + "\\" + installation_infor.Getsubsystem(input_file_name);
                        unzip_path = infolder_path;
                    }

                    di = new DirectoryInfo(unzip_path);
                    di.Create();


                    System.IO.Compression.ZipFile.ExtractToDirectory(input_file, unzip_path);
                    if (installation_infor.purpose != 3) installation_infor.subsystem_path.Add(unzip_path);
                    else installation_infor.subsystem_path.Add(Path.GetDirectoryName(unzip_path));
                }
                catch
                {
                    LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00034"), string.Format(log.logService.GetEnglishMessage("INST-00034"), input_file + " MakeTempAndUnzip Error: pass"));

                }

            }
            evtInstallprepertaionstart_unzipmanager(this, true);
            return true;
        }
    }
}
