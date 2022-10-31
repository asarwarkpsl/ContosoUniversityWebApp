using ContosoUniversity.Data.Models.Account;
using ContosoUniversity.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ContosoUniversity.Web.Pages.Account
{
    public class VerifyModel : PageModel
    {
        private readonly IAccountRepository _repository;

        bool _isEmailVerified = false;

        public VerifyModel(IAccountRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public bool IsEmailVerified { get => _isEmailVerified; set => _isEmailVerified = value; }

    
        public void OnPost(int id)
        {
            try
            {
                User _user = _repository.getUserByID(id);
                _user.EmailVerified = true;

                _repository.UpdateUser(_user);

                IsEmailVerified = true;
            }
            catch (Exception)
            {
                IsEmailVerified = false;
            }
        }
    }
}
