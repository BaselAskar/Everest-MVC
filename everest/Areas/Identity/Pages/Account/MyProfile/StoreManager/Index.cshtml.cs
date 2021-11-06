using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using everest.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace everest.Areas.Identity.Pages.Account.MyProfile.StoreManager
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FMyProfile%2FStoreManager");
            }

            if (!await _userManager.IsInRoleAsync(user, "Store"))
            {
                return NotFound("Sorry you are not store manager🙄🙄🙄!!");
            }

            return Page();
        }
    }
}
