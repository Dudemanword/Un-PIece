using Google.Apis.Auth.OAuth2;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnePieceAbridged.App_Start
{
    public static class JobSchedulerConfig
    {
        private static IScheduler _scheduler;

        static JobSchedulerConfig()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        public static void Start()
        {
            _scheduler.Start();
        }

        internal static void ScheduleRefreshJob(UserCredential credential)
        {
            var job = JobBuilder.Create<RefreshTokenJob>().Build();
            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(int.Parse(credential.Token.RefreshToken)))
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }
    }
}