using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public class CategoryEF : ICategory
    {
        private readonly ApplicationDbContext _context;
        public CategoryEF(ApplicationDbContext context)
        {
            _context = context;
        }
        public Category AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding category", ex);
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var category = GetCategoryById(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting category", ex);
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _context.Categories.OrderByDescending(c => c.categoryName).ToList();
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.categoryId == categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }   
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.categoryId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                existingCategory.categoryName = category.categoryName;
                _context.SaveChanges();
                return existingCategory;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating category", ex);
            }
        }
    }
}