using ContosoUniversity.Data.Models.Account;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SchoolContext _context;

        public AccountRepository(SchoolContext context)
        {
            _context = context;
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public bool isEmailVerified(User user)
        {
            User _user = _context.Users.Find(user.ID);
            return _user.EmailVerified;
        }

        public bool IsUserExists(string userName)
        {
            return _context.Users.Where(x => x.UserName == userName).Any();
        }

        public async Task<IEnumerable<User>> getUsersAsync()
        {
            return await _context.Users
                           .Where(a => a.Deleted == false)
                           .Include(s => s.UserRoles)
                           .ToListAsync();
        }

        public List<Roles> getAllRoles()
        {
            return _context.Roles
                            .ToList();
        }
        public User getUserByID(int? UserID)
        {
            var user = _context.Users
                               .Include(r => r.UserRoles)
                               .Where(a => a.ID == UserID && a.Deleted == false);

            if (user != null)
                return user.FirstOrDefault();
            else
                return null;
        }

        public User Login(string userName, string MD5password)
        {
            User loggedinUser = _context.Users.Where(_user => _user.UserName == userName && _user.Password == MD5password)
                                            .Include(s => s.UserRoles)      
                                            .ThenInclude(u => u.Roles)
                                            .FirstOrDefault();

            return loggedinUser;
        }

        public bool ValidatePassword(string userName, string MD5password)
        {
            User _user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();

            if (_user != null)
            {
                if (_user.Password == MD5password)
                    return true;
                else
                    return false;
            }

            return false;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }
        public void AssignRoles(User user, Roles[] roles)
        {
            foreach (Roles role in roles)
            {
                UserRoles _roles = new()
                {
                    User = user,
                    Roles = role
                };

                _context.UserRoles.Add(_roles);
            }
        }
        public void DeleteUser(User user)
        {
            user.Deleted = true;
            _context.Users.Update(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
