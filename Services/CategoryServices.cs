using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce_Web_API.Data;
using Ecommerce_Web_API.DTOs;
using Ecommerce_Web_API.Models;
using Ecommerce_Web_API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Web_API.Services
{
    public class CategoryServices : ICategoryService
    {
        // immemory data, don't work with the real memory data
        // private static readonly List<Category> _categories = new();
        // interface variable theke akta mapper niye nicci
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        // AppDbContext jonno dependency injection context add kora lagbe
        public CategoryServices(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        // amra basically categories value gula categoryreadDto te niye jacci and se gula category te store korci

        // Model - DTO
        // DTO- MODEL 
        // Return type Task or use kora lagbe
        public async Task<List<CategoryReadDto>> GetAllCategories()
        {

            var categories = await _appDbContext.Categories.ToListAsync();
            return _mapper.Map<List<CategoryReadDto>>(categories);
        }

        // access by categoryId
        public async Task<CategoryReadDto?> GetCategoryById(Guid categoryId)
        {
            // FirstOrDefaultAsync---where we use it, for more complex data we can use it, for primary data we could be use FindAsync
            var foundCategory = await _appDbContext.Categories.FindAsync(categoryId);
            return (foundCategory == null ? null : _mapper.Map<CategoryReadDto>(foundCategory));
        }

        public async Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData)
        {
            // var newCategory = new Category
            // {
            //     CategoryId =
            //     Name = categoryData.Name,
            //     Description = categoryData.Description,
            //     CreatedAt = DateTime.UtcNow
            // };

            // categoryCreateDto => category model a convert korbo
            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.CreatedAt = DateTime.UtcNow;

            // ready for database table
            await _appDbContext.Categories.AddAsync(newCategory);
            // and save into the database
            await _appDbContext.SaveChangesAsync();
            // newcategory theke jei value gula pabo oi gula ami categoryreaddto te rekhe dilam
            return _mapper.Map<CategoryReadDto>(newCategory);
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            var category = await _appDbContext.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _appDbContext.Categories.Remove(category);
                await _appDbContext.SaveChangesAsync();

            }
        }

        public async Task UpdateCategory(Guid categoryId, CategoryUpdateDto updated)
        {
            var FoundCategory = await _appDbContext.Categories.FindAsync(categoryId);
            if (FoundCategory != null)
            {
                _mapper.Map(updated, FoundCategory);
                _appDbContext.Categories.Update(FoundCategory);

                await _appDbContext.SaveChangesAsync();
            }
        }

    }
}