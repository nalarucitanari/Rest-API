using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public class InstructorDal : IInstructor
    {
        private List<Instructor> _instructors = new List<Instructor>();
        public InstructorDal()
        {
            _instructors = new List<Instructor>
            {
                new Instructor {InstructorId = 1, InstructorName = "Nala", InstructorEmail="nala@gmail.com", InstructorAddress="jl tinggal kenangan", InstructorCity="Yogyakarta" , InstructorPhone="028279182"},
                new Instructor {InstructorId = 2, InstructorName = "Elva", InstructorEmail="elva@gmail.com", InstructorAddress="jl sesama", InstructorCity="Yogyakarta" , InstructorPhone="082277222"},
                new Instructor {InstructorId = 3, InstructorName = "Febby", InstructorEmail="febby@gmail.com", InstructorAddress="jl kita bersama", InstructorCity="Bali" , InstructorPhone="08162527"},
                new Instructor {InstructorId = 4, InstructorName = "Amazya", InstructorEmail="amazya@gmail.com", InstructorAddress="jl apa saja", InstructorCity="Tobelo" , InstructorPhone="09162622"},
            };
        }
        public Instructor AddInstructor(Instructor instructor)
        {
            _instructors.Add(instructor);
            return instructor;
        }

        public void DeleteInstructor(int instructorId)
        {
            var instructor = GetInstructorById(instructorId);
            if(instructor != null)
            {
                _instructors.Remove(instructor);
            }
        }

        public Instructor GetInstructorById(int instructorId)
        {
           var instructor = _instructors.FirstOrDefault(x => x.InstructorId == instructorId);
           if(instructor == null)
           {
                throw new Exception("Instruction not found");
           }
           return instructor;
        }

        public IEnumerable<Instructor> GetInstructors()
        {
            return _instructors;
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            var existingInstructor = GetInstructorById(instructor.InstructorId);
            if(existingInstructor != null)
            {
                existingInstructor.InstructorName = instructor.InstructorName;
                existingInstructor.InstructorEmail = instructor.InstructorEmail;
                existingInstructor.InstructorPhone = instructor.InstructorPhone;
                existingInstructor.InstructorAddress = instructor.InstructorAddress;
                existingInstructor.InstructorCity = instructor.InstructorCity;
            }	
            return existingInstructor;
        }
    }
}