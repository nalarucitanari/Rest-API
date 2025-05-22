using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public class CourseEF : ICourse
    {
        private readonly ApplicationDbContext _context;
        public CourseEF(ApplicationDbContext context)
        {
            _context = context;
        }
        public Course AddCourse(Course course)
        {
            try
            {
                if (course == null)
                {
                    throw new ArgumentNullException(nameof(course), "Course cannot be null");
                }
                 _context.Courses.Add(course);
                _context.SaveChanges();

                var createdCourse = _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .FirstOrDefault(c => c.CourseId == course.CourseId);
                
               

                return createdCourse;
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("An error occurred while adding the course", dbex);
            }
            catch (System.Exception ex)
            {
                throw new Exception("An unecpected error occurred", ex);
            }
        }

        public void DeleteCourse(int courseId)
        {
            try
            {
                var course = _context.Courses.Find(courseId);
                if (course == null)
                {
                    throw new Exception("Course not found");
                }
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("An error occurred while deleting the course", dbex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            var courses = _context.Courses
        .Include(c => c.Category)
        .Include(c => c.Instructor)
        .OrderByDescending(c => c.CourseName)
        .ToList();

    return courses;
        }

        public ViewCourseWithCategory GetCourseById(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            return new ViewCourseWithCategory
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                categoryId = course.categoryId,
                InstructorId = course.InstructorId
            };
        }

        public Course GetCourseByIdCourse(int courseId)
        {
            var course = _context.Courses.Include(c => c.Category).Include(c => c.Instructor).FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            return course;
        }

        public IEnumerable<ViewCourseWithCategory> GetCourses()
        {
            var courses = _context.Courses.OrderByDescending(c => c.CourseId).ToList();
            var viewCourses = courses.Select(c => new ViewCourseWithCategory
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                categoryId = c.categoryId,
                InstructorId = c.InstructorId
            }).ToList();
            return viewCourses;
            
        }

        public IEnumerable<Course> GetCoursesByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Course UpdateCourse(Course course)
        {
            var existingCourse = _context.Courses
                .FirstOrDefault(c => c.CourseId == course.CourseId);
            if (existingCourse == null)
            {
                throw new Exception("Course not found");
            }

            existingCourse.CourseName = course.CourseName;
            existingCourse.CourseDescription = course.CourseDescription;
            existingCourse.Duration = course.Duration;
            existingCourse.categoryId = course.categoryId;
            existingCourse.InstructorId = course.InstructorId;

            _context.SaveChanges();
            return existingCourse;
        }
    }
}