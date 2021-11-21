using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace everest.Services
{
    public class GeneratorId
    {
        public static string CreateRandomId(int idLength)
        {
            const string AllowedChars = "0123456789abcdefghijklmnopqrstuvwxyz";

            var stringBuilder = new StringBuilder();
            var random = new Random();

            for(var i = 0; i < idLength; i++)
            {
                var n = random.Next(0, AllowedChars.Length);
                stringBuilder.Append(AllowedChars[n]);
            }

            return stringBuilder.ToString();
        }
    }
}
