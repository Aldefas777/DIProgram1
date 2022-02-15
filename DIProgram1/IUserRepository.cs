namespace DIProgram1
{
    public interface IUserRepository
    {
        List<User> GetUsers();

        void GetUserById(int? id);
        void AddUsers(string names);
        void UpdateUser(string names, int? id);
        void DeleteUser(int? id);
    }
}
