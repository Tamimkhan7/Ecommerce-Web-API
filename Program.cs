// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();
// //this ways amra swagger ar documentation add kore pelbo
// builder.Services.AddSwaggerGen();

// var app = builder.Build();


// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
// app.UseHttpsRedirection();

// // GET
// //ak line oh kora jay aita
// app.MapGet("/", () => "API is working fine");

// app.MapGet("/hello", () =>
// {
//     // this is an object
//     // var response = new
//     // {
//     //     message = "This is a json object",
//     //     success = true
//     // };
//     // return Results.Ok(response);//200
//     // now we provided the value of html data type
//     // first a amar html a format a value dite hobe then amader type dite hobe
//     return Results.Content("<h1> Hello world </h1>", "text/html");//200
// });

// // POST
// app.MapPost("/hello", () =>
// {
//     return Results.Created();//201
// });

// // PUT
// app.MapPut("/hello", () =>
// {
//     return Results.NoContent();//204
// });

// // DELETE
// app.MapDelete("/hello", () =>
// {
//     return Results.NoContent();//204
// });

// app.Run();



// public record Product(string Name, decimal Price);

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// var Products = new List<Product>
// {
//     new Product("Samsung", 12053),
//     new Product("iPhone", 14526)
// };

// app.MapGet("/", () => "API is working fine");
// app.MapGet("/products", () => Results.Ok(Products));

// app.Run();



using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services
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

// In-memory data storage
List<Category> categories = new();

// ✅ Root route
app.MapGet("/", () => "API is working fine");

// ✅ GET all categories or search by name/description
app.MapGet("/api/categories", (string? searchValue) =>
{
    if (!string.IsNullOrWhiteSpace(searchValue))
    {
        var filtered = categories
            .Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)
                     || c.Description.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
            .ToList();
        return Results.Ok(filtered);
    }

    return Results.Ok(categories);
});

// ✅ CREATE a new category
app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{
    var newCategory = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name = categoryData.Name,
        Description = categoryData.Description,
        CreatedAt = DateTime.UtcNow
    };
    categories.Add(newCategory);
    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory);
});

// ✅ DELETE a category
app.MapDelete("/api/categories/{categoryId}", (Guid categoryId) =>
{
    var found = categories.FirstOrDefault(c => c.CategoryId == categoryId);
    if (found == null)
        return Results.NotFound("Category with this ID does not exist");

    categories.Remove(found);
    return Results.NoContent();
});

// ✅ UPDATE a category
app.MapPut("/api/categories/{categoryId}", (Guid categoryId, [FromBody] Category updated) =>
{
    var found = categories.FirstOrDefault(c => c.CategoryId == categoryId);
    if (found == null)
        return Results.NotFound("Category with this ID does not exist");

    found.Name = updated.Name;
    found.Description = updated.Description;
    return Results.NoContent();
});

app.Run();

// ✅ Model
public record Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}

//CRUD
//  Create = Create a category => POST : /api/categories
//  Read = Read a category => GET : /api/categories
//  Update = Update a category => PUT : /api/categories
//  Delete = Delete a category => DELETE : /api/categories



// Model binding, validation and MVC Architecture
