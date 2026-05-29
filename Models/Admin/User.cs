using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class User
{

    public int UserId { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }

    public required string Password { get; set; }

    public UserStatus Status { get; set; } = UserStatus.Active;
}