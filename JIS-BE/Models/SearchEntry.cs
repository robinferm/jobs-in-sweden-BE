using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JIS_BE.Models
{
    public class SearchEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Searchstring { get; set; }
        public DateTime TimeStamp { get; private set; } = DateTime.Now;
    }
}
