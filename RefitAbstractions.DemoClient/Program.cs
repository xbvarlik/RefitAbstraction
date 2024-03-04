using Ntt.Exceptions;
using Ntt.RefitAbstraction.Client.Builders;
using Ntt.RefitAbstraction.Client.Handlers;
using RefitAbstractions.DemoClient.Clients;
using RefitAbstractions.DemoClient.Handlers;
using RefitAbstractions.DemoClient.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddCustomExceptionHandler();

// Collection approach
// builder.Services
//     .AddClientConfiguration<ProductClientSettings>(builder.Configuration)
//     .AddClientCollection(typeof(IProductClient), typeof(IProductClientCustomizing))
//     .RegisterHttpMessageHandlers(typeof(HeaderFillingHandler), typeof(ClientExceptionHandler))
//     .BuildClient();

// Fluent approach
builder.Services
    .AddClientConfiguration<ProductClientSettings>(builder.Configuration)
    .RegisterRefitClient<IProductClient>()
    .RegisterRefitClient<IProductClientCustomizing>()
    .RegisterHttpMessageHandler<CustomHeaderFillingHandler>()
    .RegisterHttpMessageHandler<CustomClientExceptionHandler>()
    .BuildClient();

// Fluent approach
builder.Services
    .AddClientConfiguration(builder.Configuration, "ProductClientSettings")
    .RegisterRefitClient<IProductClient>()
    .RegisterRefitClient<IProductClientCustomizing>()
    .RegisterHttpMessageHandler<CustomHeaderFillingHandler>()
    .RegisterHttpMessageHandler<CustomClientExceptionHandler>()
    .BuildClient();

var app = builder.Build();
app.UseCustomExceptionHandler();

app.MapGet("/", () => "Hello World!");

app.MapGet("/products", async (IProductClient client) =>
{
    var products = await client.GetAsync(1);
    return products;
});

app.MapDelete("/products/{id}", async (IProductClientCustomizing client, int id) =>
{
    await client.DeleteAsync(id);
    return "Deleted";
});

app.Run();