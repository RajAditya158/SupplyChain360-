public class RoleService : IRoleService
{
    private readonly IRoleRepository _repo;

    public RoleService(IRoleRepository repo)
    {
        _repo = repo;
    }

    public List<Role> GetAllRoles(string? roleName = null)
    {
        return _repo.GetAllRoles(roleName);
    }

    public Role AddRole(Role role)
    {
        return _repo.Add(role);
    }

    public void DeleteRoleById(int roleId)
    {
        _repo.DeleteRoleById(roleId);
    }

    public Role GetRoleById(int roleId)
    {
        return _repo.GetRoleById(roleId);
    }

    public Role UpdateRoleById(int roleId, Role role)
    {
        var existingRole = _repo.GetRoleById(roleId);
        if (existingRole == null)
            throw new InvalidOperationException($"Role with id {roleId} was not found.");

        existingRole.RoleName = role.RoleName;

        _repo.Update(existingRole);

        return existingRole;
    }

    public Role PatchRoleById(int roleId, Role role)
    {
        var existingRole = _repo.GetRoleById(roleId);
        if (existingRole == null)
            throw new InvalidOperationException($"Role with id {roleId} was not found.");

        if (!string.IsNullOrEmpty(role.RoleName))
            existingRole.RoleName = role.RoleName;

        _repo.Update(existingRole);

        return existingRole;
    }

}