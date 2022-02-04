using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Helpers
{
    public class UserParams : PaginationParams
    {
        public string Search { get; set; }
        public string RoleName { get; set; }
        public string OrderBy { get; set; }
    }
}
