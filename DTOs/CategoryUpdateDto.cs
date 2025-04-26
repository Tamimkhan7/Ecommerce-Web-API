using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_API.DTOs
{
    public class CategoryUpdateDto
    {
        // any propery don't required, because user doesn't provided any value sometime, that's why we don't use required
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category Name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, MinimumLength = 2, ErrorMessage = "Category Description must be between 2 and 500 characters")]
        public string Description { get; set; } = string.Empty;
    }
}