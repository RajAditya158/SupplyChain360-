public interface IRoleService
{
    List<Role> GetAllRoles(string? roleName = null);

    Role GetRoleById(int roleId);

    Role UpdateRoleById(int roleId, Role role);
    Role PatchRoleById(int roleId, Role role);

    Role AddRole(Role role);
    void DeleteRoleById(int roleId);
}
