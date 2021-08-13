using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using SI.NE.CMM.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzTest.Scheduler
{
    /// <summary>
    /// Schedule 정보
    /// </summary>
    /// <author>jcyoo</author>
    /// <date>2021-01-04</date>
    public class JobScheduleInfo
    {
        public TimeSpan TimeToTrigger { get; set; }
        public string ActionName { get; set; }
        public Action Action { get; set; }

        public JobScheduleInfo()
        {

        }

        public JobScheduleInfo(Action action, string actionName, TimeSpan time)
        {
            this.Action = action;
            this.ActionName = actionName;
            this.TimeToTrigger = time;
        }
    }

    /// <summary>
    /// 정해진 시간에 특정 Action 을 수행하는 스케줄러
    /// </summary>
    /// <author> Lee Won Jun (wjlee@satreci.com) </author>
    /// <date> 2020-09-02 </date>
    public class JobScheduler
    {
        private List<JobScheduleInfo> jobScheduleInfoList = new List<JobScheduleInfo>();

        /// <summary>
        /// Job List 추가
        /// </summary>
        /// <param name="jobInfo"></param>
        public void AddSchedule(JobScheduleInfo jobInfo)
        {
            jobScheduleInfoList.Add(jobInfo);
        }

        /// <summary>
        /// UTC 시간을 NETime과 동기화
        /// </summary>
        public void SyncNETime()
        {
            SystemTime.UtcNow = () => NETime.UtcNow;
            SystemTime.Now = () => NETime.Now;

            Trace.WriteLine("-> Quartz Time : " + Quartz.SystemTime.UtcNow());
        }

        /// <summary>
        /// 현재 시간 출력
        /// </summary>
        public void PrintTime()
        {
            Trace.WriteLine("-> Quartz Time : " + Quartz.SystemTime.UtcNow());
        }

        /// <summary>
        /// service 시작
        /// </summary>        
        /// <author>jcyoo</author>
        /// <date>2020-11-24</date>
        public void Start()
        {
            SystemTime.UtcNow = () => NETime.UtcNow;
            SystemTime.Now = () => NETime.Now;

            foreach (JobScheduleInfo task in jobScheduleInfoList)
            {
                RegisterSchedule(task).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Schedule을 Quartz에 등록
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        /// <author>jcyoo</author>
        /// <date>2020-11-24</date>
        private async Task RegisterSchedule(JobScheduleInfo jobInfo)
        {
            LogProvider.IsDisabled = true;

            // construct a scheduler factory
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            JobKey schedulerJobKey = new JobKey(string.Format("JobKey_{0}", jobInfo.ActionName), "Scheduler");

            Dictionary<string, JobScheduleInfo> taskInfo = new Dictionary<string, JobScheduleInfo>();
            taskInfo.Add(jobInfo.ActionName, jobInfo);

            // define the job and tie it to our HelloJob class            
            IJobDetail job = JobBuilder.Create<ActionJob>()
                .WithIdentity(schedulerJobKey)
                .UsingJobData(new JobDataMap(taskInfo))     // task 정보 넘기기
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(string.Format("Trigger_{0}", jobInfo.ActionName), "Scheduler")
                .ForJob(schedulerJobKey)
                .WithSchedule(CronScheduleBuilder
                    .DailyAtHourAndMinute(jobInfo.TimeToTrigger.Hours, jobInfo.TimeToTrigger.Minutes)
                    .InTimeZone(TimeZoneInfo.Utc))
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
