using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using everest.Entities;
using everest.Extensions;
using everest.Helpers;
using everest.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace everest.Areas.Identity.Pages.Account.MyProfile.AdminManager
{
    [Authorize(Roles = "Admin")]
    public class CustomersManagerModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CustomersManagerModel(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }




        public PageList<UserView> UsersPage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            var adminUser = await _userManager.GetUserAsync(User);



            var usersQuery = _userManager.Users
                .Where(user => user.Id != adminUser.Id)
                .OrderByDescending(user => user.Created)
                .Select(user => _mapper.Map<UserView>(user).UpdateRoleName());

            UsersPage = await PageList<UserView>.CreateAsync(usersQuery, 1, 10);

            return Page();
        }
    }
}
