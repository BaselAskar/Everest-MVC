using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using everest.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace everest.Areas.Identity.Pages.Account.MyProfile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public IndexModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string UserName { get; set; }
        public string RoleName { get; set; }

        private async Task LoadUser(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            UserName = userName;

            var roles = await _userManager.GetRolesAsync(user);

            var roleName = roles.Contains("Admin") ? "المدير" :
                           roles.Contains("Moderator") ? "مشرف" :
                           roles.Contains("Store") ? "مدير مخزن" :
                           roles.Contains("Clinic") ? "مدير عيادة" :
                           "مستخدم";

            RoleName = roleName;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FMyProfile");
            }

            await LoadUser(user);
            return Page();
        }
    }
}
