using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data.Models.Account;
using Data.Context;
using Microsoft.AspNetCore.Authorization;
using ContosoUniversity.Data.Repository;

namespace ContosoUniversity.Web.Pages.Users
{

    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepo;

        public IndexModel(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
           //if (_context.Users != null)
            {
                User = _accountRepo.getUsers();
            }
        }
    }
}
