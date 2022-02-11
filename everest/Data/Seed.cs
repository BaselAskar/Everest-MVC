using everest.DTOs;
using everest.Entities;
using everest.Helpers;
using everest.Interfaces;
using everest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace everest.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager
                ,IConfiguration config, IUnitOfWork uof)
        {
            if (await userManager.Users.AnyAsync()) return;



            //Seeding Admin Data
            var admin = new AppUser { UserName = config["AdminData:UserName"], Email = config["AdminData:Email"] };

            await userManager.CreateAsync(admin, config["AdminData:Password"]);

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
                new AppRole{Name = "Store"},
                new AppRole{Name = "Clinic"},
                new AppRole{Name = "Customer"}
            };

            foreach(var role in roles)
            {
                await roleManager.CreateAsync(role);
            }


            await userManager.AddToRolesAsync(admin, new List<string> { "Admin", "Customer" });



            //Seeding users Data
            var usersData = await File.ReadAllTextAsync("Data/UsersData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(usersData);

            foreach(var user in users)
            {
                await userManager.CreateAsync(user, "password");
                await userManager.AddToRoleAsync(user, "Customer");
            }


            //Seeding Classifications



            foreach(var catigory in Catigories.StoreCatigoriesList)
            {
                foreach(var section in catigory.Sections)
                {
                    var classificationDto = new ClassificationDto { Title = catigory.CatigoryName, Name = section };
                    uof.StoreClassificationRepository.AddStoreClassification(classificationDto);
                }
            }

            
            
            if (!await uof.Complete()) throw new Exception("something was wrong");

        }
    }
}
