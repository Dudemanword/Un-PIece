using System.Data;
using MongoDB.Driver;

namespace DatabaseInteractions
{
    public class MongoDbConnection
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}