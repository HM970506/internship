using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using log4net;
using SI.NE.CMM.Logging;

namespace GSIntaller
{
    public partial class Install : UserControl
    {
        public Install()
        {
            InitializeComponent();
            pb = progressBar1;
            pb.Visible = true;
            sc = script_textbox;

            installmanager = new InstallManager();

            installmanager.evtUpdateProgress += install_evtUpdateProgress;
            installmanager.evtUpdateLog += install_evtUpdateLog;
            installmanager.evtUpdateLabel += install_evtUpdateLabel;

        }

        private ILog logger = LogManager.GetLogger(typeof(Install)); //모든 파일이 하나씩 들고 있어야 함


        public int Start()
        {
            return installmanager.Start();
        }

        private void install_evtUpdateProgress(object sender, bool progress)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new MethodInvoker(delegate () { install_evtUpdateProgress(sender, progress); }));
            else
            {
                if (progress) pb.PerformStep();
            }
        }

        private void install_evtUpdateLog(object sender, string log)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new MethodInvoker(delegate () { install_evtUpdateLog(sender, log); }));
            else sc.AppendText(log);
        }

        private void install_evtUpdateLabel(object sender, string text)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new MethodInvoker(delegate () { install_evtUpdateLabel(sender, text); }));
            else state_label.Text = text;
        }

        public InstallManager installmanager;

        public static ProgressBar pb;
        public static TextBox sc;


        private void script_textbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                script_textbox.SelectionStart = script_textbox.Text.Length;
                script_textbox.ScrollToCaret();
            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
            }
        }
    }
}
