using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace everest.Entities
{
    [Table("Bookings")]
    public class Booking
    {
        public int Id { get; set; }
        public DateTime DateOfBooking { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

    }
}