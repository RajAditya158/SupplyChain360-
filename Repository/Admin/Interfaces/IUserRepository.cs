public interface IUserRepository
{
    List<User> GetAllUsers();
    User Add(User user);
    void DeleteUserById(int id);

    User GetUserById(int id);

    User UpdateUserById(int id, User user);
    User PatchUserById(int id, User user);
    User UpdateUser(User user);

    User GetByEmail(string email);


}