using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.DTOs
{
    public class SlideDto
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public int Order { get; set; }
        public double Speed { get; set; }
        public int StoreId { get; set; }
        public ICollection<SlidePhotoDto> Photos { get; set; }
    }
}
