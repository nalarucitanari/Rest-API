using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.DTO
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; }
        public double Duration { get; set; }
        public CategoryDTO? Category { get; set; }
        public InstructorDTO? Instructor { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}