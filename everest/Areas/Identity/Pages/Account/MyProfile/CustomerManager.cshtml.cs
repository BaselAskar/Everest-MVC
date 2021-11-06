using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using everest.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using everest.DTOs;
using Microsoft.EntityFrameworkCore;
using everest.Interfaces;

namespace everest.Areas.Identity.Pages.Account.MyProfile
{
    public class CustomerManagerModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uof;

        public CustomerManagerModel(UserManager<AppUser> userManager, IUnitOfWork uof)
        {
            _userManager = userManager;
            _uof = uof;
        }

        public List<CustomerManagerDto> CustomerManagerDtos { get; set; } = new List<CustomerManagerDto>();

        public List<RoleDto> RolesList { get; set; } = new List<RoleDto>
        {
            new RoleDto{Name="Moderator",DisplayedName="مشرف"},
            new RoleDto{Name="Store",DisplayedName="مدير متجر"},
            new RoleDto{Name="Clinic",DisplayedName="مدير عيادة"},
            new RoleDto{Name="Customer",DisplayedName="مستخدم"},
        };

        public List<string> ClassificationList { get; set; } = new List<string>();


        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);

            if (currentUser == null)
            {
                return Redirect("/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FMyProfile%2FCustomerManager");
            }

            if (!currentUserRoles.Contains("Admin"))
            {
                return NotFound("You are not admin 😖😖");
            }


            var users = await _userManager.Users.Where(u => u != currentUser).ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var roleName = roles.Contains("Moderator") ? "مشرف":
                               roles.Contains("Store") ? "مدير متجر":
                               roles.Contains("Clinic") ? "مدير عيادة":
                                "مستخدم";


                var customer = new CustomerManagerDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    RoleName = roleName,
                    Created = user.Created.ToString("dd MMMM yyyy")
                };

                CustomerManagerDtos.Add(customer);

            }

            var classifications = await _uof.ClassificationRepository.GetClassifications();

            foreach(var c in classifications)
            {
                var classificationString = $"{c.Title}-{c.Name}";

                ClassificationList.Add(classificationString);
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Contains(role)) return BadRequest($"This user is already {role}");

            await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (role != "Customer")
            {

                var newRoles = new List<string> { role, "Customer" };

                await _userManager.AddToRolesAsync(user, newRoles);

                return Page();
            }

            await _userManager.AddToRoleAsync(user, role);

            return Page();
        }
    }
}
