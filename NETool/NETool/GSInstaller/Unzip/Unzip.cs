using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SI.NE.CMM.Logging;
using log4net;

namespace GSIntaller
{
    public partial class Unzip : UserControl
    {


        public Unzip()
        {
            InitializeComponent();
            unzipmanager = new UnzipManager();
            unzipmanager.UpdateProgressLog += Unzipmanager_UpdateProgressLog;
            unzipmanager.evtInstallprepertaionstart_unzipmanager += evtInstallprepertaionstart_unzip;

            LogGenerator.ShowTrace = true;
            LogGenerator.ProgramName = "GSInstaller";

            LogAppenderManager appenderManager = new LogAppenderManager
            {
                LogPort = 10001,
                ProgramName = "GSInstaller",
                LogFilePath = @"C:\GSInstaller"
            };
            appenderManager.InitLogAppender();

        }

        public event EventHandler<bool> evtInstallprepertaionstart;
        private ILog logger = LogManager.GetLogger(typeof(Unzip)); 
        public UnzipManager unzipmanager;


        public void evtInstallprepertaionstart_unzip(object sender, bool start)
        {
            evtInstallprepertaionstart(sender, start);
        }


        private void Unzipmanager_UpdateProgressLog(object sender, string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(delegate () {
                    Unzipmanager_UpdateProgressLog(sender, text);
                }));
            }
            else this.unzip_label.Text = text;
        }


        public bool UnzipStart()
        {
            try {
                bool result = false;
                Task<bool> unzipTask = Task.Run(() =>
                 {
                     return unzipmanager.MakeTempAndUnzip();
                 });


                unzipTask.ContinueWith((task) =>
                {
                    result = unzipTask.Result;
                });
                return true;
            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
                return false;
            }
        }

    }
}
