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
        //[HttpGet("{page}")]
        //public Task<string> Get(int page)
        //{
        //    return GetNoteFactor(page);
        //}

        //private async Task<string> GetNoteFactor(int page)
        //{
        //    var notes = await _noteRepository.GetAllNotes(page);
        //    return JsonConvert.SerializeObject(notes);
        //}

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
                    Title= "Suarez, Cavani vô duyên, Uruguay thắng nhọc Ai Cập",
                    Test= "Pha đánh đầu vào cuối trận của Jose Gimenez giúp đội bóng Nam Mỹ thắng 1-0 trong trận đấu mà đối thủ không có ngôi sao số một Mo Salah.",
                    ImageUrl= "https://i-thethao.vnecdn.net/2018/06/15/Untitled-15-7061-1529072362.jpg"
                    //UserId = 1
                });
                _noteRepository.AddNote(new NoteModel()
                {
                    Body = "TestNote2",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    Title= "Aleksandr Golovin - nguồn cảm hứng chiến thắng của tuyển Nga",
                    Test= "Ngôi sao Alan Dzagoev sớm rời sân vì chấn thương, nhưng với Golovin tỏa sáng rực rỡ ở khu vực trung tâm, Nga đã khởi đầu tưng bừng World Cup 2018.",
                    ImageUrl= "https://i-thethao.vnecdn.net/2018/06/15/Golovin-1263-1529047946.jpg"
                    //UserId = 2
                });
                _noteRepository.AddNote(new NoteModel()
                {
                    Body = "TestNote3",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    Title= "HLV Bồ Đào Nha: 'Thay HLV chẳng ảnh hưởng đến sức mạnh của Tây Ban Nha'",
                    Test= "Fernando Santos bác bỏ luận điểm rằng việc thay tướng trước thềm World Cup 2018 sẽ khiến đối thủ tại bảng B yếu đi.",
                    ImageUrl= "https://i-thethao.vnecdn.net/2018/06/15/Untitled-3774-1529028274.jpg"
                    //UserId = 3
                });
                _noteRepository.AddNote(new NoteModel()
                {
                    Body="TestNote 4",
                    CreatedOn=DateTime.Now,
                    UpdatedOn=DateTime.Now,
                    Title= "Quân đội Uruguay làm video cổ vũ đội nhà ở World Cup 2018",
                    Test= "Người Uruguay thể hiện niềm đam mê bóng đá với cách ủng hộ đặc biệt trước trận ra quân gặp Ai Cập.",
                    ImageUrl= "https://i-thethao.vnecdn.net/2018/06/15/Screen-Shot-2018-06-15-at-11-4-8499-5729-1529038037.png"
                });
                _noteRepository.AddNote(new NoteModel()
                {
                    Body="TestNote 5",
                    CreatedOn=DateTime.Now,
                    UpdatedOn=DateTime.Now,
                    Title= "Đội tuyển Tây Ban Nha và nụ cười trong tâm bão",
                    Test= "Trung vệ Sergio Ramos cùng HLV tạm quyền Fernando Hierro kết thúc cuộc họp báo trước trận đấu với Bồ Đào Nha ở World Cup 2018 với một nụ cười. ",
                    ImageUrl= "https://i-thethao.vnecdn.net/2018/06/15/Ramos-7341-1529042302.jpg"
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
