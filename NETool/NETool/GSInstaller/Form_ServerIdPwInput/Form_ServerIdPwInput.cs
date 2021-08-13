using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using SI.NE.CMM.Logging;

namespace GSIntaller
{
    public partial class Form_ServerIdPwInput : Form
    {
        public Form_ServerIdPwInput(MainForm Parent)
        {
            InitializeComponent();
            this.CenterToParent();
            this.Owner = Parent;
            mainform = this.Owner as MainForm;
        }

        MainForm mainform;
        public event EventHandler<bool> evtInstallstart;
        private ILog logger = LogManager.GetLogger(typeof(Form_ServerIdPwInput)); //모든 파일이 하나씩 들고 있어야 함


        private void ok_button_Click(object sender, EventArgs e)
        {
            try
            {
                this.Visible = false;
                string id_text = id_textbox.Text;
                string pw_text = pw_textbox.Text;

                if (id_text != "" && pw_text != "")
                {
                    installation_infor.server_id = id_text;
                    installation_infor.server_pw = pw_text;

                    evtInstallstart(this, true);
                    this.Close();
                }
                else MessageBox.Show(Message.ServerIdPwInputNullerror(logger));
            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
            }
        }
    }
}
