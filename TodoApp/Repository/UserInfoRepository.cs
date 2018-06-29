using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Interfaces;
using TodoApp.Database;
using Microsoft.Extensions.Options;
using TodoApp.Models;
using MongoDB.Driver;
using TodoApp;
using MongoDB.Bson;

namespace TodoApp.Repository
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly GetDbContext _context = null;
        public UserInfoRepository(IOptions<SettingModel> settings)
        {
            _context = new GetDbContext(settings);
        }
        public async Task AddUser(UserInfoModel item)
        {
            try
            {
                await _context.UserInfo.InsertOneAsync(item);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<UserInfoModel>> GetAllUser()
        {
            try
            {
                return await _context.UserInfo.Find(x => true).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserInfoModel> GetSingleUser(string id)
        {
            try
            {
                return await _context.UserInfo.Find(Builders<UserInfoModel>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserInfoModel> GetSingleuserByPassword(UserInfoModel item)
        {
            try
            {
                var filter = Builders<UserInfoModel>.Filter.Eq(x => x.UserName, item.UserName) & Builders<UserInfoModel>.Filter.Eq(x => x.PassWord, Helper.MD5Hash(item.PassWord));
                var retrieveUser = await _context.UserInfo.Find(filter).FirstOrDefaultAsync();
                return retrieveUser != null ? retrieveUser : null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveAll()
        {
            try
            {
                DeleteResult actionResult = await _context.UserInfo.DeleteManyAsync(new BsonDocument());
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Removeuser(string id)
        {
            try
            {
                DeleteResult actinResult = await _context.UserInfo.DeleteOneAsync(Builders<UserInfoModel>.Filter.Eq("_id", id));
                return actinResult.IsAcknowledged && actinResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateUser(string id, UserInfoModel item)
        {
            try
            {
                var updatedItem = new UserInfoModel()
                {
                    _id=id,
                    UserName=item.UserName,
                    PassWord=item.PassWord,
                    Description=item.Description
                };
                ReplaceOneResult actionResult = await _context.UserInfo.ReplaceOneAsync(Builders<UserInfoModel>.Filter.Eq("_id", id), updatedItem, new UpdateOptions() { IsUpsert = true });
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
