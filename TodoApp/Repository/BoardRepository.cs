using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Interfaces;
using TodoApp.Database;
using Microsoft.Extensions.Options;
using TodoApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TodoApp.Repository
{
    public class BoardRepository : IBoardRespository
    {
        private readonly GetDbContext _context;
        public BoardRepository(IOptions<SettingModel>settings)
        {
            _context = new GetDbContext(settings);
        }
        public async Task AddBoard(BoardModel item)
        {
            try
            {
                await _context.Board.InsertOneAsync(item);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BoardModel>> GetAllBoard()
        {
            try
            {
                return await _context.Board.Find(_ => true).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BoardModel> GetSingleBoard(string id)
        {
            try
            {
                return await _context.Board.Find(Builders<BoardModel>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
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
                DeleteResult actionResult = await _context.Board.DeleteManyAsync(new BsonDocument());
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveBoard(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Board.DeleteOneAsync(Builders<BoardModel>.Filter.Eq("_id", id));
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateBoard(string id, BoardModel item)
        {
            try
            {
                var updatedItem = new BoardModel() {
                    _id = id,
                    IsPublic=item.IsPublic,
                    Title=item.Title,
                    TypeId=item.TypeId,
                    UpdatedOn=DateTime.Now
                };
                ReplaceOneResult actionResult = await _context.Board.ReplaceOneAsync(Builders<BoardModel>.Filter.Eq("_id", id), updatedItem, new UpdateOptions() { IsUpsert = true });
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
