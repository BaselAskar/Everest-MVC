using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.DTOs
{
    public class ProductPhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

    }
}
