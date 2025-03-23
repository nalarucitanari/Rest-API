using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTAPI.Models
{
    public class ViewCourseWithCategory
    {
        public int CourseId { get; set;}
        public string CourseName { get; set;} = null!;
        public string? CourseDescription { get; set;} 
        public double Duration { get; set;} 
        public int categoryId { get; set; }
        public string categoryName { get; set; } = null!;


    }
}