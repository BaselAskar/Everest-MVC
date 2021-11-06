using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}
