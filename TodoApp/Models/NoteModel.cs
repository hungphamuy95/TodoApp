using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApp.Models
{
    public class NoteModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Test { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
