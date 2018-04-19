using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TodoApp.Models;

namespace TodoApp.Models
{
    public class BoardModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsPublic { get; set; }
        public string UserId { get; set; }
        public List<string> TypeId { get; set; }
    }
}
