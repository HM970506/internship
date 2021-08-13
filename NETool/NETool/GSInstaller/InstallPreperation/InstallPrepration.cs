using System;
using System.Configuration;
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
    public partial class InstallPrepration : UserControl
    {
        public InstallPrepration()
        {
            InitializeComponent();
        }
        public InstallPreperationManager instalpreperationmanager;
        private ILog logger = LogManager.GetLogger(typeof(InstallPrepration)); //모든 파일이 하나씩 들고 있어야 함

        public void Start()
        {
            try {
                instalpreperationmanager = new InstallPreperationManager();
                if (GV_input.DataSource != null) ((DataTable)GV_input.DataSource).Rows.Clear();
                if (GV_exist.DataSource != null) ((DataTable)GV_exist.DataSource).Rows.Clear();

                instalpreperationmanager.Start(GV_input, GV_exist);
            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
            }
        }
    }
}