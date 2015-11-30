using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions
{
    public static class ExtensionMethods
    {
        public static List<BsonDocument> ToBsonDocumentList(this IEnumerable list)
        {
            var bsonList = new List<BsonDocument>();
            foreach (var item in list)
            {
                bsonList.Add(item.ToBsonDocument());
            }
            return bsonList;
        }
    }
}
