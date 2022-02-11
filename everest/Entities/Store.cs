using everest.Helpers;
using everest.Models;
using System.Collections.Generic;

namespace everest.Entities
{
    public class Store : Client
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<StoreClassification> Classifications { get; set; }
        public string StorePhotoId { get; set; }
        public StorePhoto StorePhoto { get; set; }

    }
}