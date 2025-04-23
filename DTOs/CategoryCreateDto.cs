using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_API.DTOs
{
    public class CategoryCreateDto
    {
        // DTO file oi sob value niye kaj kore jei gula change hoy, jei gula input ba user theke ase na oi gula main file ar modde theke jay
        // DTO kaj kore data transfer ar sathe
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}