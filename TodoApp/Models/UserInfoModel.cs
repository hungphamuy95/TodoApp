using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApp.Models
{
    public class UserInfoModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        public string UserName { get; set;}
        public string PassWord { get; set; }
        public DateTime CreateOn { get; set; }
        public string Description { get; set; }
    }
}
