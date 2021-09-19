using GenshinPray.Util;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Timer
{
    public static class TimerManager
    {
        private static ISchedulerFactory schedulerFactory;
        private static IScheduler scheduler;

        public static void StartAllJob()
        {
            schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.Start();
            StartClearJob(scheduler);
        }

        private static void StartClearJob(IScheduler scheduler)
        {
            LogHelper.Info($"正在初始化定时清理任务...");
            IJobDetail job = JobBuilder.Create<ClearJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ClearTrigger", "DailyGroup")
                .StartNow()
                .WithCronSchedule("0 0 2 * * ?")
                .Build();
            scheduler.ScheduleJob(job, trigger).Wait();
        }



    }
}
