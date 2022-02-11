using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.ModelsView
{
    public class ClientView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manager { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string LocationUrl { get; set; }
        public int Whatsapp1 { get; set; }
        public int Whatsapp2 { get; set; }
        public int PhoneNumber1 { get; set; }
        public int PhoneNumber2 { get; set; }
        public string Classifications { get; set; }

    }
}
