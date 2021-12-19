using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace everest.Services
{
    public class GeneratorId
    {

        

        public static string CreateRandomId(int idLength = 6)
        {
            return Guid.NewGuid().ToString().Substring(0, idLength).Replace("-", "").ToUpper();
        }
    }
}
