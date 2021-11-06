using System.ComponentModel.DataAnnotations.Schema;

namespace everest.Entities
{
    [Table("ProductsPhotos")]
    public class ProductPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

    }
}