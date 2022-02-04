using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.ModelsView
{
    public class UserView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public string RoleName { get; set; }
    }
}
