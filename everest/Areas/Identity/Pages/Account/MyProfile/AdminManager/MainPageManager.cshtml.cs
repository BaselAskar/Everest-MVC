using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using everest.Entities;
using everest.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace everest.Areas.Identity.Pages.Account.MyProfile.AdminManager
{
    public class MainPageManagerModel : PageModel
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uof;

        public MainPageManagerModel(UserManager<AppUser> userManager,IUnitOfWork uof)
        {
            _userManager = userManager;
            _uof = uof;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login?returnUrl=%2FIdentity%2FAccount%2FMyProfile%2FAdminManager%2FMainPageManager");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Contains("Admin"))
            {
                return BadRequest("You are not admin😛");
            }



            return Page();
        }
    }
}
