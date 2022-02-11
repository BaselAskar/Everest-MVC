using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class ClinicClassification
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public ICollection<Clinic> Clinics { get; set; }
    }
}
