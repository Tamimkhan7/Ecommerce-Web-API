var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
//this ways amra swagger ar documentation add kore pelbo
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}
app.UseHttpsRedirection();

// GET
app.MapGet("/", () =>
{
    return "API is working fine";
});

app.MapGet("/hello", () =>
{
    return "Get Method : Hello";
});

// POST
app.MapPost("/hello", () =>
{
    return "Post Method : Hello";
});

// PUT
app.MapPut("/hello", () =>
{
    return "Put Method : Hello";
});

// DELETE
app.MapDelete("/hello", () =>
{
    return "Delete Method : Hello";
});

app.Run();
