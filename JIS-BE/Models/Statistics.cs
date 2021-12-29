using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JIS_BE.Models
{
    public class Statistics
    {
        public string Id { get; set; }
        [BsonElement("totalOccurences")]
        public int Count { get; set; }
    }
}
