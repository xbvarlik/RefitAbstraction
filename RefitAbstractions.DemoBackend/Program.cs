using Ntt.Exceptions;
using Ntt.Exceptions.ExceptionTypes;
using Ntt.RefitAbstraction.Server;
using RefitAbstractions.DemoBackend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRefitServer();
builder.Services.AddCustomExceptionHandler();

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseRefitServer();

app.MapGet("/", () => "Hello World!");


app.MapGet("/product", () => new List<ProductViewModel>()
{
    new ProductViewModel()
    {
        Id = 1,
        Name = "Product 1"
    },
    new ProductViewModel()
    {
        Id = 2,
        Name = "Product 2"
    }
});

app.MapGet("/product/{id}", (int id) => new ProductViewModel()
{
    Id = id,
    Name = $"Product {id}"
});

app.MapPost("/product", (ProductCreateModel model) => new ProductViewModel()
{
    Id = model.Id,
    Name = model.Name
});

app.MapPut("/product/{id}", (int id, ProductUpdateModel model) => new ProductViewModel()
{
    Id = id,
    Name = model.Name ?? $"Product {id}"
});

app.MapDelete("/product/{id}", (int id) =>
{
    throw new ForbiddenException("You are not allowed to delete products.");
});

app.Run();