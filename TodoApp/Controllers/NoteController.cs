using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Interfaces;
using TodoApp.Database;
using Newtonsoft.Json;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{ver:apiVersion}/[controller]")]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        [HttpGet, AllowAnonymous]
        [Route("getallnotes/{page}")]
        public Task<IEnumerable<NoteModel>> Get(int page)
        {
            return GetNoteFactor(page);
        }
        private async Task<IEnumerable<NoteModel>> GetNoteFactor(int page)
        {
            var notes = await _noteRepository.GetAllNotes(page);
            return notes;
        }

        [HttpGet("{id}")]
        [Authorize]
        public Task<NoteModel>GetSingleNote(string id)
        {
            return GetSingleNoteFactor(id);
        }
        private async Task<NoteModel>GetSingleNoteFactor(string id)
        {
            var note = await _noteRepository.GetNote(id) ?? new Models.NoteModel();
            return note;
        }
        [HttpPost]
        [Authorize]
        public void AddNewNote([FromBody]NoteModel item)
        {
            _noteRepository.AddNote(new NoteModel()
            {
                Test = item.Test,
                Body = item.Body,
                CreatedOn = DateTime.Now,
                ImageUrl = item.ImageUrl,
                Title = item.Title,
                UserId = HttpContext.User.Claims.Skip(2).FirstOrDefault().Value.ToString()
        });
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<bool> UpdateNote(string id, [FromBody]NoteModel item)
        {
            return await _noteRepository.UpdateNoteDocument(id, item);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<bool> NoteDelelte(string id)
        {
            return await _noteRepository.RemoveNote(id);
        }

        [Route("getnotesbyuser")]
        [HttpGet]
        [Authorize]
        public async Task<object> GetNotesByUser()
        {
            var userId = HttpContext.User.Claims.Skip(1).FirstOrDefault().Value.ToString();
            return await _noteRepository.GetAllNotesByUser(userId);
        }
    }
}