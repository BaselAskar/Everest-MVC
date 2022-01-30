using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class Slide
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int Order { get; set; }
        public double Speed { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public ICollection<SlidePhoto> Photos { get; set; }

    }
}
