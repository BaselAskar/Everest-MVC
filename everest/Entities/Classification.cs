using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class Classification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public ICollection<Store> Stores { get; set; }
        public ICollection<Clinic> Clinics { get; set; }

    }
}
