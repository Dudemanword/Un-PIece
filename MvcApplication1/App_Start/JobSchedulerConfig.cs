using Google.Apis.Auth.OAuth2;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnePieceAbridged.App_Start
{
    public static class JobSchedulerConfig
    {
        private static BackgroundJobServer _backgroundJobServer;

        static JobSchedulerConfig()
        {
            _backgroundJobServer = new BackgroundJobServer();
        }
                
        internal static void RegisterJobs()
        {
            var updateAccessToken = new UpdateAccessToken();
            var updateVideos = new UpdateVideos();

            BackgroundJob.Enqueue<UpdateAccessToken>(x => x.UpdateToken());
            RecurringJob.AddOrUpdate(() => updateAccessToken.UpdateToken(), Cron.Hourly);
        }
    }
}