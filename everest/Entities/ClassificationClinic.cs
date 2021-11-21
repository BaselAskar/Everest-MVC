using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class ClassificationClinic
    {
        public int Id { get; set; }
        public int ClassificationId { get; set; }
        public Classification Classification { get; set; }
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

    }
}
