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
        // asynchronous use korar jonno amader task use kora lagbe 
        Task<List<CategoryReadDto>> GetAllCategories();
        Task<CategoryReadDto?> GetCategoryById(Guid categoryId);
        Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData);
        Task DeleteCategory(Guid categoryId); // ✅ fixed
        Task UpdateCategory(Guid categoryId, CategoryUpdateDto updated); // ✅ fixed
    }
}
