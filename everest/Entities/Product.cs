using everest.Interfaces;
using everest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace everest.Entities
{
    [Table("Products")]
    public class Product
    {
        public string Id { get; set; } = GeneratorId.CreateRandomId();
        public string Name { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        public bool IsAllowed { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public ICollection<ProductPhoto> Photos { get; set; }
        public string StoreId { get; set; }
        public Store Store { get; set; }

    }

}