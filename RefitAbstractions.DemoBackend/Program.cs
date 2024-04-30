using Ntt.Exceptions;
using Ntt.RefitAbstraction.Server;
using RefitAbstractions.DemoBackend.Middleware;
using RefitAbstractions.DemoBackend.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRefitServer<RefitSettings>();
builder.Services.AddCustomExceptionHandler();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseRefitServer<CustomRequestHeaderParserMiddleware, RefitSettings>();

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();