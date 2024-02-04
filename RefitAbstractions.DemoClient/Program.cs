using Ntt.RefitAbstraction.Client.Builders;
using Ntt.RefitAbstraction.Client.Handlers;
using RefitAbstractions.DemoClient.Clients;
using RefitAbstractions.DemoClient.Handlers;
using RefitAbstractions.DemoClient.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// builder.Services
//     .AddClientConfiguration<ProductClientSettings>(builder.Configuration)
//     .AddClientCollection(typeof(IProductClient), typeof(IProductClientCustomizing))
//     .RegisterHttpMessageHandlers(typeof(HeaderFillingHandler), typeof(ClientExceptionHandler))
//     .BuildClient();

builder.Services
    .AddClientConfiguration<ProductClientSettings>(builder.Configuration)
    .RegisterRefitClient<IProductClient>()
    .RegisterRefitClient<IProductClientCustomizing>()
    .RegisterHttpMessageHandler<HeaderFillingHandler>()
    .RegisterHttpMessageHandler<ClientExceptionHandler>()
    .BuildClient();
//
// builder.Services.AddDefaultClient<ProductClientSettings>(builder.Configuration, 
//     typeof(IProductClient),
//     typeof(IProductClientCustomizing));

var app = builder.Build();

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