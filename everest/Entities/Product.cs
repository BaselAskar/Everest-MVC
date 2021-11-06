using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace everest.Entities
{
    public class Product
    {
        [StringLength(8)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        public bool IsAllowed { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public ICollection<ProductPhoto> Photos { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

    }
}