using System;
using Ecommerce_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Web_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
    }
}
