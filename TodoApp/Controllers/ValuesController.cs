using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Repository;
using TodoApp.Database;
using TodoApp.Interfaces;
using Newtonsoft.Json;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly ITypeNoteRepository _typeNoteRepository;
        private readonly IUserInfoRepository _userInfoRepi;
        public ValuesController(INoteRepository noteRepository, ITypeNoteRepository typeNoteRepository, IUserInfoRepository userInfoRepository)
        {
            _noteRepository = noteRepository;
            _typeNoteRepository = typeNoteRepository;
            _userInfoRepi = userInfoRepository;
        }
        // GET api/values
        [HttpGet]
        public Task<string> Get()
        {
            return GetNoteFactor();
        }

        private async Task<string> GetNoteFactor()
        {
            var notes = await _noteRepository.GetAllNotes();
            return JsonConvert.SerializeObject(notes);
        }

        // GET api/values/5
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting == "init")
            {
                List<string> refData = new List<string>();
                refData.Add("5ad030c77999a11ad81272aa");
                refData.Add("5ad030c87999a11ad81272ab");
                _noteRepository.RemoveAllNotes();
                _typeNoteRepository.RemoveAll();
                _userInfoRepi.RemoveAll();
                _userInfoRepi.AddUser(new UserInfoModel()
                {
                    NoteRef=Guid.NewGuid().ToString(),
                    UserName="admin",
                    PassWord="admin",
                    CreateOn=DateTime.Now,
                    Description="test"
                });
                _userInfoRepi.AddUser(new UserInfoModel()
                {
                    NoteRef = Guid.NewGuid().ToString(),
                    UserName = "admin1",
                    PassWord = "admin1",
                    CreateOn = DateTime.Now,
                    Description = "test"
                });
                _userInfoRepi.AddUser(new UserInfoModel()
                {
                    NoteRef = Guid.NewGuid().ToString(),
                    UserName = "admin2",
                    PassWord = "admin2",
                    CreateOn = DateTime.Now,
                    Description = "test"
                });
                _noteRepository.AddNote(new NoteModel()
                {
                    Body = "TestNote1",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    //UserId = 1
                });
                _noteRepository.AddNote(new NoteModel()
                {
                    Body = "TestNote2",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    //UserId = 2
                });
                _noteRepository.AddNote(new NoteModel()
                {
                    Body = "TestNote3",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    //UserId = 3
                });
                _typeNoteRepository.AddTypeNote(new TypeNoteModel()
                {
                    CreatedOn = DateTime.Now,
                    Title="Đã làm",
                    NoteId=refData
                });
                return "Done";
            }
            return "Undknow";
        }

        // POST api/values
        [HttpPost("{accountKey}")]
        public void Post()
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
