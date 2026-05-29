using Supplychain.Data;
using Supplychain.DTOs.Admin;
using System.Linq;
using System.Collections.Generic;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public List<User> GetAllUsers(UserSearchDto searchDto)
    {
        var query = _repository.GetAllUsers().AsQueryable();

        if (!string.IsNullOrEmpty(searchDto.Name))
            query = query.Where(u => u.Name.Contains(searchDto.Name));

        if (!string.IsNullOrEmpty(searchDto.Email))
            query = query.Where(u => u.Email.Contains(searchDto.Email));

        if (!string.IsNullOrEmpty(searchDto.Role))
            query = query.Where(u => u.Role == searchDto.Role);

        return query.ToList();
    }
    public User GetUserById(int id)
    {
        return _repository.GetUserById(id);
    }

    public User CreateUser(User user) => _repository.Add(user);

    public User UpdateUserById(int id, User user) => _repository.UpdateUserById(id, user);

    public User PatchUserById(int id, User user) => _repository.PatchUserById(id, user);

    public bool DeleteUserById(int id)
    {
        var foundUser = _repository.GetUserById(id);
        if (foundUser == null) return false;

        _repository.DeleteUserById(id);
        return true;
    }

    public User? Login(LoginDto loginDto)
    {
        var user = _repository.GetByEmail(loginDto.Email);
        if (user == null || user.Password != loginDto.Password)
            return null;

        return user;
    }
}

