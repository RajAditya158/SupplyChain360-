using System.ComponentModel.DataAnnotations;

namespace Supplychain.DTOs.Admin
{
    public class LoginDto
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }

    }
}