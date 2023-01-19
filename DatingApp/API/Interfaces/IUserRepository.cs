using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user) ;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUserAsync();
        
        Task<AppUser>GetUserByIdAsync(int id);
        
        Task<AppUser>GetUserByUserNameAsync(string username);

        Task<IEnumerable<MemberDtos>>GetMemberAsync();

        Task<MemberDtos>GetMemberAsync(string username);

    }
}