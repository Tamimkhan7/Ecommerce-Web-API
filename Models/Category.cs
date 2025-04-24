using System;

namespace Ecommerce_Web_API.Models
{
    public class Category
    {
        // DTO file oi sob value niye kaj kore jei gula change hoy, jei gula input ba user theke ase na oi gula main file ar modde theke jay
        // model basically kaj kore batabase ar sathe 
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
