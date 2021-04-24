using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Models
{
    public class Pagination
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string SortBy { get; set; }
        public bool IsSortOrderDescending { get; set; }
    }
}
