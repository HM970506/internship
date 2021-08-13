using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using log4net;
using SI.NE.CMM.Logging;
using System.Drawing.Text;

namespace GSIntaller
{
    public partial class MainForm : Form
    {

        public MainForm()
        {

            //input
            this.Controls.Add(input);
            input = new Input();
            input.Location = new Point(-10, 70);
            this.Controls.Add(input);

            log.logSetting();

            try
            {
                InitializeComponent();
                privateFonts = new PrivateFontCollection();
                


                this.CenterToScreen();

                installation_infor.InstallationInputSetting();
                string path = System.Windows.Forms.Application.StartupPath;
                path = Path.GetDirectoryName(path);
                path = Path.GetDirectoryName(path);
                testimage.Image = System.Drawing.Image.FromFile(path + "\\test.png");
                evtInstallstart += Install_start;

            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
            }

        }


        Input input;
        InstallPrepration installpreperation;
        Install install;
        Unzip unzip;
        PrivateFontCollection privateFonts;
        public event EventHandler<bool> evtInstallstart;
        private ILog logger = LogManager.GetLogger(typeof(MainForm)); //모든 파일이 하나씩 들고 있어야 함


        private void next_button_Click(object sender, EventArgs e)
        {
            try
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00033"),
                          string.Format(log.logService.GetEnglishMessage("INST-00033"), "MainForm next button click"));

                if (this.Controls.Contains(input))
                {
                    installation_infor.purpose = input.PurposeCheck();
                    if (installation_infor.purpose < 1 || installation_infor.openfile == null) MessageBox.Show(Message.MainFormNullexist(logger));

                    else
                    {
                        int check = input.inputmanager.SubsystemCheck();
                        if (check == 0)
                        {
                            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00034"),
                                          string.Format(log.logService.GetEnglishMessage("INST-00034"), "MainForm input check success"));

                            installation_infor.subsystem = input.subsystem;

                            if (installation_infor.Check_input())
                            {

                                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00035"),
                                              string.Format(log.logService.GetEnglishMessage("INST-00035"), "MainForm installation_infor check success"));

                                //unzip
                                this.Cursor = Cursors.AppStarting;
                                next_button.Visible = false;

                                unzip = new Unzip();
                                unzip.evtInstallprepertaionstart += installpreperation_start;
                                unzip.Location = new Point(-10, 70);
                                this.Controls.Add(unzip);
                                this.Controls.Remove(input);

                                bool result = false;


                                Task<bool> unzipTask = Task.Run(() =>
                                {
                                    return unzip.UnzipStart();
                                });

                                unzipTask.ContinueWith((task) =>
                                {
                                    result = unzipTask.Result;
                                });
                            }
                            else MessageBox.Show(Message.MainFormNullexist(logger));
                        }
                        else
                        {
                            if (check == 1) MessageBox.Show(Message.MainFormCheck1(logger));
                            else if (check == 2) MessageBox.Show(Message.MainFormCheck2(logger));
                            else if (check == 3) MessageBox.Show(Message.MainFormCheck3(logger));
                            return;
                        }
                    }
                }
                else if (this.Controls.Contains(installpreperation))
                {
                    //id,pw입력창

                    installation_infor.exit = true;
                    Form_ServerIdPwInput fs = new Form_ServerIdPwInput(this);
                    fs.evtInstallstart += Install_start;
                    fs.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
                Application.Exit();
            }
        }


        private void before_button_Click(object sender, EventArgs e)
        {
            try
            {
                this.Controls.Add(input);
                this.Controls.Remove(installpreperation);
                installpreperation.Visible = false;
                input.Visible = true;
                previous_button.Visible = false;
                installation_infor.subsystem_path = new List<string>();
            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
            }
        }


        /// <summary>
        /// install 시작 신호. install의 자동 진행을 위하여 installpreperation가 모두 완료되면 바로 실행된다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Install_start(object sender, bool start)
        {

            if (start)
            {
                this.Controls.Remove(installpreperation);


                install = new Install();
                install.Location = new Point(-10, 70);
                this.Controls.Remove(unzip);
                this.Controls.Add(install);

                cancel_button.Visible = true;
                next_button.Visible = false;
                previous_button.Visible = false;
                install.Invalidate();

                installation_infor.exit = false;

                Task<int> installTask = Task.Run(() =>
                {

                    return install.Start();

                });


                installTask.ContinueWith((task) =>
                {
                    Dictionary<int, string> errorInfo = new Dictionary<int, string>();

                    errorInfo[1] = Message.MainFormErrorInfo1(logger);
                    errorInfo[2] = Message.MainFormErrorInfo2(logger);
                    errorInfo[3] = Message.MainFormErrorInfo3(logger);
                    errorInfo[4] = Message.MainFormErrorInfo4(logger);
                    errorInfo[5] = Message.MainFormErrorInfo5(logger);
                    errorInfo[6] = Message.MainFormErrorInfo6(logger);

                    int check = installTask.Result;
                    if (check >= 1 && check <= 6)
                    {
                        MessageBox.Show(errorInfo[check]);
                        if (check == 6)
                        {
                            Application.Exit();
                        }
                    }
                    else
                    {
                        exit_buttion.Visible = true;
                        cancel_button.Visible = false;
                    }

                }, TaskScheduler.FromCurrentSynchronizationContext()
                );

            }
        }

        private void installpreperation_start(object sender, bool start)
        {
            if (start)
            {
                installpreperation = new InstallPrepration();
                installpreperation.Location = new Point(-10, 70);
                //installpreperation
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        installpreperation_start(sender, start);
                    }));
                }
                else
                {
                    this.Controls.Add(installpreperation);
                    this.Controls.Remove(unzip);
                    next_button.Visible = true;
                    previous_button.Visible = true;
                    this.Cursor = Cursors.Arrow;
                    installpreperation.Start();
                }
            }
        }

        private void exit_buttion_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            try
            {
                installation_infor.cancel = true;
                MessageBox.Show(Message.MainFormCancel(logger));
            }
            catch (Exception ex)
            {
                LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00100"), string.Format(log.logService.GetEnglishMessage("INST-00100"), ex.Message, ex.StackTrace));
            }
        }
    }


}
