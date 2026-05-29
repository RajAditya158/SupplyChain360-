namespace Supplychain.DTOs.Admin
{
    public class UserDto
    {
        public required string Name { get; set; }
        public required string Role { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; internal set; }

    }
}