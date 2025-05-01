using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_Web_API.DTOs;

namespace Ecommerce_Web_API.Models.Interfaces
{
    // interface holo akta abstrucated, sob common function gula ai khane rekhe call kore kaj kore nite pari
    public interface ICategoryService
    {
        List<CategoryReadDto> GetAllCategories();
        CategoryReadDto? GetCategoryById(Guid categoryId);
        CategoryReadDto CreateCategory(CategoryCreateDto categoryData);
        void DeleteCategory(Guid categoryId);
        void UpdateCategory(Guid categoryId, CategoryUpdateDto updated);
    }
}