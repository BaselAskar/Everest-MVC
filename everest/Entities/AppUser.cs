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
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int? StoreId { get; set; }
        public Store Store { get; set; }
        public int? ClinicId { get; set; }
        public Clinic Clinic { get; set; }

    }
}
