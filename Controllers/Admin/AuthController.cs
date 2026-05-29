using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supplychain.DTOs.Admin;

[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    private readonly IAuditService _audit;

    public AuthController(IAuthService service, IAuditService audit)
    {
        _service = service;
        _audit = audit;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        var result = _service.Register(dto);

        if (!result)
            return BadRequest("User already exists");

        _audit.Log(null, "User Registered");

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid request");

        var result = _service.Login(dto);

        if (result == null || string.IsNullOrEmpty(result.Token))
        {
            return Unauthorized("Invalid credentials");
        }
        _audit.Log(GetUserId(), "Login Success");

        return Ok(new
        {
            token = result.Token,
            name = result.Name,
            email = result.Email,
            role = result.Role
        });
    }

    private int? GetUserId()
    {
        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null) return null;

        if (int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }

        return null;
    }
}


