using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.Mongo;

[assembly: OwinStartup(typeof(OnePieceAbridged.App_Start.Startup))]

namespace OnePieceAbridged.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseMongoStorage("mongodb://localhost", "StrawHatEntertainment");
            app.UseHangfireDashboard("/jobs");
            JobSchedulerConfig.RegisterJobs();
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
