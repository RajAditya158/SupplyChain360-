public interface IRoleRepository
{
    List<Role> GetAllRoles(string? roleName = null);
    Role Add(Role role);
    void DeleteRoleById(int id);

    Role GetRoleById(int id);
    void Update(Role existingRole);
}