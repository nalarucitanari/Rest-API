using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTAPI.DTO
{
    public class CategoryDTO
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; } = null!;
    }
}