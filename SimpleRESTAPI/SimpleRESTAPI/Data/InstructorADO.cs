using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public class InstructorADO : IInstructor
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;
        public InstructorADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }
        public Instructor AddInstructor(Instructor instructor)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"INSERT INTO Instructors (InstructorName, InstructorEmail, InstructorPhone, InstructorAddress, InstructorCity) VALUES (@InstructorName, @InstructorEmail, @InstructorPhone, @InstructorAddress, @InstructorCity); select SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@InstructorName", instructor.InstructorName);
                    cmd.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmail);
                    cmd.Parameters.AddWithValue("@InstructorPhone", instructor.InstructorPhone);
                    cmd.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddress);
                    cmd.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCity);
                    conn.Open();
                    int instructorId = Convert.ToInt32(cmd.ExecuteScalar());
                    instructor.InstructorId = instructorId;
                    return instructor;
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

        public void DeleteInstructor(int instructorId)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"DELETE FROM Instructors WHERE InstructorId = @InstructorId" ;
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@InstructorId", instructorId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result == 0)
                    {
                        throw new Exception("Instructor not found");
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

        public Instructor GetInstructorById(int InstructorId)
        {
            Instructor instructor = new();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT * FROM Instructors WHERE InstructorId = @InstructorId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@InstructorId", InstructorId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    instructor.InstructorId = Convert.ToInt32(dr["InstructorId"]);
                    instructor.InstructorName = dr["InstructorName"].ToString();
                        instructor.InstructorEmail = dr["InstructorEmail"].ToString();
                        instructor.InstructorPhone = dr["InstructorPhone"].ToString();
                        instructor.InstructorAddress = dr["InstructorAddress"].ToString();
                        instructor.InstructorCity = dr["InstructorCity"].ToString();
                        return instructor;
                }
                else
                {
                    throw new Exception("Category not found");
                }
            }
            return instructor;
        }

        public IEnumerable<Instructor> GetInstructors()
        {
            List<Instructor> instructors = new List<Instructor>();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT * FROM Instructors ORDER BY InstructorName";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Instructor instructor = new();
                        instructor.InstructorId = Convert.ToInt32(dr["InstructorId"]);
                        instructor.InstructorName = dr["InstructorName"].ToString();
                        instructor.InstructorEmail = dr["InstructorEmail"].ToString();
                        instructor.InstructorPhone = dr["InstructorPhone"].ToString();
                        instructor.InstructorAddress = dr["InstructorAddress"].ToString();
                        instructor.InstructorCity = dr["InstructorCity"].ToString();
                        instructors.Add(instructor);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return instructors;
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"UPDATE Instructors SET InstructorName = @InstructorName, InstructorEmail = @InstructorEmail, InstructorPhone = @InstructorPhone, InstructorAddress = @InstructorAddress, InstructorCity = @InstructorCity WHERE InstructorId = @InstructorId" ;
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@InstructorName", instructor.InstructorName);
                    cmd.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmail);
                    cmd.Parameters.AddWithValue("@InstructorPhone", instructor.InstructorPhone);
                    cmd.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddress);
                    cmd.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCity);
                    cmd.Parameters.AddWithValue("@InstructorId",instructor.InstructorId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result == 0)
                    {
                        throw new Exception("Instructor not found");
                    }
                    return instructor;
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
    }
}