using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Supplychain.Data;
using System.Linq;
using Supplychain.DTOs.Admin;

public class AuthService : IAuthService
{
    private readonly SupplyChainContext _context;
    private readonly IConfiguration _config;
    private readonly IAuditService _audit;

    public AuthService(
        SupplyChainContext context,
        IConfiguration config,
        IAuditService audit)
    {
        _context = context;
        _config = config;
        _audit = audit;
    }

    public LoginResponse Login(LoginDto dto)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email == dto.Email && u.Password == dto.Password);

        if (user == null || user.UserId == 0)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        var claims = new[]
        {
            new Claim("UserId", user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var jwtKey = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
            throw new InvalidOperationException("JWT key is not configured.");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey)
        );

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        if (user.UserId > 0)
            _audit.Log(user.UserId, "Login Success");

        return new LoginResponse
        {
            Token = jwt,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public bool Register(RegisterDto dto)
    {
        var exists = _context.Users.Any(u => u.Email == dto.Email);
        if (exists) return false;

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role,
            Phone = dto.Phone,
            Status = UserStatus.Active
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        _audit?.Log(user.UserId, "User Registered");

        return true;
    }
}