using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;
using TodoApp.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoApp.Database;
using Microsoft.Extensions.Options;

namespace TodoApp.Repository
{
    public class TypeNoteRepository : ITypeNoteRepository
    {
        private readonly GetDbContext _context = null;

        Task<IEnumerable<TypeNoteModel>> ITypeNoteRepository.TypeAndNoteChildren => throw new NotImplementedException();

        public TypeNoteRepository(IOptions<SettingModel> settings)
        {
            _context = new GetDbContext(settings);
        }
        public async Task AddTypeNote(TypeNoteModel item)
        {
            try
            {
                await _context.TypeNotes.InsertOneAsync(item);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TypeNoteModel>> GetAllTypeNote()
        {
            try
            {
                return await _context.TypeNotes.Find(x => true).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TypeNoteModel> GetSingleTypeNote(string id)
        {
            try
            {
                var filter = Builders<TypeNoteModel>.Filter.Eq("_id", id);
                return await _context.TypeNotes.Find(filter).FirstOrDefaultAsync();
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
                DeleteResult actionResult = await _context.TypeNotes.DeleteManyAsync(new BsonDocument());
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveTypeNote(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.TypeNotes.DeleteOneAsync(Builders<TypeNoteModel>.Filter.Eq("_id", id));
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateTypeNote(string id, TypeNoteModel item)
        {
            try
            {
                var updatedItem = new TypeNoteModel
                {
                    UpdatedOn=DateTime.Now,
                    Title=item.Title,
                    NoteId=item.NoteId
                };
                ReplaceOneResult actionResult = await _context.TypeNotes.ReplaceOneAsync(Builders<TypeNoteModel>.Filter.Eq("_id", id), updatedItem, new UpdateOptions { IsUpsert=true});
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<TypeNoteModel>> TypeAndNoteChildren()
        {
            try
            {
                //var query = from p in _context.Notes.AsQueryable()
                //            join o in _context.TypeNotes.AsQueryable()
                //            on p._id equals o.NoteId into NotesChildrens
                //            select new TypeNoteModel()
                //            {
                //                Title=
                //            }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
