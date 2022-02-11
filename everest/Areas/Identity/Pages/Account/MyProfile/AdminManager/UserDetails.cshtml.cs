using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using everest.DTOs;
using everest.Entities;
using everest.Extensions;
using everest.Helpers;
using everest.Interfaces;
using everest.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace everest.Areas.Identity.Pages.Account.MyProfile.AdminManager
{
    [Authorize(Roles = "Admin")]
    public class UserDetailsModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserDetailsModel(UserManager<AppUser> userManager,IUnitOfWork uow,IMapper mapper)
        {
            _userManager = userManager;
            _uow = uow;
            _mapper = mapper;
        }


        public UserDetailsView UserDetails { get; set; }

        public List<RoleName> RolesNames { get; set; } = new List<RoleName>
        {
            new RoleName("Moderator","مشرف"),
            new RoleName("Store","متجر"),
            new RoleName("Clinic","عيادة"),
            new RoleName("Customer","مستخدم")
        };

        public List<ClassificationDto> Classifications { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound("The user is not found!!");

            var userDeteils = _mapper.Map<UserDetailsView>(user);

            var rolesNames = await _userManager.GetRolesAsync(user);

            var roleName = rolesNames.GetRoleName();

            userDeteils.RoleName = roleName;
            userDeteils.DisplayedRoleName = roleName.GetDisplayedRoleName();

            

            var store = await _uow.StoreRepository.GetStoreWithClassificationsAsync(user);

            if (store != null)
            {
                var client = _mapper.Map<ClientView>(store);

                userDeteils.Client = client;
            }

            UserDetails = userDeteils;

            Classifications =(List<ClassificationDto>) await _uow.StoreClassificationRepository.GetStoreClassificatiosAsyc();

            return Page();
        }
    }
}
