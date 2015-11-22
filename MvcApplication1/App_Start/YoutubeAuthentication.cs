using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace OnePieceAbridged.App_Start
{
    public class YoutubeAuthenticaion
    {
        public void GenerateAuthenticationAndCreateService()
        {
            var credential = GenerateCredential().Result;
            HttpRuntime.Cache.Insert("credential", credential, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            GenerateYoutubeService(credential);
            GenerateRefreshTokenScheduler(credential);
        }

        private async Task<UserCredential> GenerateCredential()
        {
            UserCredential credential;
            var fileName = HttpContext.Current.Server.MapPath("~/Controllers/Videos/client_secrets.json");
            var timeToRefreshToken = "3600";
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new[] { YouTubeService.Scope.YoutubeReadonly },
                "user",
                CancellationToken.None,
                new FileDataStore(GetType().ToString())
               );

                credential.Token.RefreshToken = timeToRefreshToken;
            }

            return credential;
        }

        private void GenerateYoutubeService(UserCredential credential)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            HttpRuntime.Cache.Insert("youtubeService", youtubeService, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
        }

        private void GenerateRefreshTokenScheduler(UserCredential credential)
        {
            credential.Token.RefreshToken = "3600";
            JobSchedulerConfig.ScheduleRefreshJob(credential);
        }
    }
}