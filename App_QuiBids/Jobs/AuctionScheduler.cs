using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_QuiBids.Jobs
{
    public class AuctionScheduler
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            var job = JobBuilder.Create<AuctionJobs>().Build();
            var trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule(
                    builder =>
                        builder.WithIntervalInSeconds(1)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))).Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}