using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{


    public class BuggyController : BaseApicontroller
    {
        private readonly Datacontext _context;

        public BuggyController(Datacontext context)
        {
            _context = context;

        }
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret Text ";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotfound()
        {
             var thing= _context.User.Find(-1);
             if(thing==null) return NotFound();
             return thing;
             
        }
        [HttpGet("Server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing= _context.User.Find(-1);
             var thingToReturn =thing.ToString();
             return thingToReturn;
        }
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This is not Good request");
        }


    }
}