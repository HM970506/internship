using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using log4net;
using SI.NE.CMM.Logging;

namespace GSIntaller
{
    public partial class Input : UserControl
    {
        public Input()
        {
            InitializeComponent();
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "zip files (*.zip)|*.zip"; //zip파일만 open가능
            subsystem = new List<string>();
            inputmanager = new InputManager();

            if (Properties.Settings.Default.Restore.ToString().Equals("OFF")) restore_checkbox.Visible = false;
        }
        public InputManager inputmanager;
        public string id = "";
        public string pw = "";
        public List<string> subsystem;
        public string[] openfile;

        private ILog logger = LogManager.GetLogger(typeof(Input)); //모든 파일이 하나씩 들고 있어야 함


        /// <summary>
        /// 설치인지, 업데이트인지, 백업인지 체크박스를 확인하여 해당 값으로 return
        /// </summary>
        /// <returns></returns>
        public int PurposeCheck()
        {
            if (download_checkbox.Checked) return 1;
            else if (update_checkbox.Checked) return 2;
            else if (restore_checkbox.Checked) return 3;
            else return -1;
        }

        private void open_button_Click(object sender, EventArgs e)
        {
            try {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    openfile = openFileDialog1.FileNames;
                    if (openfile != null)
                    {
                        dataGridView1.Rows.Clear();
                        subsystem.Clear();
                        for (int x = 0; x < openfile.Length; x++)
                        {
                            string now_subsystem = Path.GetFileNameWithoutExtension(openfile[x]);
                            dataGridView1.Rows.Add(now_subsystem);
                            subsystem.Add(now_subsystem);
                        }
                        installation_infor.openfile = openfile;
                    }
                }
            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
            }
        }
    }


}
