﻿using System;
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
using System.Net;
using TodoApp;

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

        public async Task<IEnumerable<object>> GetAllNotes(int page, int pageSize)
        {
            try
            {
                var total = _context.Notes.Find(_ => true).Count();
                var skip = pageSize * (page - 1);
                var canPage = skip < total;
                if (!canPage)
                {
                    return null;
                }
                var query = from p in _context.Notes.AsQueryable() select new { p.Title, p._id, p.CreatedOn, p.Test, p.ImageUrl, p.seoUrl };
                var res = await query.OrderByDescending(x => x.CreatedOn).Skip(skip).Take(pageSize).ToListAsync();
                return res;
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

        public async Task<IEnumerable<object>> GetAllNoteTitle()
        {
            try
            {
                var query = from p in _context.Notes.AsQueryable() select new { p._id, p.Title, p.CreatedOn };
                return await query.OrderByDescending(x=>x.CreatedOn).ToListAsync();
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

        public async Task<int> GetNumberOfPage()
        {
            try
            {
                int numOfPage;
                var numOfRecord = _context.Notes.Find(_ => true).Count();
                var temp = numOfRecord % 3;
                if (temp == 0)
                {
                    numOfPage = (int)Math.Floor((decimal)numOfRecord / 3);
                }
                else
                {
                    numOfPage = (int)Math.Floor((decimal)numOfRecord / 3) + 1;
                }

                var numOfPageFactory = await Task.FromResult<int>(numOfPage);
                return numOfPageFactory;
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
            var query = from p in _context.Notes.AsQueryable() where (p._id==id) select new { p.CreatedOn, p.UserId };
            var res = await query.FirstOrDefaultAsync();
            var updatedItem = new NoteModel
            {
                _id = id,
                Body = WebUtility.HtmlEncode(item.Body),
                Test = item.Test,
                UserId = res.UserId,
                UpdatedOn = DateTime.Now,
                ImageUrl = item.ImageUrl,
                Title = item.Title,
                CreatedOn = res.CreatedOn,
                seoUrl = Helper.RemoveUnicode(item.Title)
            };
                ReplaceOneResult actionResult = await _context.Notes.ReplaceOneAsync(Builders<NoteModel>.Filter.Eq("_id", id), updatedItem, new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            
        }
    }
}
