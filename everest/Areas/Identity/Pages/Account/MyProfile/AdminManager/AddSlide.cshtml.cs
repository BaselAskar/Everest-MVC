using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using everest.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace everest.Areas.Identity.Pages.Account.MyProfile.AdminManager
{
    [Authorize(Roles = "Admin")]
    public class AddSlideModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uof;
        private readonly IPhotoServices _photoServices;

        public AddSlideModel(IUnitOfWork uof, UserManager<AppUser> userManager,IPhotoServices photoServices)
        {
            _uof = uof;
            _userManager = userManager;
            _photoServices = photoServices;
        }



        public List<AddSlideView> AddSlideViews { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (user == null) 
            {
                return Redirect("/Identity/Account/Login?returnUrl=%2FAccount%2FMyProfile%2FAdminManager%2FAddSlide");
            }
            if (!userRoles.Contains("Admin"))
            {
                return BadRequest("You are not admin😛");
            }

            AddSlideViews = new List<AddSlideView>();

            List<Store> stores = await _uof.StoreRepository.GetStoresAsync();

            stores.ForEach(store =>
            {
                var addSlideView = new AddSlideView
                {
                    StoreName = store.Name,
                    City = store.City,
                    UserId = store.UserId
                };

                AddSlideViews.Add(addSlideView);
            });

            return Page();
        }


    }
}
