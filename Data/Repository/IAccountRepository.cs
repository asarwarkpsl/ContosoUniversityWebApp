using ContosoUniversity.Data.Models.Account;

namespace ContosoUniversity.Data.Repository
{
    public interface IAccountRepository
    {
        void AddUser(User user);
        void AssignRoles(User user, Roles[] roles);
        void DeleteUser(User user);
        bool isEmailVerified(User user);
        bool IsUserExists(string userName);
        User getUserByID(int UserID);
        List<User> getUsers();

        User Login(string userName, string MD5password);
        void Save();
        void UpdateUser(User user);
        bool ValidatePassword(string userName, string MD5password);
    }
}