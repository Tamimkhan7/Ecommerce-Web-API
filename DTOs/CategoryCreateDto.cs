using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_API.DTOs
{
    public class CategoryCreateDto
    {
        // DTO file oi sob value niye kaj kore jei gula change hoy, jei gula input ba user theke ase na oi gula main file ar modde theke jay
        // DTO kaj kore data transfer ar sathe
        // required means holo oita needed, jodi na ase tahole akta errormessage dibe
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category Name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Category Description cann't exceed 500 characters")]

        public string Description { get; set; } = string.Empty;

    }
}