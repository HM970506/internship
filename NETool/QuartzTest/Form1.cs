using QuartzTest.Scheduler;
using SI.NE.CMM.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuartzTest
{
    public partial class Form1 : Form
    {
        JobScheduler jobScheduler = new JobScheduler();

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            Scheduler.NETime.InitTimer(new Scheduler.NETimeConfig() {
                TestStartTime = DateTime.Parse("2022-05-10 01:00:40"),
                Type = Scheduler.NETimeType.UseNetworkTestTime,
                NETimeServerIP = "127.0.0.1",
                NETimeServerPort = 4567
            });


            // 스케줄 입력            
            jobScheduler.AddSchedule(new JobScheduleInfo(ScheduleJob1(), "ScheduleJob1", new TimeSpan(1, 1, 0)));
            jobScheduler.AddSchedule(new JobScheduleInfo(ScheduleJob2(), "ScheduleJob2", new TimeSpan(1, 2, 0)));
            jobScheduler.Start();

            // timer 시작
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private Action ScheduleJob1()
        {
            return () =>
            {
                UpdateLog(string.Format("ScheduleJob1!! --> {0}", Scheduler.NETime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")));
            };
        }

        private Action ScheduleJob2()
        {
            return () =>
            {
                UpdateLog(string.Format("ScheduleJob2!! --> {0}", Scheduler.NETime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")));
            };
        }

        /// <summary>
        /// 화면에 시간 전시
        /// </summary>
        /// <param name="time"></param>
        private void UpdateCurrentTime(string time)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => { UpdateCurrentTime(time); }));
            }
            else
            {
                currentTimeLabel.Text = time;
            }
        }

        /// <summary>
        /// 로그전시
        /// </summary>
        /// <param name="time"></param>
        private void UpdateLog(string log)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => { UpdateLog(log); }));
            }
            else
            {
                logTextBox.AppendText(string.Format("[{0}] {1}\r\n", Scheduler.NETime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), log));
            }
        }

        /// <summary>
        /// Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                Thread.Sleep(1000);
                UpdateCurrentTime(Scheduler.NETime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));                
            }
        }
    }
}
