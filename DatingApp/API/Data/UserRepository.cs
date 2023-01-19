 using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly Datacontext _context;
    
        private readonly IMapper _mapper;

        public UserRepository(Datacontext context ,IMapper mapper)
        {
            _mapper = mapper;
          
            _context = context;
            
        }

    

        public async Task<MemberDtos> GetMemberAsync(string username)
        {
            return await  _context.User.Where(x => x.UserName == username)
            .ProjectTo<MemberDtos>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public  async Task<IEnumerable<MemberDtos>> GetMemberAsync()
        {
            return  await _context.User.ProjectTo<MemberDtos>(_mapper.ConfigurationProvider).ToListAsync();
            
        }

        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            return await _context.User
            .Include(p => p.Photos)
            .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
           return await _context.User.FindAsync(id);

        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _context.User
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName ==username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public void Update(AppUser user)
        {
           _context.Entry(user).State= EntityState.Modified;
        }
    }
}