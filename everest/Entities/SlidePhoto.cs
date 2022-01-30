using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class SlidePhoto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public int SlideId { get; set; }
        public Slide Slide { get; set; }
    }
}
