using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public interface IInstructor
    {
        IEnumerable<Instructor> GetInstructors();

        Instructor GetInstructorById(int InstructorId);
        Instructor AddInstructor(Instructor instructor);
        Instructor UpdateInstructor(Instructor instructor);
        void DeleteInstructor(int instructorId);
        Instructor GetInstructorByIdCourse(int courseId);
        

    }
}