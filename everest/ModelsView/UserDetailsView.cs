using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.ModelsView
{
    public class UserDetailsView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public string RoleName { get; set; }
        public string DisplayedRoleName { get; set; }
        public string Email { get; set; }
        public ClientView Client { get; set; }
    }
}
