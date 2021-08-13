using Quartz;
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
    /// 하나의 Action 을 수행하는 Job, Quartz 가 Job 단위로 수행하기 때문에, 타 서브 시스템에서 Quartz 설치없이 사용하게 하기위한 Job 을 미리 만들어둠.
    /// </summary>
    /// <author> Lee Won Jun (wjlee@satreci.com) </author>
    /// <date> 2020-09-02 </date>
    public class ActionJob : IJob
    {
        /// <summary>
        /// 수행할 Action
        /// </summary>
        static public Action Action { get; set; }
        /// <summary>
        /// Action 수행
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns>Task</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            if (dataMap.Values.Count == 0)
            {
                Trace.WriteLine("Wrong task info");
                return;
            }

            // 등록된 Action 실행
            JobScheduleInfo scheduleInfo = dataMap.Values.ToArray()[0] as JobScheduleInfo;

            // 임시 출력
            Trace.WriteLine(string.Format("{0}. Executed Time : {1}. Scheduled Time : {2}", scheduleInfo.ActionName, NETime.UtcNow, scheduleInfo.TimeToTrigger.ToString()));

            scheduleInfo.Action();
        }
    }
}
