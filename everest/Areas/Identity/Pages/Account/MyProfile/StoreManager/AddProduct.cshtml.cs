using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace everest.Areas.Identity.Pages.Account.MyProfile.StoreManager
{
    public class AddProductModel : PageModel
    {
        private readonly IUnitOfWork _uof;
        private readonly UserManager<AppUser> _userManager;
        public AddProductModel(IUnitOfWork uof,UserManager<AppUser> userManager)
        {
            _uof = uof;
            _userManager = userManager;
        }

        public List<ClassificationDto> Classifications { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "إسم المنتج: ")]
            public string Name { get; set; }

            [Display(Name= "وصف المنتج: ")]
            public string Description { get; set; }

            [Display(Name="التصنيف: ")]
            public string Classification { get; set; }

            [Display(Name="متوفر")]
            public bool IsAllowed { get; set; }

            [Display(Name="السعر: ")]
            public double Price { get; set; }

            [Display(Name="العملة")]
            public string Currency { get; set; }

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userRoles = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                return Redirect("/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FMyProfile%2FStoreManager%2FAddProduct");
            }

            if (!userRoles.Contains("Store"))
            {
                return NotFound("You are not store Manager!!🙄🙄");
            }

            var store = await _uof.StoreRepository.GetStoreAsync(user);

            Classifications =  await _uof.StoreRepository.GetClassificationStoreAsync(store);

            return Page();
        }
    }
}
