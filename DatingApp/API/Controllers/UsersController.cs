using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApicontroller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
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
            return await _userRepository.GetMemberAsync(username);


        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _userRepository.GetUserByUserNameAsync(User.GetUsername());
            if (user == null) return NotFound();
            _mapper.Map(memberUpdateDto, user);
            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to Update User");

        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDtos>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUserNameAsync(User.GetUsername());

            if (user == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var Photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicID = result.PublicId
            };
           // AppUser users = new();
            if (user.Photos.Count == 0) Photo.IsMain = true;


            user.Photos.Add(Photo);

            if (await _userRepository.SaveAllAsync()) 
            {
            return  CreatedAtAction(nameof(Getusers), new {username = user.UserName},_mapper.Map<PhotoDtos>(Photo));
            }


            return BadRequest("Problem Add photos");


        }
        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> setMainPhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUserNameAsync(User.GetUsername());
            if(user==null) return NotFound();
            var Photo = user.Photos.FirstOrDefault(x=>x.ID==photoId);
            if(Photo==null) return NotFound();
            if(Photo.IsMain) return BadRequest("This is already your main photo");
            var CurrentMain =user.Photos.FirstOrDefault(x=>x.IsMain);
            if(CurrentMain!=null)CurrentMain.IsMain=false;
            Photo.IsMain=true;
            if(await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Problem setting the main photo");

        }
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUserNameAsync(User.GetUsername());
             var Photo = user.Photos.FirstOrDefault(x =>x.ID ==photoId);

            if(Photo==null) return NotFound();
            if(Photo.IsMain) return BadRequest("You Cannot delete Your main Photo");
            if(Photo.PublicID!=null)
            {
                var result = await _photoService.DeletePhotoAsync(Photo.PublicID);
                if(result.Error!=null) return BadRequest(result.Error.Message);
            }
             user.Photos.Remove(Photo);

             if(await _userRepository.SaveAllAsync()) return Ok();

             return BadRequest("Prolbem deleting photo");
        }



    }
}