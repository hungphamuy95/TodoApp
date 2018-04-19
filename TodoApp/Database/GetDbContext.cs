using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Database
{
    public class GetDbContext
    {
        private readonly IMongoDatabase _database = null;
        public GetDbContext(IOptions<SettingModel> settings)
        {
            var client = new MongoClient(MongoUrl.Create(settings.Value.ConnectionString));
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }
        public IMongoCollection<NoteModel> Notes
        {
            get
            {
                return _database.GetCollection<NoteModel>("Note");
            }
        }
        public IMongoCollection<PostModel> Posts
        {
            get
            {
                return _database.GetCollection<PostModel>("Post");
            }
        }
        public IMongoCollection<TypeNoteModel> TypeNotes
        {
            get
            {
                return _database.GetCollection<TypeNoteModel>("TypeNote");
            }
        }
        public IMongoCollection<UserInfoModel> UserInfo
        {
            get
            {
                return _database.GetCollection<UserInfoModel>("UserInfo");
            }
        }
        public IMongoCollection<BoardModel> Board
        {
            get { return _database.GetCollection<BoardModel>("Board"); }
        }
    }
}
