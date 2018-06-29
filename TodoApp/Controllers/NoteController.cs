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
using System.Net;
using TodoApp;

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
        [Route("getnumpage")]
        public async Task<JsonResult> GetNumPage()
        {
            return new JsonResult(new { numpage = await _noteRepository.GetNumberOfPage() });
        }
        [HttpGet, AllowAnonymous]
        [Route("getallnotes/{page}&{pageSize}")]
        public Task<IEnumerable<object>> Get(int page, int pageSize)
        {
            return GetNoteFactor(page, pageSize);
        }
        private async Task<IEnumerable<object>> GetNoteFactor(int page, int pageSize)
        {
            var notes = await _noteRepository.GetAllNotes(page, pageSize);
            return notes;
        }
        [HttpGet]
        [Route("getnotetitle")]
        public async Task<IEnumerable<object>> GetNoteTitile()
        {
            return await _noteRepository.GetAllNoteTitle();
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<JsonResult>GetSingleNote(string id)
        {
            var singleNote = await GetSingleNoteFactor(id);


            return new JsonResult(new { id = singleNote._id, title = singleNote.Title, body = WebUtility.HtmlDecode(singleNote.Body), shortcontent = singleNote.Test, updatedon = singleNote.UpdatedOn, createdon=singleNote.CreatedOn, userid= singleNote.UserId, imageurl = singleNote.ImageUrl });
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
                Body = WebUtility.HtmlEncode(item.Body),
                CreatedOn = DateTime.Now,
                ImageUrl = item.ImageUrl,
                Title = item.Title,
                UserId = HttpContext.User.Claims.Skip(2).FirstOrDefault().Value.ToString(),
                seoUrl=Helper.RemoveUnicode(item.Title)
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