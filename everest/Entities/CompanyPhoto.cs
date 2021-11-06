using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class CompanyPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public int? StoreId { get; set; }
        public Store Store { get; set; }
        public int? ClinicId { get; set; }
        public Clinic Clinic { get; set; }

    }
}
