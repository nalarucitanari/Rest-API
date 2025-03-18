using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public class CategoryADO : ICategory
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;
        public CategoryADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }
        public Category AddCategory(Category category)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"INSERT INTO Categories (categoryName) VALUES (@categoryName); SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@categoryName", category.categoryName);
                    conn.Open();
                    int categoryId = Convert.ToInt32(cmd.ExecuteScalar());
                    category.categoryId = categoryId;
                    return category;
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

        public void DeleteCategory(int categoryId)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"DELETE FROM Categories WHERE categoryId = @categoryId";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                try
                {
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result == 0)
                    {
                        throw new Exception("Category not found");
                    }
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

        public IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT * FROM Categories ORDER BY CategoryName";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Category category = new();
                        category.categoryId = Convert.ToInt32(dr["categoryId"]);
                        category.categoryName = dr["categoryName"].ToString();
                        categories.Add(category);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = new();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT * FROM Categories WHERE categoryId = @categoryId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    dr.Read();
                    category.categoryId = Convert.ToInt32(dr["categoryId"]);
                    category.categoryName = dr["categoryName"].ToString();
                }
                else
                {
                    throw new Exception("Category not found");
                }
            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"UPDATE Categories SET categoryName = @categoryName WHERE categoryId = @categoryId";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                try
                {
                    cmd.Parameters.AddWithValue("@categoryName", category.categoryName);
                    cmd.Parameters.AddWithValue("@categoryId", category.categoryId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result == 0)
                    {
                        throw new Exception("Category not found");
                    }
                    return category;
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
    }
}