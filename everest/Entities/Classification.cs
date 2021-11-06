using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class Classification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public ICollection<StoreClassification> Stores { get; set; }
        public ICollection<ClinicClassification> Clinics { get; set; }

    }
}
