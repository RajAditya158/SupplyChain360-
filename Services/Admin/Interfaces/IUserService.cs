using Supplychain.DTOs.Admin;
using System.Collections.Generic;

public interface IUserService
{
    List<User> GetAllUsers(UserSearchDto searchDto);
    User GetUserById(int id);
    User CreateUser(User user);
    User UpdateUserById(int id, User user);
    User PatchUserById(int id, User user);
    bool DeleteUserById(int id);

    User? Login(LoginDto loginDto);
}
