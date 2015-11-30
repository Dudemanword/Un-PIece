using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions
{
    public class MongoDbInteraction : IDatabaseInteraction
    {
        private readonly MongoDbConnection _connection;
        private readonly MongoClient _mongo;
        public MongoDbInteraction(MongoDbConnection connection, MongoClient mongo)
        {
            _connection = connection ?? new MongoDbConnection();
            _mongo = mongo ?? new MongoClient("mongodb://localhost");
        }
        public async Task Log<T>(T objectToStore)
        {
            var database = _mongo.GetDatabase(_connection.DatabaseName);
            var collection = database.GetCollection<BsonDocument>(_connection.CollectionName);
            var document = objectToStore.ToBsonDocument();
            await collection.InsertOneAsync(document);
        }

        public async Task LogMany<T>(IEnumerable<T> objectsToStore)
        {
            var database = _mongo.GetDatabase(_connection.DatabaseName);
            var collection = database.GetCollection<BsonDocument>(_connection.CollectionName);
            var documentList = objectsToStore.ToBsonDocumentList();
            await collection.InsertManyAsync(documentList);
        }

        public Task<List<T>> FindAll<T>()
        {
            var database = _mongo.GetDatabase(_connection.DatabaseName);
            var collection = database.GetCollection<T>(_connection.CollectionName);
            return collection.Find(_ => true).ToListAsync();
        }

        public Task Replace<T1, T2>(T1 _id, T2 updatedObject)
        {
            var database = _mongo.GetDatabase(_connection.DatabaseName);
            var collection = database.GetCollection<BsonDocument>(_connection.CollectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", _id);
            return collection.ReplaceOneAsync(filter, updatedObject.ToBsonDocument());
        }
    }
}
