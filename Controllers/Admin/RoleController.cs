using System.Data;
using Microsoft.AspNetCore.Mvc;
using Supplychain.DTOs.Admin;

[ApiController]
[Route("api/v1/roles")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;
    private readonly IAuditService _audit;

    public RoleController(IRoleService service, IAuditService audit)
    {
        _service = service;
        _audit = audit;
    }

    [HttpGet]
    public IActionResult GetAllRoles([FromQuery] string? roleName)
    {
        _audit.Log(null, "Viewed all roles");

        var roleList = _service.GetAllRoles(roleName);
        return Ok(roleList);
    }

    [HttpGet("{id}")]
    public IActionResult GetRoleById(int id)
    {
        var foundRole = _service.GetRoleById(id);

        if (foundRole == null)
            return NotFound("Role not found");

        _audit.Log(null, $"Viewed role: {foundRole.RoleName}");

        return Ok(foundRole);
    }

    [HttpPost]
    public IActionResult AddRole([FromBody] RoleDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid role data");

        var role = new Role
        {
            RoleName = dto.RoleName
        };

        var result = _service.AddRole(role);

        _audit.Log(null, $"Added role: {role.RoleName}");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoleById(int id)
    {
        var foundRole = _service.GetRoleById(id);

        if (foundRole == null)
            return NotFound("Role not found");

        _service.DeleteRoleById(id);

        _audit.Log(null, $"Deleted role: {foundRole.RoleName}");

        return Ok("Role Deleted");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRoleById(int id, [FromBody] RoleDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid role data");

        var role = _service.GetRoleById(id);

        if (role == null)
            return NotFound("Role not found");

        role.RoleName = dto.RoleName;
        _service.UpdateRoleById(id, role);

        _audit.Log(null, $"Updated role: {role.RoleName}");

        return Ok(new { roleName = role.RoleName });
    }

    [HttpPatch("{id}")]
    public IActionResult PatchRoleById(int id, [FromBody] RoleDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid role data");

        var role = _service.GetRoleById(id);

        if (role == null || id <= 0)
            return NotFound("Role not found");

        if (!string.IsNullOrEmpty(dto.RoleName))
            role.RoleName = dto.RoleName;

        _service.PatchRoleById(id, role);

        _audit.Log(null, $"Partially updated role: {role.RoleName}");

        return Ok(new { roleName = role.RoleName });
    }
}