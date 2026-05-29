using Supplychain.Data;

public class UserRepository : IUserRepository
{
    private readonly SupplyChainContext _context;

    public UserRepository(SupplyChainContext context)
    {
        _context = context;
    }
    public List<User> GetAllUsers() => _context.Users.ToList();

    public User Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public void DeleteUserById(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public User GetUserById(int id) => _context.Users.Find(id);

    public User UpdateUserById(int id, User user)
    {
        var existingUser = _context.Users.Find(id);
        if (existingUser == null) return null;

        existingUser.Name = user.Name;
        existingUser.Role = user.Role;
        existingUser.Email = user.Email;
        existingUser.Phone = user.Phone;

        _context.SaveChanges();
        return existingUser;
    }

    public User PatchUserById(int id, User user)
    {
        var existingUser = _context.Users.Find(id);
        if (existingUser == null) return null;

        if (!string.IsNullOrEmpty(user.Name))
            existingUser.Name = user.Name;

        if (!string.IsNullOrEmpty(user.Role))
            existingUser.Role = user.Role;

        if (!string.IsNullOrEmpty(user.Email))
            existingUser.Email = user.Email;

        if (!string.IsNullOrEmpty(user.Phone))
            existingUser.Phone = user.Phone;

        _context.SaveChanges();
        return existingUser;
    }

    public User UpdateUser(User user)
    {
        var existingUser = _context.Users.Find(user.UserId);
        if (existingUser == null) return null;

        existingUser.Name = user.Name;
        existingUser.Role = user.Role;
        existingUser.Email = user.Email;
        existingUser.Phone = user.Phone;

        _context.SaveChanges();
        return existingUser;
    }
    public User GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }


}