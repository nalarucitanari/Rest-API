using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public class CourseADO : ICourse
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;
        public CourseADO(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Course AddCourse(Course course)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"INSERT INTO Courses (CourseName, CourseDescription,Duration, categoryId) VALUES (@CourseName, @CourseDescription, @Duration, @categoryId);select SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseDescription", course.CourseDescription);
                    cmd.Parameters.AddWithValue("Duration", course.Duration);
                    cmd.Parameters.AddWithValue("categoryId", course.categoryId);
                    conn.Open();
                    int courseId = Convert.ToInt32(cmd.ExecuteScalar());
                    course.CourseId = courseId;
                    return course;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void DeleteCourse(int courseId)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"DELETE FROM Courses WHERE CourseId = @CourseId";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result == 0)
                    {
                        throw new Exception("Course not found");
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                   cmd.Dispose();
                   conn.Close(); 
                }
            }
        }



        public ViewCourseWithCategory GetCourseById(int courseId)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT CourseId, CourseName, CourseDescription,Duration, categoryId, categoryName
                                FROM  ViewCourseWithCategory
                                WHERE CourseId = @CourseId";    
                SqlCommand cmd = new SqlCommand(strSql,conn);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                ViewCourseWithCategory course = new ViewCourseWithCategory();
                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        course.CourseId = Convert.ToInt32(dr["CourseId"]);
                        course.CourseName = dr["CourseName"].ToString();
                        course.CourseDescription = dr["CourseDescription"].ToString();
                        course.Duration = Convert.ToInt32(dr["Duration"]);
                        course.categoryId = Convert.ToInt32(dr["categoryId"]);
                        course.categoryName = dr["categoryName"].ToString();
                    }
                    return course;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public IEnumerable<ViewCourseWithCategory> GetCourses()
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT * FROM ViewCourseWithCategory";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                List<ViewCourseWithCategory> courses = new List <ViewCourseWithCategory>();
                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while(dr.Read())
                    {
                        ViewCourseWithCategory course = new ViewCourseWithCategory();
                        course.CourseId = Convert.ToInt32(dr["CourseId"]);
                        course.CourseName = dr["CourseName"].ToString();
                        course.CourseDescription = dr["CourseDescription"].ToString();
                        course.Duration = Convert.ToInt32(dr["Duration"]);
                        course.categoryId = Convert.ToInt32(dr["categoryId"]);
                        course.categoryName = dr["categoryName"].ToString();
                        courses.Add(course);
                    }
                    return courses;
                }
                catch(SqlException sqlex)
                {
                    throw new Exception(sqlex.Message);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

        }

        public Course UpdateCourse(Course course)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"UPDATE Courses SET CourseName = @CourseName, CourseDescription = @CourseDescription, Duration = @Duration, categoryId = @categoryId
                WHERE CourseId = @CourseId" ;
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseDescription", course.CourseDescription);
                    cmd.Parameters.AddWithValue("@Duration", course.Duration);
                    cmd.Parameters.AddWithValue("@categoryId", course.categoryId);
                    cmd.Parameters.AddWithValue("@CourseId", course.CourseId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return course;
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close ();
                }
            }
        }
    }
}