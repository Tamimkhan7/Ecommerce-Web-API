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




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Category> categories = new List<Category>();

app.MapGet("/", () => "API is working fine");
//  Read = Read a category => GET : /api/categories
app.MapGet("/api/categories", () => Results.Ok(categories));




//  Create = Create a category => POST : /api/categories
app.MapPost("/api/categories", () =>
{
    var newcategory = new Category
    {
        // given random id 
        CategoryId = Guid.Parse("6953d581-1c31-4896-9dc0-3f8bbbf20abb"),
        Name = "Electronics",
        Description = "Devices and gadgets including phones, laptops, and other electronic equipment",
        CreatedAt = DateTime.UtcNow,

    };
    categories.Add(newcategory);
    return Results.Created($"/api/categories/{newcategory.CategoryId}", newcategory);
});

//  Delete = Delete a category => DELETE : /api/categories
app.MapDelete("/api/categories", () =>
{
    var foundCategories = categories.FirstOrDefault(category => category.CategoryId == Guid.Parse("6953d581-1c31-4896-9dc0-3f8bbbf20abb"));
    if (foundCategories == null) { return Results.NotFound("Categories with this id does not exist"); }
    categories.Remove(foundCategories);
    return Results.NoContent();
});


//  update = update a category => update : /api/categories
app.MapPut("/api/categories", () =>
{
    var foundCategories = categories.FirstOrDefault(category => category.CategoryId == Guid.Parse("6953d581-1c31-4896-9dc0-3f8bbbf20abb"));
    if (foundCategories == null) { return Results.NotFound("Categories with this id does not exist"); }
    foundCategories.Name = "Smart Phone";
    foundCategories.Description = "smart phone is a nice category";
    return Results.NoContent();
});

public record Category
{
    // create random id for category, guid provided random id
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