using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_Web_API.DTOs;
using Ecommerce_Web_API.Models;

namespace Ecommerce_Web_API.Services
{
    public class CategoryServices
    {
        private static readonly List<Category> categories = new();

        public List<CategoryReadDto> GetAllCategories()
        {
            return categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
        }
    }
}