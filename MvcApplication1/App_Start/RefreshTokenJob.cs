using System;
using Quartz;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using System.Web;

namespace OnePieceAbridged.App_Start
{
    internal class RefreshTokenJob : IJob
    {
        
        public void Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;
            var credential = (UserCredential)HttpRuntime.Cache.Get("credential");
            credential.RefreshTokenAsync(CancellationToken.None);
        }
    }
}