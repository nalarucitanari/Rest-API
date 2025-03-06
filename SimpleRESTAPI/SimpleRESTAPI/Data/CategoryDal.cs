using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTAPI.Models;

namespace SimpleRESTAPI.Data
{
    public class CategoryDal : ICategory
    {
        private List<Category> _categories = new List<Category>();
        public CategoryDal()
        {
            _categories = new List<Category>
            {
                new Category{categoryId = 1, categoryName = "ASP.NET Core"},
                new Category{categoryId = 2, categoryName = "ASP.NET MVC"},
                new Category{categoryId = 3, categoryName = "ASP.NET Web API"},
                new Category{categoryId = 4 , categoryName = "Blazor"},
                new Category{categoryId = 5, categoryName = "Xamarin"},
                new Category{categoryId = 6, categoryName = "Azure"}
            }; 
        }
        public Category GetCategoryById(int categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.categoryId == categoryId);
            if(category == null)
            {
                throw new Exception("Category not Found");
            }
            return category;

        }
        public IEnumerable<Category> GetCategories()
        {
            return _categories;
        }
        public Category AddCategory(Category category)
        {
            _categories.Add(category);
            return category;
        }

        public void DeleteCategory(int categoryId)
        {
            var category =  GetCategoryById(categoryId);
            if(category != null)
            {
                _categories.Remove(category);
            }
        }

        public Category UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.categoryId);    
            if(existingCategory != null)
            {
                existingCategory.categoryName = category.categoryName;  
            }
            return existingCategory;
        }
    }
}