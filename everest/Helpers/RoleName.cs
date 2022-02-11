using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Helpers
{
    public class RoleName
    {
        public RoleName(string name, string displayedName)
        {
            Name = name;
            DisplayedName = displayedName;
        }

        public string Name { get; set; }
        public string DisplayedName { get; set; }

    }
}
