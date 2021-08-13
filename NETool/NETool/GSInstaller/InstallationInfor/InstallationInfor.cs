using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using SI.NE.CMM.Logging;



namespace GSIntaller
{
    public class installation_infor
    {
        public static string id;
        public static string pw;
        public static int purpose;
        public static bool server;
        public static bool client;
        public static List<string> subsystem;
        public static List<string> subsystem_install;
        public static List<string> subsystem_path;
        public static string[] path;
        public static string[] openfile;
        public static List<List<string>> before_hash;
        public static List<List<string>> after_hash;
        public static string server_id;
        public static string server_pw;
        public static bool exit;
        public static bool cancel;

        public static string[] MCS = { "FDS", "MPS", "SCS", "DMS" };
        public static string[] IRPS = { "ICPS", "DIS", "PMS", "UIS" };


        /// <summary>
        /// 초기 세팅 함수. 모든 값을 비운다.
        /// </summary>
        public static void InstallationInputSetting()
        {
            string[] path_ = { Properties.Settings.Default.MCSClient.ToString(),
              Properties.Settings.Default.IRPSClient.ToString(),
            Properties.Settings.Default.MCSServer.ToString(),
            Properties.Settings.Default.IRPSServer.ToString()};

            path = path_;

            openfile = null;
            subsystem = new List<string>();
            subsystem_install = new List<string>();
            subsystem_path = new List<string>();
            before_hash = new List<List<string>>(); ;
            after_hash = new List<List<string>>();
            purpose = -1;
            server = true;
            client = true;
            cancel = false;
        }

        public static string Findroot(string subsystem_name)
        {
            if (MCS.Contains(subsystem_name)) return "MCS";
            else if (IRPS.Contains(subsystem_name)) return "IRPS";
            else return "";
        }

        /// <summary>
        /// subsystem에 해당하는 저장 path로 return
        /// </summary>
        /// <returns></returns>
        public static string Findpath(string subsystem)
        {
            string subsystem_name = Getsubsystemname(subsystem);
            string root = Findroot(subsystem_name);

            if (root.Equals("MCS") && subsystem.Contains("Client")) return path[0];
            else if (root.Equals("IRPS") && subsystem.Contains("Client")) return path[1];
            else if (root.Equals("MCS") && subsystem.Contains("Server")) return path[2];
            else if (root.Equals("IRPS") && subsystem.Contains("Server")) return path[3];
            else return "";

        }

        /// <summary>
        /// step1에 null값이 있는지 확인하는 함수
        /// </summary>
        /// <returns></returns>
        public static bool Check_input()
        {
            if (purpose <= 0) return false;
            //if (Check_Step1_Allfalse()) return false;
            if (subsystem.Count == 0) return false;

            return true;
        }



        /// <summary>
        /// fds_v0.0.1 -> fds
        /// fdsclient 0.0.1 -> fdsclient
        /// </summary>
        /// <param name="subsystem"></param>
        /// <returns></returns>
        public static string Getsubsystem(string subsystem)
        {
            int index = 0;

            if (subsystem.Contains('_')) index = subsystem.IndexOf('_');
            else if (subsystem.Contains(' ')) index = subsystem.IndexOf(' ');

            string now = subsystem.Substring(0, index);
            return now;
        }

        /// <summary>
        /// fds_v0.0.1 -> 0.0.1
        /// </summary>
        /// <param name="subsystem"></param>
        /// <returns></returns>
        public static string Getversion(string subsystem)
        {
            int index = 0;

            index = subsystem.IndexOf('_') + 2;
            int final = subsystem.Length;
            string now = subsystem.Substring(index, final - index);
            return now;
        }
        public static string Getsubsystemname(string subsystem)
        {
            subsystem = subsystem.Replace("Client", "");
            subsystem = subsystem.Replace("Server", "");
            return subsystem;
        }


        /// <summary>
        /// exe파일로부터 빌드 버전을 얻는 함수
        /// </summary>
        /// <param name="client_or_server"></param>
        /// <param name="path"></param>
        /// <param name="subsystem_name"></param>
        /// <returns></returns>
        public static string GetBuildVersion(string path, string subsystem, string subsystem_name)
        {
            string file_name = "SI.E.";

            file_name = file_name + installation_infor.Findroot(subsystem_name) + "." + subsystem_name + ".";
            string file_path = path + "\\" + file_name + subsystem + ".exe";

            if (File.Exists(file_path))
            {
                return FileVersionInfo.GetVersionInfo(file_path).FileVersion;
            }
            return "";
        }

    }
}

