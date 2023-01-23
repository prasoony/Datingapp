
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApicontroller
    {
        private readonly ITokenservice _tokenservice;
        private readonly IMapper _mapper;
        private readonly Datacontext _Context;
        public AccountController(Datacontext context ,ITokenservice tokenservice , IMapper mapper)
        {
            _tokenservice = tokenservice;
             _mapper = mapper;
            _Context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto _rto)
        {
            if (await UserExists(_rto.Username)) return BadRequest("user not valid");

            var user =_mapper.Map<AppUser>(_rto);

            using var hmac = new HMACSHA512();
            
                user.UserName = _rto.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(_rto.Password));
                user.PasswordSalt = hmac.Key;
                user.KnownAs=_rto.KnownAs;

            _Context.User.Add(user);
            await _Context.SaveChangesAsync();
            return new UserDto
            {
                Username=user.UserName,
                Token=_tokenservice.CreateToken(user),
                KnownAs=user.KnownAs

            };

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto _login)
        {
            var user = await _Context.User.Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == _login.Username);
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
                PhotoUrl=user.Photos.FirstOrDefault(x=> x.IsMain)?.Url,
                KnownAs=user.KnownAs

            };
        }

        private async Task<bool> UserExists(string Username)
        {
            return await _Context.User.AnyAsync(x => x.UserName== Username.ToLower());


        }


    }
}