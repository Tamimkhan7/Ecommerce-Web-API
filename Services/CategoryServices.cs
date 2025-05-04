using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce_Web_API.Controllers;
using Ecommerce_Web_API.Data;
using Ecommerce_Web_API.DTOs;
using Ecommerce_Web_API.Models;
using Ecommerce_Web_API.Models.Interfaces;
using Ecommerce_Web_API.Services.Enums;
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
        public async Task<PaginationResult<CategoryReadDto>> GetAllCategories(QueryParameter queryParameters)
        {
            // for query access IQueryable we can used 
            IQueryable<Category> query = _appDbContext.Categories;
            // this is not more efficiency 
            // if (!string.IsNullOrWhiteSpace(search.ToLower()))
            // {
            //     query = query.Where(c => c.Name.ToLower().Contains(search) || c.Description.ToLower().Contains(search));
            // }
            // we can use Entity framework for efficiency 
            if (!string.IsNullOrWhiteSpace(queryParameters.search))
            {
                var searchValue = $"%{queryParameters.search.Trim()}%";
                query = query.Where(c => EF.Functions.ILike(c.Name, searchValue) || EF.Functions.ILike(c.Description, searchValue));
            }

            if ((string.IsNullOrWhiteSpace(queryParameters.sortOrder)))
                query = query.OrderBy(c => c.Name);
            else
            {
                var formattedsortOrder = queryParameters.sortOrder.Trim().ToLower();
                if (Enum.TryParse<sortOrder>(formattedsortOrder, true, out var parserSortOrder))
                {
                    if (formattedsortOrder == "name_asc")
                        query = query.OrderBy(c => c.Name);
                    else if (formattedsortOrder == "name_desc") query = query.OrderByDescending(c => c.Name);
                    else if (formattedsortOrder == "createdat_asc") query = query.OrderBy(c => c.CreatedAt);
                    else if (formattedsortOrder == "createdat_desc") query = query.OrderByDescending(c => c.CreatedAt);
                }
            }


            var totalCount = await query.CountAsync();

            // pagination, pagenumber = 3, pagesize = 5
            // 20 categories
            // skip((pageNumber-1)*pagesize).Take(pagesize)

            var items = await query.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize).ToListAsync();

            // var categories = await _appDbContext.Categories.ToListAsync();

            var results = _mapper.Map<List<CategoryReadDto>>(items);
            // now result provided to the paginationResult folder a 
            return new PaginationResult<CategoryReadDto>
            {
                Items = results,
                TotalCount = totalCount,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize
            };
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