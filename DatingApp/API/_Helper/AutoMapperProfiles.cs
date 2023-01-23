using System.Linq;
using API.DTO;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API._Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser ,MemberDtos>()
            .ForMember(dest => dest.PhotoUrl , opt => opt.MapFrom(src=> src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age , opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo ,PhotoDtos>();
            CreateMap<MemberUpdateDto,AppUser>();
            CreateMap<RegisterDto,AppUser>();
        }
    }
}