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

            // return Ok(categoryList);
            //  value return from ApiResponse
            // create objct for Apiresponse use new keyword
            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(categoryList, 200, "Category returned successfully"));
        }
        // GET: /api/categories/{categoryId} => Read a category by Id
        [HttpGet("{categoryId:guid}")]
        public IActionResult GetCategoryById(Guid categoryId)
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

            // check kori categoryid name kono kicu exist kore kina

            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (foundCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this ID does not exist" }, 400, "Validation Failed"));

            var categoryReadDto = new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };
            // converted each value on the tolist value

            // return Ok(categoryList);
            //  value return from ApiResponse
            // create objct for Apiresponse use new keyword
            // ami just akta value return korci tai list return kora lagbe na, just akta id value return korle aii hobe
            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, 200, "Category is returned successfully"));
        }

        // POST: /api/categories
        [HttpPost]
        // CategoryUpdateDto asce DTO  file theke, jeita user define
        public IActionResult PostCategories([FromBody] CategoryUpdateDto categoryData)
        {
            // aita akhn ami data validation ar maddome amra korbo  
            // if (string.IsNullOrEmpty(categoryData.Name))
            //     return BadRequest("Category Name is required and cannot be empty");

            // This checks whether the incoming model(data from client) satisfies all validation rules(like[Required], [StringLength], etc.) that you’ve defined on your model class.
            // if (!ModelState.IsValid)
            // {

            // }

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
            return Created($"/api/categories/{newCategory.CategoryId}", ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, 201, "Category Created successfully"));
        }

        // DELETE: /api/categories/{categoryId}
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategories(Guid categoryId)
        {
            var found = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (found == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category is not found with this id" }, 400, "Validation Failed"));

            categories.Remove(found);
            return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category Deleted successfully"));
        }

        // PUT: /api/categories/{categoryId}
        [HttpPut("{categoryId:guid}")]
        public IActionResult PutCategories(Guid categoryId, [FromBody] Category updated)
        {
            var found = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (found == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category is not found with this id" }, 400, "Validation Failed"));

            // if (!string.IsNullOrEmpty(updated.Name))
            // {
            //     if (updated.Name.Length >= 2)
            //         found.Name = updated.Name;
            //     else return BadRequest("Category Name must be at least 2 characters");
            // }
            found.Name = updated.Name;
            found.Description = updated.Description;

            // if (!string.IsNullOrWhiteSpace(updated.Description))
            //     found.Description = updated.Description;

            return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category Updated successfully"));
        }
    }
}
