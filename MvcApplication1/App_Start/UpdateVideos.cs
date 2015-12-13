using DatabaseInteractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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
            var result = _databaseInteraction.FindAll<BsonDocument>().Result;
            var videoList = _youtubeOperations.GetVideoList(tokenInfo);

            if (result == null)
            {
                _databaseInteraction.Log(videoList);
            }

            else
            {
                var oldVideoList = BsonSerializer.Deserialize<Videos>(result[0]);
                _databaseInteraction.ReplaceById(oldVideoList._id, videoList);
            }
        }
    }
}