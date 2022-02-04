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
        public string LocationUrl { get; set; }
        public string Whatsapp1 { get; set; }
        public string Whatsapp2 { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}
