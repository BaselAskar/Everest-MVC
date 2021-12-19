using System;
using System.Collections.Generic;
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
    public class ProductsManagerModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uof;
        public ProductsManagerModel(IUnitOfWork uof,UserManager<AppUser> userManager)
        {
            _uof = uof;
            _userManager = userManager;
        }



        public List<ProductDto> Products { get; set; }


        public async Task<IActionResult> OnGetAsync(string result = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FMyProfile%2FStoreManager%2ProductsManager");
            }

            if (!await _userManager.IsInRoleAsync(user, "Store"))
            {
                return NotFound("Sorry you are not store manager🙄🙄🙄!!");
            }


            var store = await _uof.StoreRepository.GetStoreAsync(user);

            Products = await _uof.StoreRepository.GetProductsAsync(store);
            

            return Page();

        }

    }
}
