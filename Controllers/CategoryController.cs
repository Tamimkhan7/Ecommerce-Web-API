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
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                var filtered = categories
                    .Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)
                             || c.Description.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                return Ok(filtered);
            }

            return Ok(categories);
        }

        // POST: /api/categories
        [HttpPost]
        public IActionResult PostCategories([FromBody] Category categoryData)
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
            return Created($"/api/categories/{newCategory.CategoryId}", newCategory);
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
