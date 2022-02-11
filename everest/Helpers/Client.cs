using everest.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Helpers
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manager { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public bool Valid { get; set; } = true;
        public string LocationUrl { get; set; }
        public int? Whatsapp1 { get; set; }
        public int? Whatsapp2 { get; set; }
        public int? PhoneNumber1 { get; set; }
        public int? PhoneNumber2 { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}
