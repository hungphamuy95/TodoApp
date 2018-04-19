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
    [Route("api/[controller]")]
    public class TypesController : Controller
    {
        private readonly ITypeNoteRepository _typeRepo;
        public TypesController(ITypeNoteRepository typeRepo)
        {
            _typeRepo = typeRepo;
        }
        [HttpGet]
        public async Task<IEnumerable<TypeNoteModel>> GetAllTypes()
        {
            return await _typeRepo.GetAllTypeNote();
        }
    }
}