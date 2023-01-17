
using System.ComponentModel.DataAnnotations;
using API.Controllers;

namespace API.DTO
{
    public class RegisterDto : BaseApicontroller
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8 , MinimumLength=4)]
        public string Password { get; set; }
    }
}