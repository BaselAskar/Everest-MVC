using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace everest.Entities
{
    [Table("Bookins")]
    public class Booking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBooking { get; set; }
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

    }
}