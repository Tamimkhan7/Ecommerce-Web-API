using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_Web_API.DTOs;
using Ecommerce_Web_API.Models;
using Ecommerce_Web_API.Models.Interfaces;

namespace Ecommerce_Web_API.Services
{
    public class CategoryServices : ICategoryService
    {
        private static readonly List<Category> _categories = new();

        public List<CategoryReadDto> GetAllCategories()
        {
            return _categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
        }

        // access by categoryId
        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {

            var foundCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
                return null;

            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };
        }

        public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
        {
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow
            };
            _categories.Add(newCategory);

            // newcategory theke jei value gula pabo oi gula ami categoryreaddto te rekhe dilam
            return new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt,
            };
        }

        public void DeleteCategory(Guid categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }

        public void UpdateCategory(Guid categoryId, CategoryUpdateDto updated)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category != null)
            {
                category.Name = updated.Name;
                category.Description = updated.Description;
            }
        }

    }
}