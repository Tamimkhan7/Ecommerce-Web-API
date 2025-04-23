using Ecommerce_Web_API.DTOs;
using Ecommerce_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce_Web_API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new();

        // GET: /api/categories
        [HttpGet]
        public IActionResult GetCategories(string? searchValue)
        {
            // if (!string.IsNullOrWhiteSpace(searchValue))
            // {
            //     var filtered = categories
            //         .Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)
            //                  || c.Description.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
            //         .ToList();
            //     return Ok(filtered);
            // }
            //categoryReadDto file theke value gula asbe and akta akta kore nibo, and store korabo categories stores a 
            var categoryList = categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
            // converted each value on the tolist value

            return Ok(categoryList);
        }

        // POST: /api/categories
        [HttpPost]
        // CategoryUpdateDto asce DTO  file theke, jeita user define
        public IActionResult PostCategories([FromBody] CategoryUpdateDto categoryData)
        {
            if (string.IsNullOrEmpty(categoryData.Name))
                return BadRequest("Category Name is required and cannot be empty");

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow
            };
            categories.Add(newCategory);

            // newcategory theke jei value gula pabo oi gula ami categoryreaddto te rekhe dilam
            var categoryReadDto = new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt,
            };
            return Created($"/api/categories/{newCategory.CategoryId}", categoryReadDto);
        }

        // DELETE: /api/categories/{categoryId}
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategories(Guid categoryId)
        {
            var found = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (found == null)
                return NotFound("Category with this ID does not exist");

            categories.Remove(found);
            return NoContent();
        }

        // PUT: /api/categories/{categoryId}
        [HttpPut("{categoryId:guid}")]
        public IActionResult PutCategories(Guid categoryId, [FromBody] Category updated)
        {
            var found = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (found == null)
                return NotFound("Category with this ID does not exist");

            if (!string.IsNullOrEmpty(updated.Name))
            {
                if (updated.Name.Length >= 2)
                    found.Name = updated.Name;
                else return BadRequest("Category Name must be at least 2 characters");
            }

            if (!string.IsNullOrWhiteSpace(updated.Description))
                found.Description = updated.Description;

            return NoContent();
        }
    }
}
