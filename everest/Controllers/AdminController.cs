using AutoMapper;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uof;
        private readonly ILogger<AdminController> _logger;
        public AdminController(UserManager<AppUser> userManager, IUnitOfWork uof,ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _uof = uof;
            _logger = logger;
        }



        //public async Task<ActionResult> ChangeRole([FromQuery] string userId, string role,string classifications = null)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null) return NotFound("This user is not existed🙄🙄");

        //    var userRoles = await _userManager.GetRolesAsync(user);

        //    await _userManager.RemoveFromRolesAsync(user, userRoles);

        //    switch (role)
        //    {
        //        case "Moderator":

        //            if (userRoles.Count != 0)
        //            {
                        
        //                if (userRoles.Contains("Store")) _uof.StoreRepository.RemoveStore(await _uof.StoreRepository.GetStoreAsync(user));

        //                if (userRoles.Contains("Clinic")) _uof.ClinicRepository.RemoveClinic(await _uof.ClinicRepository.GetClinicAsync(user));

        //                if (!await _uof.Complete()) return BadRequest("Something was wrong please try agian");

        //            }


        //            await _userManager.AddToRoleAsync(user, role);

        //            return Redirect("/Identity/Account/MyProfile/CustomerManager");


        //        case "Customer":

        //            if (!userRoles.Contains("Moderator"))
        //            {
        //                if (userRoles.Contains("Store")) _uof.StoreRepository.RemoveStore(await _uof.StoreRepository.GetStoreAsync(user));

        //                if (userRoles.Contains("Clinic")) _uof.ClinicRepository.RemoveClinic(await _uof.ClinicRepository.GetClinicAsync(user));

        //                if (!await _uof.Complete()) return BadRequest("Something was wrong please try agian");

        //            }


        //            return Redirect("/Identity/Account/MyProfile/CustomerManager");



        //        case "Store":

                    
        //            await _uof.StoreRepository.AddStoreAsync(new Store {UserId = user.Id,User = user });

        //            if (!await _uof.Complete()) return BadRequest("Field to add store");

        //            var store = await _uof.StoreRepository.GetStoreAsync(user);

        //            var classificationsList = classifications.Split(',').ToList();


        //            foreach(var classific in classificationsList)
        //            {

        //                var title = classific.Substring(0, classific.IndexOf('-'));
        //                var name = classific.Substring(classific.IndexOf('-') + 1);

        //                var classification = await _uof.ClassificationRepository.GetClassificationByTitleAndName(title, name);



        //                store.Classifications.Add(classification);

        //                if (!await _uof.Complete()) return BadRequest("Something was wrong please try agian");

        //            }


        //            await _userManager.AddToRoleAsync(user, role);


        //            return Redirect("/Identity/Account/MyProfile/CustomerManager");
                    

        //        case "Clinic":

                    
        //            await _uof.ClinicRepository.AddClinicAsync(new Clinic { UserId = user.Id,User = user });

        //            if (!await _uof.Complete()) return BadRequest("Field to add new clinic");

        //            var clinic = await _uof.ClinicRepository.GetClinicAsync(user);

        //            var classificationsClinicList = classifications.Split(',').ToList();

        //            foreach (var classific in classificationsClinicList)
        //            {
        //                var title = classific.Substring(0, classific.IndexOf('-'));
        //                var name = classific.Substring(classific.IndexOf('-') + 1);

        //                var classification = await _uof.ClassificationRepository.GetClassificationByTitleAndName(title, name);


        //                clinic.Classifications.Add(classification);

        //                if (!await _uof.Complete()) return BadRequest("Something was wrong please try agian");

        //            }


        //            await _userManager.AddToRoleAsync(user, role);

        //            if (!await _uof.Complete()) return BadRequest("Something was wrong please try agian");


        //            return Redirect("/Identity/Account/MyProfile/CustomerManager");



        //        default:return BadRequest("Some thing was wrong please try again");

        //    }
        //}
    }
}
