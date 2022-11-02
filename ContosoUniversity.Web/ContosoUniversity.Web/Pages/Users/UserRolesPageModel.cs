using ContosoUniversity.Data.Models;
using ContosoUniversity.Data.Models.Account;
using ContosoUniversity.Data.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ContosoUniversity.Web.Pages.Users
{
    public class UserRolesPageModel : PageModel
    {
        List<AssignedUserRoles> assignedUserRoles;

        public List<AssignedUserRoles> PopulateAssignedUserRoles(IAccountRepository repo,User user)
        {
            assignedUserRoles = new List<AssignedUserRoles>();

            var allRoles = repo.getAllRoles();

            foreach(var role in allRoles)
            {
                AssignedUserRoles assignedRole = new AssignedUserRoles();
                assignedRole.Name = role.Name;
                assignedRole.RoleID = role.ID;

                if (user.UserRoles.Any(u => u.Roles.ID == role.ID))
                    assignedRole.isAssigned = true;
                else
                    assignedRole.isAssigned = false;

                assignedUserRoles.Add(assignedRole);
            }


            return assignedUserRoles;
        }
    }
}
