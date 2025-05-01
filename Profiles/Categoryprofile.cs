using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce_Web_API.DTOs;
using Ecommerce_Web_API.Models;

namespace Ecommerce_Web_API.Profiles
{
    public class Categoryprofile : Profile
    {
        public Categoryprofile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
