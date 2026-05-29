using Supplychain.Data;
using Microsoft.EntityFrameworkCore;
namespace Supplychain.Repository.Admin.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SupplyChainContext _context;

        public RoleRepository(SupplyChainContext context)
        {
            _context = context;
        }

        public List<Role> GetAllRoles(string? roleName = null)
        {
            var query = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(roleName))
            {
                var pattern = $"%{roleName}%";
                query = query.Where(r => EF.Functions.Like(r.RoleName, pattern));
            }

            return query.ToList();
        }

        public Role Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public void DeleteRoleById(int id)
        {
            var r = _context.Roles.Find(id);
            if (r != null)
            {
                _context.Roles.Remove(r);
                _context.SaveChanges();
            }
        }

        public Role GetRoleById(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (role == null)
            {
                throw new InvalidOperationException($"Role with id {id} was not found.");
            }

            return role;
        }

        public void Update(Role existingRole)
        {
            _context.Roles.Update(existingRole);
            _context.SaveChanges();
        }
    }
}