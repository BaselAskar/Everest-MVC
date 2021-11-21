using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using everest.Entities;
using everest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static everest.Helpers.Catigories;

namespace everest.Areas.Identity.Pages.Account.MyProfile.StoreManager
{
    public class StoreInfoModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        public StoreInfoModel(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork uof)
        {
            _userManager = userManager;
            _mapper = mapper;
            _uof = uof;
        }


        [BindProperty]
        public InputModel Input { get; set; }



        public List<string> Cities = new List<string> { "عفرين", "أعزاز", "إدلب", "الباب", "صوران", "جندريس" };

        private async Task LoadUser(AppUser user)
        {
            var store = await _uof.StoreRepository.GetStoreAsync(user);

            var userInput = _mapper.Map<InputModel>(store);

            var photo = await _uof.StoreRepository.GetStorePhotoAsync(store);

            userInput.StorePhotoUrl = photo != null ? photo.Url : null;

            Input = userInput;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FMyProfile%2FStoreManager%2FStoreInfo");
            }

            if (!await _userManager.IsInRoleAsync(user, "Store"))
            {
                return NotFound("You are not store manager!!🙄🙄");
            }

            

           await LoadUser(user);

            return Page();
        }


        public class InputModel
        {
            [Display(Name = ":إسم المحل")]
            public string Name { get; set; }

            [Display(Name = ":وصف المحل")]
            public string Description { get; set; }

            [Display(Name = ":مدير المحل")]
            public string Manager { get; set; }

            [Display(Name = ":العنوان")]
            public string Adress { get; set; }

            [Display(Name = ":المدينة")]
            public string City { get; set; }

            [Display(Name = ":الموقع")]
            public string LocationUrl { get; set; }

            [Display(Name = ": (1) واتساب")]
            public string Whatsapp1 { get; set; }

            [Display(Name = ": (2) واتساب")]
            public string Whatsapp2 { get; set; }

            [Display(Name = ": (1) رقم الهاتف")]
            public string PhoneNumber1 { get; set; }

            [Display(Name = ": (2) رقم الهاتف")]
            public string PhoneNumber2 { get; set; }

            [Display(Name = "إضافة أو تعديل الصورة")]
            public string StorePhotoUrl { get; set; }


        }
    }
}
