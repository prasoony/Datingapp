
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.Controllers
{
    public class AccountController : BaseApicontroller
    {
        private readonly ITokenservice _tokenservice;

        private readonly Datacontext _Context;
        public AccountController(Datacontext context ,ITokenservice tokenservice)
        {
            _tokenservice = tokenservice;
            _Context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto _rto)
        {
            if (await UserExists(_rto.Username)) return BadRequest("user not valid");
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = _rto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(_rto.Password)),
                PasswordSalt = hmac.Key
            };
            _Context.User.Add(user);
            await _Context.SaveChangesAsync();
            return new UserDto
            {
                Username=user.UserName,
                Token=_tokenservice.CreateToken(user),

            };

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto _login)
        {
            var user = await _Context.User.SingleOrDefaultAsync(x => x.UserName == _login.Username);
            if (user == null) 
            {
            return Unauthorized("invalid user name");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(_login.Password));
            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");

            }
            return new UserDto
            {
                Username=user.UserName,
                Token=_tokenservice.CreateToken(user),

            };
        }

        private async Task<bool> UserExists(string Username)
        {
            return await _Context.User.AnyAsync(x => x.UserName== Username.ToLower());


        }


    }
}