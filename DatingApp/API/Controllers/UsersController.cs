using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApicontroller
    {
        private readonly Datacontext _context;
        public UsersController(Datacontext context)
        {
            _context = context;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> Getusers()
        {
            var users = await _context.User.ToListAsync();
            return users;

        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<AppUser>> Getusers(int ID)
        {
            var users = await _context.User.FindAsync(ID);
            return users;

        }

    }
}