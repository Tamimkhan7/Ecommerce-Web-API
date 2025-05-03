using Ecommerce_Web_API.Controllers;
using Ecommerce_Web_API.Data;
using Ecommerce_Web_API.Models.Interfaces;
using Ecommerce_Web_API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// add services to the controller
// builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
// {
//     option.SuppressModelStateInvalidFilter = true;
//     // automatic model validation response 
// });
// Add services

builder.Services.AddScoped<ICategoryService, CategoryServices>();

builder.Services.AddControllers(); // important for MVC
builder.Services.Configure<ApiBehaviorOptions>(option =>
{
    option.InvalidModelStateResponseFactory = context =>
    {
        // context ar modde request and response both are include 
        // var errors = context.ModelState.Where(e => e.Value != null && e.Value.Errors.Count > 0)
        //                .Select(e => new
        //                {
        //                    Field = e.Key,
        //                    Errors = e.Value.Errors != null ? e.Value.Errors.Select(x => x.ErrorMessage).ToArray() : new string[0]
        //                }).ToList();
        // amra jodi kokhon sob gula error message ke ak sathe string ar modde rakhte cai tahole amra ai vabe korte pari
        // var errorString = string.Join("; ", errors.Select(e => $"{e.Field} : {string.Join(", ", e.Errors)}"));

        var errors = context.ModelState.Where(e => e.Value != null && e.Value.Errors.Count > 0)
                              .SelectMany(e => e.Value?.Errors != null ? e.Value.Errors.Select(x => x.ErrorMessage) : new List<string>()).ToList();
        return new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(errors, 400, "Validation failed"));
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // necessary to map attribute-routed controllers

// Root test endpoint
app.MapGet("/", () => "API is working fine");

app.Run();
