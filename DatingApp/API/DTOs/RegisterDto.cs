
using System.ComponentModel.DataAnnotations;
using API.Controllers;

namespace API.DTO
{
    public class RegisterDto : BaseApicontroller
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}