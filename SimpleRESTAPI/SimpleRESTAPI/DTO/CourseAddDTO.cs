using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTAPI.DTO
{
    public class CourseAddDTO
    {
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; }
        public double Duration { get; set; }
        public int categoryId { get; set; }
        public int InstructorId { get; set; }
        

    }
}