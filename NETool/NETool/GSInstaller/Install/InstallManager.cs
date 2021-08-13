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
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using log4net;
using SI.NE.CMM.Logging;
using System.Security.AccessControl;

namespace GSIntaller
{
    public class InstallManager
    {
        public HashManager hm;

        private ILog logger = LogManager.GetLogger(typeof(InstallManager)); //모든 파일이 하나씩 들고 있어야 함

        public event EventHandler<bool> evtUpdateProgress;
        public event EventHandler<string> evtUpdateLog;
        public event EventHandler<string> evtUpdateLabel;

        /// <summary>
        /// Install과정의 전체적인 진행 함수. background에서 병렬로 실행된다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public int Start()
        {
            evtUpdateLabel(this, "");
            evtUpdateLog(this, Message.InstallStart(logger));
            evtUpdateLog(this, Message.DoubleNewline());

            evtUpdateLog(this, Message.StartSetting(logger));
            evtUpdateLog(this, Message.Newline());
            if(installation_infor.purpose==1) Setting();
            evtUpdateLog(this, Message.StartSettingEnd(logger));
            evtUpdateLog(this, Message.DoubleNewline());
            evtUpdateProgress.Invoke(this, true);

            //server 연결
            evtUpdateLog(this, Message.ServerConnect(logger));
            evtUpdateLog(this, Message.Newline());
            ServerConnector sc = new ServerConnector();
            sc.Connect();

            if (installation_infor.cancel) return 6;
            //client, server 존재 유무 출력, 존재하는 것만 subsystem_install에 남긴다
            evtUpdateLog(this, Message.FolderCheck(logger));
            evtUpdateLog(this, Message.Newline());
            if (FolderCheck() == null) return 1;
            evtUpdateLog(this, Message.FolderCheckEnd(logger));
            evtUpdateLog(this, Message.DoubleNewline());
            evtUpdateProgress.Invoke(this, true);


            if (installation_infor.purpose != 1) //update
            {
                hm = new HashManager();

                //설치장소에 백업폴더 있는지 확인하고 없으면 만듦
                evtUpdateLog(this, Message.Backupfolder(logger));
                evtUpdateLog(this, Message.Newline());
                CheckandMakeBackupfolder();
                evtUpdateLog(this, Message.BackupfolderEnd(logger));
                evtUpdateLog(this, Message.DoubleNewline());
                evtUpdateProgress.Invoke(this, true);

                if (installation_infor.cancel) return 6;
                //해당 경로 해쉬 데이터 생성
                evtUpdateLog(this, Message.HashMake(logger));
                evtUpdateLog(this, Message.Newline());
                hm.HashMake(installation_infor.before_hash, true, false);
                evtUpdateLog(this, Message.HashMakeEnd(logger));
                evtUpdateLog(this, Message.DoubleNewline());
                evtUpdateProgress.Invoke(this, true);

                if (installation_infor.cancel) return 6;
                //백업폴더 만들어서 zip백업,백업한 폴더 삭제
                evtUpdateLog(this, Message.Zip(logger));
                evtUpdateLog(this, Message.Newline());
                int backup = ExistingFolderBackup();
                if (backup == 0) return 2;
                else if (backup == 6) return 6;

                evtUpdateLog(this, Message.ZipEnd(logger));
                evtUpdateLog(this, Message.DoubleNewline());
                evtUpdateProgress.Invoke(this, true);
            }

            else
            {//설치시

                //폰트 설치

                evtUpdateLog(this, Message.InstallFontInstallStart(logger));
                evtUpdateLog(this, Message.Newline());
                if (!FontInstall())
                {
                    evtUpdateLog(this, Message.InstallFontInstallError(logger));
                    evtUpdateLog(this, Message.Newline());
                }
                evtUpdateLog(this, Message.InstallFontInstallEnd(logger));
                evtUpdateLog(this, Message.DoubleNewline());
                evtUpdateProgress.Invoke(this, true);

                //데이터 설치
                evtUpdateLog(this, Message.InstallDataInstallStart(logger));
                evtUpdateLog(this, Message.Newline());
                DataInstall();
                evtUpdateLog(this, Message.InstallDataInstallEnd(logger));
                evtUpdateLog(this, Message.DoubleNewline());
                evtUpdateProgress.Invoke(this, true);

            }

            evtUpdateLabel(this, "");
            if (installation_infor.cancel) return 6;
            //해당 path로 subsystem파일을 옮긴다
            evtUpdateLog(this, Message.InstallWorkStart(logger));
            evtUpdateLog(this, Message.Newline());
            if (!Install()) return 3;
            evtUpdateLog(this, Message.InstallWorkEnd(logger));
            evtUpdateLog(this, Message.DoubleNewline());
            evtUpdateProgress.Invoke(this, true);

            if (installation_infor.purpose == 3 || installation_infor.purpose == 2)//backup, restore
            {
                if (installation_infor.cancel) return 6;
                //해쉬를 다시 생성
                evtUpdateLog(this, Message.HashCheck(logger));
                evtUpdateLog(this, Message.Newline());
                hm.HashMake(installation_infor.after_hash, false, true);

                //무결성 검사
                if (hm.HashCheck()) evtUpdateLog(this, Message.HashCheckEnd(logger));
                else
                {
                    evtUpdateLog(this, Message.HashCheckFail(logger));
                    Application.Exit();
                }
                evtUpdateProgress.Invoke(this, true);
            }
            evtUpdateLog(this, Message.DoubleNewline());

            //다이얼로그 파일 수정
            evtUpdateLog(this, Message.MakeinstallinformationStart(logger));
            evtUpdateLog(this, Message.Newline());
            if (!Makeinstallinformation()) return 4;
            evtUpdateLog(this, Message.MakeinstallinformationEnd(logger));
            evtUpdateLog(this, Message.DoubleNewline());
            evtUpdateProgress.Invoke(this, true);


            //템프 폴더 삭제
            evtUpdateLabel(this, "");
            evtUpdateLog(this, Message.TempDeleteStart(logger));
            evtUpdateLog(this, Message.Newline());
            if (!TempDelete())
            {
                evtUpdateLog(this, Message.TempDeleteArleady(logger));
                evtUpdateLog(this, Message.Newline());
            }
            evtUpdateLog(this, Message.TempDeleteEnd(logger));
            evtUpdateLog(this, Message.DoubleNewline());
            evtUpdateProgress.Invoke(this, true);

            //서버 끊기
            sc.Unconnect();
            evtUpdateLog(this, Message.ServerUnconnect(logger));
            evtUpdateLog(this, Message.Newline());
            evtUpdateLog(this, Message.InstallEnd(logger));
            evtUpdateLog(this, Message.DoubleNewline());
            End();
            return 0;

        }

        public void Setting()
        {
            DirectoryInfo[] di = {
           new DirectoryInfo(installation_infor.path[0]),
           new DirectoryInfo(installation_infor.path[1]),
           new DirectoryInfo(installation_infor.path[2]),
          new DirectoryInfo(installation_infor.path[3])};

            foreach(DirectoryInfo now_di in di)
            {
                if (now_di.Exists) now_di.Delete(true);
                now_di.Create();
            }

        }


        /// <summary>
        /// 남은 progress진행
        /// </summary>
        /// <param name="Parent"></param>
        public void End()
        {
            if (installation_infor.purpose == 1)
            {
                evtUpdateProgress.Invoke(this, true);
                evtUpdateProgress.Invoke(this, true);
                evtUpdateProgress.Invoke(this, true);
            }

            else if (installation_infor.purpose == 2)
            {
                evtUpdateProgress.Invoke(this, true);
                evtUpdateProgress.Invoke(this, true);
            }

            else evtUpdateProgress.Invoke(this, true);


        }

        /// <summary>
        /// installinformation 생성
        /// </summary>
        public bool Makeinstallinformation()
        {

            foreach (string now_subsystem_path in installation_infor.subsystem_install)
            {
                try
                {
                    string subsystem_name = Path.GetFileName(now_subsystem_path);
                    string subsystem_version = installation_infor.Getversion(Path.GetFileName(Path.GetDirectoryName(now_subsystem_path)));

                    string history_path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(now_subsystem_path))) + "\\InstallHistory";
                    DirectoryInfo history = new DirectoryInfo(history_path);
                    if (!history.Exists) history.Create();

                    string installinformation_path = history_path + "\\history.log";

                    StreamWriter writer;
                    writer = File.AppendText(installinformation_path);
                    writer.Close();

                    string before_text = File.ReadAllText(installinformation_path);
                    string text = "";

                    if (installation_infor.purpose == 1) text = "install";
                    else if (installation_infor.purpose == 2) text = "update";
                    else if (installation_infor.purpose == 3) text = "restore";

                    text += " " + subsystem_name + " " + subsystem_version + " " + installation_infor.Findpath(subsystem_name);

                    text = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + " " + text;

                    writer = File.AppendText(installinformation_path);
                    writer.WriteLine(text);
                    writer.Close();
                }
                catch
                {
                    evtUpdateLog(this, Message.MakeInstallinformationError(Path.GetFileName(now_subsystem_path), logger));
                    evtUpdateLog(this, Message.Newline());
                }
            }
            return true;
        }


        /// <summary>
        /// step1_2에서 만들었던 temp 폴더 삭제
        /// </summary>
        public bool TempDelete()
        {
            string temp_path = Path.GetDirectoryName(installation_infor.openfile[0]) + "\\temp";
            DirectoryInfo di = new DirectoryInfo(temp_path);
            if (di.Exists)
            {
                di.Delete(true);
                return true;
            }
            else return false;
        }


        public bool Install()
        {

            foreach (string now_subsystem_path in installation_infor.subsystem_install)
            {
                try
                {
                    string subsystem = Path.GetFileName(now_subsystem_path);
                    string destination_subsystem_path = installation_infor.Findpath(subsystem);

                    destination_subsystem_path += "\\" + Path.GetFileNameWithoutExtension(now_subsystem_path);
                    DirectoryInfo di = new DirectoryInfo(destination_subsystem_path);
                    di.Create();

                    CopyFolder(destination_subsystem_path, now_subsystem_path);
                }
                catch
                {
                    evtUpdateLog(this, Message.InstallError(Path.GetFileName(now_subsystem_path), logger));
                    evtUpdateLog(this, Message.Newline());
                }
            }
            return true;
        }


        /// <summary>
        /// 폴더와 파일을 copy하는 재귀함수
        /// </summary>
        /// <param name="destFolder"></param>
        /// <param name="sourceFolder"></param>
        public void CopyFolder(string destFolder, string sourceFolder)
        {
            if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);

            string[] files = Directory.GetFiles(sourceFolder);
            string[] folders = Directory.GetDirectories(sourceFolder);

            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                DirectoryInfo folder_di = new DirectoryInfo(dest);
                if (!folder_di.Exists) folder_di.Create();
                if (dest == "") return;
                CopyFolder(dest, folder);

            }

            foreach (string file in files)
            {
                evtUpdateLabel(this, Path.GetFileName(file));
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                if (dest == "") return;
                File.Copy(file, dest, true);
            }


        }

        /// <summary>
        /// server나 client 폴더가 존재하는지 체크하는 함수
        /// 존재하는 항목만 subsystem_install에 추가된다
        /// </summary>
        /// <returns></returns>
        public List<string> FolderCheck()
        {
            List<string> return_list = new List<string>();
            foreach (string now_subsystem_path in installation_infor.subsystem_path)
            {
                try
                {
                    bool client = false;
                    bool server = false;
                    DirectoryInfo di = new DirectoryInfo(now_subsystem_path);
                    foreach (DirectoryInfo di_inner in di.GetDirectories())
                    {
                        if (di_inner.Name.Contains("Client"))
                        {
                            client = true;
                            installation_infor.subsystem_install.Add(now_subsystem_path + "\\" + di_inner.ToString());
                        }
                        if (di_inner.Name.Contains("Server"))
                        {
                            server = true;
                            installation_infor.subsystem_install.Add(now_subsystem_path + "\\" + di_inner.ToString());
                        }
                    }

                    if (client)
                    {
                        evtUpdateLog(this, Message.FolderCheckClientExist(Path.GetFileName(now_subsystem_path), logger));
                        return_list.Add(Message.FolderCheckClientExist(Path.GetFileName(now_subsystem_path), logger));
                    }
                    else if (!client)
                    {
                        evtUpdateLog(this, Message.FolderCheckClientNotExist(Path.GetFileName(now_subsystem_path), logger));
                        return_list.Add(Message.FolderCheckClientExist(Path.GetFileName(now_subsystem_path), logger));
                    }

                    evtUpdateLog(this, Message.Newline());

                    if (server)
                    {
                        evtUpdateLog(this, Message.FolderCheckServerExist(Path.GetFileName(now_subsystem_path), logger));
                        return_list.Add(Message.FolderCheckClientExist(Path.GetFileName(now_subsystem_path), logger));
                    }
                    else if (!server)
                    {
                        evtUpdateLog(this, Message.FolderCheckServerNotExist(Path.GetFileName(now_subsystem_path), logger));
                        return_list.Add(Message.FolderCheckClientExist(Path.GetFileName(now_subsystem_path), logger));
                    }

                    evtUpdateLog(this, Message.Newline());

                }

                catch
                {
                    evtUpdateLog(this, Message.FolderCheckError(Path.GetFileName(now_subsystem_path), logger));
                    evtUpdateLog(this, Message.Newline());
                }
            }
            return return_list;
        }


        /// <summary>
        /// 설치 장소에 백업폴더 존재 체크하고 백업폴더 만듦
        /// </summary>
        public bool CheckandMakeBackupfolder()
        {
            foreach (string now_path in installation_infor.path)
            {
                DirectoryInfo di_backup = new DirectoryInfo(now_path + "\\Backup");
                if (!di_backup.Exists) di_backup.Create();
            }
            return true;
        }

        /// <summary>
        /// 해당 경로에서 같은 subsystem 폴더를 찾아 zip
        /// zip을 backup폴더로 이동
        /// </summary>
        public int ExistingFolderBackup()
        {
            foreach (string input_path in installation_infor.subsystem_path)
            {
                if (installation_infor.cancel) return 6;
                DirectoryInfo di = new DirectoryInfo(input_path);

                foreach (DirectoryInfo di_inner in di.GetDirectories())
                {
                    string now_file = di_inner.Name;
                    string now_install_subsystem = Path.GetFileNameWithoutExtension(now_file);
                    string now_install_subsystem_name = installation_infor.Getsubsystem(now_install_subsystem);
                    string now_path = installation_infor.Findpath(now_install_subsystem);

                    if (!Backup(now_install_subsystem, now_path)) return 0;
                }
            }
            return 1;
        }

        /// <summary>
        /// 폴더 zip, 이동, 기존폴더 삭제를 실행하는 함수
        /// 이동하려는 폴더에 같은 이름의 폴더가 이미 존재하면 덮어씌움
        /// </summary>
        /// <param name="client_or_server"></param>
        /// <param name="now_path"></param>
        /// <param name="now_install_subsystem_name"></param>
        public bool Backup(string now_subsystem, string now_path)
        {

            //백업zip 이름 설정에 사용되는 변수
            string build_version = BeforeBuildVersion(now_path, now_subsystem); //설치되어 있는 폴더의 exe에서 가져옴
            string backupfile_path = now_path + "\\" + now_subsystem;

            DirectoryInfo di = new DirectoryInfo(backupfile_path);
            if (di.Exists)
            {

                string backup_zip = now_subsystem + "_v" + build_version + ".zip";
                string destination_zip = now_path + "\\" + backup_zip;

                evtUpdateLabel(this, backup_zip + " backup..");
                DirectoryInfo di_in = new DirectoryInfo(destination_zip);
                if (di_in.Exists) di_in.Delete(true);
                System.IO.Compression.ZipFile.CreateFromDirectory(backupfile_path, destination_zip); //zip

                string destination_path = now_path + "\\Backup\\" + backup_zip;



                DirectoryInfo di_exist = new DirectoryInfo(destination_path);
                if (di_exist.Exists) di_exist.Delete();
                System.IO.File.Move(now_path + "\\" + backup_zip, destination_path); //백업
                System.IO.Directory.Delete(backupfile_path, true); //삭제}
                return true;
            }
            else
            {
                evtUpdateLog(this, "\r\nsubsystem Folder is not exist. Just Install");
                return false;
            }
        }



        /// <summary>
        /// 설치되어있는 exe파일로부터 build버전을 가져옴
        /// </summary>
        /// <param name="temp_path"></param>
        /// <param name="now_subsystem"></param>
        /// <returns></returns>
        public string BeforeBuildVersion(string now_path, string subsystem)
        {
            string subsystem_name = installation_infor.Getsubsystemname(subsystem);
            return installation_infor.GetBuildVersion(now_path + "\\" + subsystem, subsystem, subsystem_name);
        }



        public bool FontInstall()
        {
            string font_destination = Properties.Settings.Default.Font.ToString();
            string font_file = Path.GetDirectoryName(Path.GetDirectoryName(System.Windows.Forms.Application.StartupPath)) + "\\Font.zip";
            string font_path = Path.GetDirectoryName(font_file) + "\\Font";
            DirectoryInfo font = new DirectoryInfo(font_path);
            if (!font.Exists) font.Create();

            FileInfo di = new FileInfo(font_file);

            if (di.Exists)
            {
                if (font.Exists) font.Delete(true);
                System.IO.Compression.ZipFile.ExtractToDirectory(font_file, font_path);//unzip

                foreach (FileInfo now_font_file in font.GetFiles())
                {
                    try
                    {
                        evtUpdateLabel(this, now_font_file.Name);
                        System.IO.File.Move(font_path + "\\" + now_font_file.ToString(), font_destination); //move
                    }
                    catch { }

                }
                evtUpdateLabel(this, "");
                System.IO.Directory.Delete(font_path, true); //삭제
                return true;
            }
            else return false;
        }

        public void DataInstall()
        {
            string data_destination;
            string data_file = Path.GetDirectoryName(Path.GetDirectoryName(System.Windows.Forms.Application.StartupPath)) + "\\data.zip";
            string data_path = Path.GetDirectoryName(data_file) + "\\data";
            FileInfo fi = new FileInfo(data_file);
            DirectoryInfo data = new DirectoryInfo(data_path);
            if (data.Exists) data.Delete(true);
            data.Create();
            if (fi.Exists)
            {
                evtUpdateLabel(this, "unzipping...");
                System.IO.Compression.ZipFile.ExtractToDirectory(data_file, data_path); //unzip

                foreach (string now_subsystem_path in installation_infor.subsystem_install)
                {
                    try
                    {
                        //해당 서브시스템의 경로를 구하고 이것으로 data폴더 경로를 넣는다
                        data_destination = Path.GetDirectoryName(installation_infor.Findpath(Path.GetFileName(now_subsystem_path))) + "\\Data";
                        DirectoryInfo di_destination = new DirectoryInfo(data_destination);
                        if (!di_destination.Exists) di_destination.Create();
                        CopyFolder(data_destination, data_path); //move
                    }
                    catch
                    {
                        evtUpdateLog(this, Message.InstallDataInstallError(logger));
                        evtUpdateLog(this, Message.Newline());
                    }
                }


                System.IO.Directory.Delete(data_path, true); //삭제
            }
        }
    }

    public class HashManager
    {

        /// <summary>
        /// hash값 생성 함수
        /// 폴더 내 모든 파일을 재귀로 체크
        /// 현재는 빠른 비교를 위해 exe파일만을 체크하도록 함. -> 모든 파일 체크시 string범위를 벗어남!! 실제 사용 전 해당 부분 수정 필요
        /// </summary>
        /// <param name="nowList">생성한 hash값을 담는다</param>
        public List<List<string>> HashMake(List<List<string>> hashlist, bool before, bool after)
        {
            //설치하고자 하는 서브시스템 목록을 가져옴
            //해당 목록이 설치path에 존재하면 그 경로를 넣음
            foreach (string subsystem_path in installation_infor.subsystem_install)
            {
                List<string> nowlist = new List<string>(); //subsystem별로list를 만든다

                //before_hash면 temp폴더에서 시작
                //after_hash면 설치 장소에서 시작

                string path = "";

                if (before && !after) path = subsystem_path;
                if (after && !before) path = installation_infor.Findpath(Path.GetFileName(subsystem_path))+"\\"+Path.GetFileName(subsystem_path);

                string now_subsystem = Path.GetFileName(path);
                string now_subsystem_path = installation_infor.Findpath(now_subsystem);


                chkDir_md5(nowlist, subsystem_path);
                hashlist.Add(nowlist);
            }
            return hashlist;
        }

        /// <summary>
        /// 디렉토리 내부를 이동하며 모든 파일의 해쉬값을 얻는 재귀 함수
        /// </summary>
        /// <param name="_str"></param>
        /// <param name="_path"></param>
        private static void chkDir_md5(List<string> _str, string _path)
        {

            FileAttributes att = File.GetAttributes(_path);
            if ((att & FileAttributes.Directory) == FileAttributes.Directory) //폴더면 안쪽으로 이동
            {
                List<string> tmpPath = Directory.GetDirectories(_path, "*").ToList<string>();
                tmpPath.Sort();

                foreach (string s in tmpPath)
                {
                    chkDir_md5(_str, s);
                }

                string[] tmpFiles = Directory.GetFiles(_path, "*.*");

                foreach (string s in tmpFiles)
                {
                    chkDir_md5(_str, s);
                }
            }
            else //파일이면 해쉬값 얻어서 str에 추가
            {
                _str.Add(getMD5Hash(_path));

            }
        }

        /// <summary>
        /// 파일의 해쉬값을 얻는 함수
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string getMD5Hash(string str)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(str))
                {
                    var hash = md5.ComputeHash(stream);
                    string str_hash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    return str_hash;
                }
            }
        }


        /// <summary>
        /// 해쉬값을 비교하는 함수
        /// </summary>
        /// <returns></returns>
        public bool HashCheck()
        {
            
                int out_before = installation_infor.before_hash.Count();
                int out_after = installation_infor.after_hash.Count();

                if (out_before != out_after) return false;
                else
                {
                    for (int x = 0; x < out_before; x++)
                    {
                        List<string> before_list = installation_infor.before_hash[x];
                        List<string> after_list = installation_infor.after_hash[x];

                        int in_before = before_list.Count();
                        int in_after = after_list.Count();

                        if (in_before != in_after) return false;
                        else
                        {
                            for (int y = 0; y < before_list.Count(); y++)
                            {
                                string b = before_list[y];
                                string a = after_list[y];
                                if (!b.Equals(a)) return false;
                            }
                        }
                    }
                    return true;
                }

        }
    }


    public class ServerConnector
    {
        NetworkConnector share1;
        public ServerConnector()
        {
            share1 = new NetworkConnector();
        }
        public void Connect()
        {
            string ip = installation_infor.path[1];
            string id = installation_infor.server_id;
            string pw = installation_infor.server_pw;

            share1.TryConnectNetwork(ip, id, pw);

        }

        public void Unconnect()
        {
            share1.DisconnectNetwork();
        }
    }

    public class NetworkConnector
    {

        public NETRESOURCE NetResource = new NETRESOURCE();


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NETRESOURCE
        {
            public uint dwScope;
            public uint dwType;
            public uint dwDisplayType;
            public uint dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        [DllImport("mpr.dll", CharSet = CharSet.Auto)]
        public static extern int WNetUseConnection(
                    IntPtr hwndOwner,
                    [MarshalAs(UnmanagedType.Struct)] ref NETRESOURCE lpNetResource,
                    string lpPassword,
                    string lpUserID,
                    uint dwFlags,
                    StringBuilder lpAccessName,
                    ref int lpBufferSize,
                    out uint lpResult);


        [DllImport("mpr.dll", EntryPoint = "WNetCancelConnection2", CharSet = CharSet.Auto)]
        public static extern int WNetCancelConnection2A(String IpName, int dwFlags, int fForce);


        public int TryConnectNetwork(string server, string userID, string pwd) //userid가 아니라 폴더이름!
        {
            int capacity = 1024;
            uint resultFlags = 0;
            uint flags = 0;
            System.Text.StringBuilder sb = new System.Text.StringBuilder(capacity);
            NETRESOURCE ns = new NETRESOURCE();
            ns.dwType = 1;
            ns.lpLocalName = null;
            ns.lpRemoteName = @server;
            ns.lpProvider = null;

            int result = WNetUseConnection(IntPtr.Zero, ref ns, userID, pwd, flags,
                                            sb, ref capacity, out resultFlags);
            return result;
        }

        public void DisconnectNetwork()
        {
            WNetCancelConnection2A(installation_infor.path[1], 1, 0);
        }

    }


}

