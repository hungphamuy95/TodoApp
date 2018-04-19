using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Database;
using Newtonsoft.Json;
using TodoApp.Models;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TodoApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private IConfiguration _configuration;
        public UserController(IUserInfoRepository userInfoRepository, IConfiguration configuration)
        {
            _userInfoRepository = userInfoRepository;
            _configuration = configuration;
        }
        [HttpGet]
        [Authorize]
        public string GetAllUser()
        {
            var currentUser = HttpContext.User;
            return currentUser.Claims.FirstOrDefault().Value.ToString();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> CreateToken([FromBody]UserInfoModel item)
        {
            IActionResult response = Unauthorized();
            var user = await _userInfoRepository.GetSingleuserByPassword(item);
            if (user != null)
            {
                var tokenstring = "Bearer " + BuildToken(item);
                return new JsonResult(new { YourToken = tokenstring});
            }
            return new JsonResult(new { message="incorrect account or password"});
        }
        #region Helper
        private string BuildToken(UserInfoModel item)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, item.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}