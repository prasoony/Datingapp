using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApicontroller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository  , IMapper mapper )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            
        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDtos>>> Getusers()
        {
         var user = await _userRepository.GetMemberAsync();
         return Ok(user);
           

        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDtos>> Getusers(string username)
        {
           return  await _userRepository.GetMemberAsync(username);
            

        }

    }
}