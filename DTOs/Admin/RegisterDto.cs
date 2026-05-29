using System.ComponentModel.DataAnnotations;

namespace Supplychain.DTOs.Admin
{
    public class RegisterDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; }

        [Required]
        public required string Phone { get; set; }
    }
}