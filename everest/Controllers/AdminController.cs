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



        public async Task<ActionResult> ChangeRole([FromQuery] string userId, string role,string classifications = null)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound("This user is not existed🙄🙄");

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);

            switch (role)
            {
                case "Moderator":

                    if (userRoles.Count != 0)
                    {
                        if (userRoles.Contains("Store")) await _uof.StoreRepository.RemoveStore(user);

                        if (userRoles.Contains("Clinic")) await _uof.ClinicRepository.RemoveClinic(user);

                        if (!await _uof.Complete()) return BadRequest("Something was wrong please try agian");

                    }


                    await _userManager.AddToRoleAsync(user, role);

                    return Redirect("/Identity/Account/MyProfile/CustomerManager");


                case "Customer":

                    if (!userRoles.Contains("Moderator"))
                    {
                        if (userRoles.Contains("Store")) await _uof.StoreRepository.RemoveStore(user);

                        if (userRoles.Contains("Clinic")) await _uof.ClinicRepository.RemoveClinic(user);

                        if (!await _uof.Complete()) return BadRequest("Something was wrong please try agian");

                    }


                    return Redirect("/Identity/Account/MyProfile/CustomerManager");



                case "Store":


                    await _uof.StoreRepository.AddStore(user);       

                    var classificationsList = classifications.Split(',').ToList();


                    foreach(var classific in classificationsList)
                    {
                        var classificationDto = new ClassificationDto
                        {
                            Title = classific.Substring(0, classific.IndexOf('-')),
                            Name = classific.Substring(classific.IndexOf('-') + 1)
                        };


                        
                        await _uof.StoreRepository.AddClassificationToStore(user, classificationDto);
                    }

                    if (!await _uof.Complete()) return BadRequest("Something was wrong please try agian");

                    await _userManager.AddToRoleAsync(user, role);


                    return Redirect("/Identity/Account/MyProfile/CustomerManager");
                    

                case "Clinic":


                    await _uof.ClinicRepository.AddClinic(user);



                    var classificationsClinicList = classifications.Split(',').ToList();

                    foreach (var classific in classificationsClinicList)
                    {
                        var classificationDto = new ClassificationDto
                        {
                            Title = classific.Substring(0, classific.IndexOf('-')),
                            Name = classific.Substring(classific.IndexOf('-') + 1)
                        };

                        await _uof.ClinicRepository.AddClassificationToClinic(user, classificationDto);
                    }

                    if (!await _uof.Complete()) return BadRequest("Something was wrong please try agian");

                    await _userManager.AddToRoleAsync(user, role);

                    return Redirect("/Identity/Account/MyProfile/CustomerManager");



                default:return BadRequest("Some thing was wrong please try again");

            }
        }
    }
}
