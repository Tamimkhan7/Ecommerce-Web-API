using AutoMapper;
using Ecommerce_Web_API.DTOs;
using Ecommerce_Web_API.Models;
using Ecommerce_Web_API.Models.Interfaces;
using Ecommerce_Web_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Ecommerce_Web_API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryServices)
        {
            _categoryService = categoryServices;
        }



        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 6)
        {
            //   Console.WriteLine($"PageNumber {PageNumber}, PageSize {PageSize}");
            var categoryList = await _categoryService.GetAllCategories(PageNumber, PageSize);
            return Ok(ApiResponse<PaginationResult<CategoryReadDto>>.SuccessResponse(categoryList, 200, "Category returned successfully"));
        }

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid categoryId) 
        {
            var foundCategory = await _categoryService.GetCategoryById(categoryId);

            if (foundCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category with this ID does not exist" }, 400, "Validation Failed"));

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(foundCategory, 200, "Category is returned successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> PostCategories([FromBody] CategoryCreateDto categoryData)
        {
            var categoryReadDto = await _categoryService.CreateCategory(categoryData);

            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = categoryReadDto.CategoryId },
                ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, 201, "Category Created successfully"));
        }

        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategories(Guid categoryId)
        {
            var found = await _categoryService.GetCategoryById(categoryId);
            if (found == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category is not found with this id" }, 400, "Validation Failed"));

            // _categoryService.DeleteCategory(categoryId);

            return Ok(ApiResponse<object>.SuccessResponse(null, 200, "Category Deleted successfully"));
        }

        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> PutCategories(Guid categoryId, [FromBody] CategoryUpdateDto updated)
        {
            var found = await _categoryService.GetCategoryById(categoryId);
            if (found == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "Category is not found with this id" }, 400, "Validation Failed"));

            // _categoryService.UpdateCategory(categoryId, updated);

            return Ok(ApiResponse<object>.SuccessResponse(null, 200, "Category Updated successfully"));
        }
    }
}
