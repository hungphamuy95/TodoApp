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
using MongoDB.Driver.Linq;

namespace TodoApp.Repository
{
    public class NoteRepository:INoteRepository
    {
        private readonly GetDbContext _context = null;
        public NoteRepository(IOptions<SettingModel>settings)
        {
            _context = new GetDbContext(settings);
        }

        public async Task AddNote(NoteModel item)
        {
            try
            {
                await _context.Notes.InsertOneAsync(item);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<NoteModel>> GetAllNotes()
        {
            try
            {
                return await _context.Notes.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetAllNotesByUser(string userid)
        {
            try
            {
                var query = from p in _context.UserInfo.AsQueryable()
                            join o in _context.Notes on p.NoteRef equals o.UserId into joined
                            where p._id == userid
                            select new { p.UserName, p._id, joined };
                var res = await query.SingleOrDefaultAsync();
                return res;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NoteModel> GetNote(string id)
        {
            var filter = Builders<NoteModel>.Filter.Eq("_id", id);
            try
            {
                return await _context.Notes.Find(filter).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveAllNotes()
        {
            try
            {
                DeleteResult actionResult = await _context.Notes.DeleteManyAsync(new BsonDocument());
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveNote(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Notes.DeleteOneAsync(Builders<NoteModel>.Filter.Eq("_id", id));
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateNote(string id, string body)
        {
            var filter = Builders<NoteModel>.Filter.Eq("_id", id);
            var update = Builders<NoteModel>.Update.Set(s => s.Body, body).CurrentDate(s => s.UpdatedOn);
            try
            {
                UpdateResult actionResult = await _context.Notes.UpdateOneAsync(filter, update);
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateNoteDocument(string id, NoteModel item)
        {
            var updatedItem = new NoteModel
            {
                _id=id,
                Body=item.Body,
                Test=item.Test,
                UserId=item.UserId,
                UpdatedOn=DateTime.Now
            };
                ReplaceOneResult actionResult = await _context.Notes.ReplaceOneAsync(Builders<NoteModel>.Filter.Eq("_id", id), updatedItem, new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            
        }
    }
}
