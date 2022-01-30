using everest.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace everest.DTOs
{
    public class SlideForm
    {
        [Required(ErrorMessage = "You have to add store")]
        public string UserId { get; set; }


        public int Order { get; set; }


        public double Speed { get; set; }

        public ICollection<SlidePhoto> Photos { get; set; }
    }
}
