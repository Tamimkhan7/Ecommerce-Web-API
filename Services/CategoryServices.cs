using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce_Web_API.DTOs;
using Ecommerce_Web_API.Models;
using Ecommerce_Web_API.Models.Interfaces;

namespace Ecommerce_Web_API.Services
{
    public class CategoryServices : ICategoryService
    {
        private static readonly List<Category> _categories = new();
        // interface variable theke akta mapper niye nicci
        private readonly IMapper _mapper;
        public CategoryServices(IMapper mapper)
        {
            _mapper = mapper;
        }
        // amra basically categories value gula categoryreadDto te niye jacci and se gula category te store korci

        // Model - DTO
        // DTO- MODEL 
        public List<CategoryReadDto> GetAllCategories()
        {
            return _mapper.Map<List<CategoryReadDto>>(_categories);
        }

        // access by categoryId
        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            return (foundCategory == null ? null : _mapper.Map<CategoryReadDto>(foundCategory));
        }

        public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
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

            _categories.Add(newCategory);

            // newcategory theke jei value gula pabo oi gula ami categoryreaddto te rekhe dilam
            return _mapper.Map<CategoryReadDto>(newCategory);
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
            var FoundCategory = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (FoundCategory != null)
            {
                _mapper.Map(updated, FoundCategory);
            }
        }

    }
}