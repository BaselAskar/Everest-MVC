using everest.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace everest.Entities
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string PublicId { get; set; } = GeneratorId.CreateRandomId(6);
        public string Name { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        public bool IsAllowed { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public ICollection<ProductPhoto> Photos { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

    }
}