using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTAPI.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; } = null!;
        public IEnumerable<Course> Courses { get; set; } = new List<Course>(); 
    }
}