using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding course", ex);
            }
        }

        public void DeleteCourse(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            try
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting course", ex);
            }
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
                categoryId = course.categoryId
            };
        }

        public IEnumerable<ViewCourseWithCategory> GetCourses()
        {
            var courses = _context.Courses.OrderByDescending(c => c.CourseId).ToList();
            var viewCourses = courses.Select(c => new ViewCourseWithCategory
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                categoryId = c.categoryId
            }).ToList();
            return viewCourses;
            
        }

        public Course UpdateCourse(Course course)
        {
            var existingCourse = _context.Courses.FirstOrDefault(c => c.CourseId == course.CourseId);
            if (existingCourse == null)
            {
                throw new Exception("Course not found");
            }
            try
            {
                existingCourse.CourseName = course.CourseName;
                existingCourse.categoryId = course.categoryId;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.Duration = course.Duration;
                _context.Courses.Update(existingCourse);
                _context.SaveChanges();
                return existingCourse;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating course", ex);
            }
        }
    }
}