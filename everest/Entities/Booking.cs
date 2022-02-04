using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace everest.Entities
{
    [Table("Bookings")]
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime DateOfBooking { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string ClinicId { get; set; }
        public Clinic Clinic { get; set; }

    }
}