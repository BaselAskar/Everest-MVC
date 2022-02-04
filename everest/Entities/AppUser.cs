using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class AppUser : IdentityUser
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public string UserPhotoId { get; set; }
        public UserPhoto Photo { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}
