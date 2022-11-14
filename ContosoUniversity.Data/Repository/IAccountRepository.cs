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
        User getUserByID(int? UserID);
        public Task<IEnumerable<User>> getUsersAsync();
        List<Roles> getAllRoles();

        User Login(string userName, string MD5password);
        public Task SaveChangesAsync();
        void UpdateUser(User user);
        bool ValidatePassword(string userName, string MD5password);
    }
}