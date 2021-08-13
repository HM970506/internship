using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace GSIntaller
{
    public class InstallPreperationManager
    {
        public DataGridView GV_input;
        public DataGridView GV_exist;
        public Label print_label;

        /// <summary>
        /// 초기 세팅 함수
        /// </summary>
        /// <param name="GV_input"></param>
        /// <param name="GV_exist"></param>
        public void Start(DataGridView GV_input, DataGridView GV_exist)
        {
            this.GV_input = GV_input;
            this.GV_exist = GV_exist;
            GridviewSetting();
        }

        /// <summary>
        /// subsystem을 각각 unzip파일과 installed파일에서 찾아와 출력
        /// </summary>
        /// <returns></returns>
        public bool GridviewSetting()
        {
            //User input subsystem
            foreach (string now_subsystem_path in installation_infor.subsystem_path)
            {
                 DirectoryInfo di = new DirectoryInfo(now_subsystem_path);
                foreach(DirectoryInfo di_inner in di.GetDirectories())
                {
                    string subsystem = "";
                    string buildversion_path = "";

                    subsystem = di_inner.Name;
                    buildversion_path = now_subsystem_path + "\\" + subsystem;

                    string subsystem_name = installation_infor.Getsubsystemname(subsystem);

                    string buildversion = "";
                    string path = "";

                    if (subsystem.Contains("Client"))
                    {
                        buildversion = installation_infor.GetBuildVersion(buildversion_path, subsystem, subsystem_name);
                        path = installation_infor.Findpath(subsystem);
                    }
                    else if (subsystem.Contains("Server"))
                    {
                        buildversion = installation_infor.GetBuildVersion(buildversion_path, subsystem, subsystem_name);
                        path = installation_infor.Findpath(subsystem);
                    }

                    GV_input.Rows.Add(subsystem, buildversion, path);
                }               
            }

            //PC exist subsystem
            foreach (string now_path in installation_infor.path)
            {
                DirectoryInfo di = new DirectoryInfo(now_path);
                foreach (DirectoryInfo di_inner in di.GetDirectories())
                {
                    string subsystem = Path.GetFileNameWithoutExtension(di_inner.Name);
                    string subsystem_name = installation_infor.Getsubsystemname(subsystem);

                    string buildversion = "";

                    string path = now_path;

                    if (subsystem.Contains("Client"))buildversion = installation_infor.GetBuildVersion(now_path + "\\" + subsystem, subsystem, subsystem_name);
                    else if (subsystem.Contains("Server")) buildversion = installation_infor.GetBuildVersion(now_path + "\\" + subsystem, subsystem, subsystem_name);

                    if(!subsystem.Equals("Backup")) GV_exist.Rows.Add(subsystem, buildversion, path);
                }
            }
            return true;
        }
    }
}
