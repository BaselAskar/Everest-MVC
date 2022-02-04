using everest.Helpers;
using everest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class Clinic : Client
    {
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Classification> Classifications { get; set; }

    }
}
