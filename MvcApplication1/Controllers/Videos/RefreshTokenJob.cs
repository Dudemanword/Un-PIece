using System;
using Quartz;
using Google.Apis.Auth.OAuth2;
using System.Threading;

namespace OnePieceAbridged.App_Start
{
    internal class RefreshTokenJob : IJob
    {
        
        public void Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap;
            var credential = (UserCredential)dataMap["credential"];
            credential.RefreshTokenAsync(CancellationToken.None);
        }
    }
}