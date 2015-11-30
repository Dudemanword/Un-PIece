using DatabaseInteractions;
using MongoDB.Driver;
using OnePieceAbridged.Controllers;
using OnePieceAbridged.Controllers.Videos;
using System;
using System.Collections.Generic;

namespace OnePieceAbridged.App_Start
{
    internal class UpdateVideos
    {
        private readonly YoutubeOperations _youtubeOperations;
        private readonly IDatabaseInteraction _databaseInteraction;

        public UpdateVideos() : this(new MongoDbInteraction(new MongoDbConnection { CollectionName = "videos", DatabaseName = "StrawHatEntertainment" },
                                                             new MongoClient("mongodb://localhost")), new YoutubeOperations())
        { }
        public UpdateVideos(IDatabaseInteraction databaseInteractions = null, YoutubeOperations youtubeOperations = null)
        {
            _youtubeOperations = youtubeOperations ?? new YoutubeOperations();
            _databaseInteraction = databaseInteractions ?? new MongoDbInteraction(new MongoDbConnection { CollectionName = "videos", DatabaseName = "StrawHatEntertainment" },
                                                             new MongoClient("mongodb://localhost"));
        }

        internal void GetVideosAndLogToDatabase(TokenInfo tokenInfo)
        {
            var videoList = _youtubeOperations.GetVideoList(tokenInfo, new List<Video>());

            _databaseInteraction.LogMany(videoList.ToBsonDocumentList());
        }
    }
}