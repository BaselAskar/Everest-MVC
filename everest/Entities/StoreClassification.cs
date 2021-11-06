using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Entities
{
    public class StoreClassification
    {
        public int Id { get; set; }


        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int ClassificationId { get; set; }
        public Classification Classification { get; set; }

    }
}
