using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.DTOs
{
    public class ProductDto
    {
        public string PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        public double price { get; set; }
        public string Currency { get; set; }
        public bool IsAllowed { get; set; }

    }
}
