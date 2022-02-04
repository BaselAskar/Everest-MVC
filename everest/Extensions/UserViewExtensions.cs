using everest.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Extensions
{
    public static class UserViewExtensions
    {
        public static UserView UpdateRoleName(this UserView user)
        {
            if (user.RoleName == "Store")
            {
                user.RoleName = "مدير متجر";

            }
            else if (user.RoleName == "Clinic")
            {
                user.RoleName = "مدير عيادة";

            }
            else
            {
                user.RoleName = "مستخدم";
            }

            return user;

        }
    }
}
