using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApp.Models
{
    public class PostModel
    {
        [BsonId]
        public string Id { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
