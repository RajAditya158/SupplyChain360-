using Microsoft.AspNetCore.Mvc;
using Supplychain.DTOs.Admin;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllUsers([FromQuery] UserSearchDto searchDto)
    {
        var userList = _service.GetAllUsers(searchDto);
        return Ok(userList);
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserById(int userId)
    {
        var foundUser = _service.GetUserById(userId);
        if (foundUser == null || userId <= 0)
            return NotFound($"User with ID {userId} not found");
        return Ok(foundUser);
    }

    [HttpPost]
    public IActionResult AddUser([FromBody] UserDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid user data");

        var user = new User
        {
            Name = dto.Name,
            Role = dto.Role,
            Email = dto.Email,
            Phone = dto.Phone ?? string.Empty,
            Password = dto.Password ?? string.Empty
        };

        var createdUser = _service.CreateUser(user);
        return Ok(createdUser);
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUserById(int userId, [FromBody] UserDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid user data");

        var user = new User
        {
            UserId = userId,
            Name = dto.Name,
            Role = dto.Role,
            Email = dto.Email,
            Phone = dto.Phone ?? string.Empty,
            Password = dto.Password ?? string.Empty
        };

        var updatedUser = _service.UpdateUserById(userId, user);

        if (updatedUser == null || userId <= 0)
            return NotFound($"User with ID {userId} not found");

        return Ok(updatedUser);
    }

    [HttpPatch("{userId}")]
    public IActionResult PatchUserById(int userId, [FromBody] UserPatchDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid user data");

        var user = new User
        {
            UserId = userId,
            Name = dto.Name ?? string.Empty,
            Role = dto.Role ?? string.Empty,
            Email = dto.Email ?? string.Empty,
            Phone = dto.Phone ?? string.Empty,
            Password = dto.Password ?? string.Empty
        };

        var result = _service.PatchUserById(userId, user);

        if (result == null || userId <= 0)
            return NotFound($"User with ID {userId} not found");

        return Ok(result);
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUserById(int userId)
    {
        var success = _service.DeleteUserById(userId);

        if (!success || userId <= 0)
            return NotFound($"User with ID {userId} not found");

        return Ok("User deleted successfully");
    }
}