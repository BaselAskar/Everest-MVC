using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Helpers
{
    public class PaginationParams
    {
        private const int MAX_PAGE_SIZE = 20;
        public int PageNumber { get; set; }
        private int _pageSize = 10;

        public int PageSize 
        { 
            get => _pageSize; 
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value; 
        }
    }
}
