using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace everest.Areas.Identity.Pages.Account.MyProfile.StoreManager
{
    public class EditProductModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public EditProductModel(UserManager<AppUser> userManager, IUnitOfWork uof, IMapper mapper)
        {
            _userManager = userManager;
            _uof = uof;
            _mapper = mapper;
        }

        

        public List<ClassificationDto> Classifications { get; set; }

        public List<ProductPhotoDto> ProductPhotos { get; set; }

        [BindProperty]
        public InputProductModel Input { get; set; }


        public async Task<IActionResult> OnGetAsync(string id)
        {

            if (id == null)
            {
                return BadRequest("Id of product is null😣!");
            }

            var user = await _userManager.GetUserAsync(User);
            var userRoles = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                return Redirect($"/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FMyProfile%2FStoreManager%2FEditProduct?id={id}");
            }

            if (!userRoles.Contains("Store"))
            {
                return NotFound("You are not store Manager!!🙄🙄");
            }

            

            var store = await _uof.StoreRepository.GetStoreAsync(user);

            Classifications = store.Classifications
                                .Select(classification => _mapper.Map<ClassificationDto>(classification))
                                .ToList();

            var product = await _uof.StoreRepository.GetProductByIdAsync(id);

            Input = _mapper.Map<InputProductModel>(product);

            ProductPhotos = product.Photos
                .Where(ph => !ph.IsMain)
                .Select(ph => _mapper.Map<ProductPhotoDto>(ph))
                .ToList();

            return Page();

        }





        public class InputProductModel
        {
            [Display(Name = "اسم المنتج:")]
            public string Name { get; set; }

            [Display(Name = "وصف المنتج:")]
            public string Description { get; set; }

            [Display(Name = "التصنيف:")]
            public string Classification { get; set; }

            [Display(Name = "متوفر")]
            public bool IsAllowed { get; set; }

            [Display(Name = "السعر:")]
            public double Price { get; set; }

            [Display(Name = "العملة")]
            public string Currency { get; set; }

            public string MainPhotoUrl { get; set; }

        }
    }
}
