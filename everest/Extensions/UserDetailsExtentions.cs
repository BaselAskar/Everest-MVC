using everest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Extensions
{
    public static class UserDetailsExtentions
    {
        public static string ConvertToString(this ICollection<StoreClassification> classifications)
        {
            string classificationsString = "";

            var classificationsList = classifications.ToList();

            for (var i = 0;i < classificationsList.Count; i++)
            {
                if (i == 0)
                {
                    classificationsString += $"{classificationsList[i].Title}-{classificationsList[i].Name}";
                }
                else
                {
                    classificationsString += $" {classificationsList[i].Title}-{classificationsList[i].Name}";
                }
            }

            return classificationsString;
        }


        public static string GetRoleName(this IList<string> rolesNames)
        {
            var roleName = rolesNames.Contains("Admin") ? "Admin" :
                           rolesNames.Contains("Moderator") ? "Moderator" :
                           rolesNames.Contains("Store") ? "Store" :
                           rolesNames.Contains("Clinic") ? "Clinic" : "Customer";

            return roleName;
        }







        public static string GetDisplayedRoleName(this string roleName)
        {
            string displayedName = roleName switch
            {
                "Store" => "متجر",
                "Clinic" => "عيادة",
                _ => "مستخدم"
            };

            return displayedName;
        }
    }
}
