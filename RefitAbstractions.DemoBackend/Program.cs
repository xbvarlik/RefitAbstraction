using Ntt.Exceptions;
using Ntt.RefitAbstraction.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRefitServer();
builder.Services.AddCustomExceptionHandler();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseRefitServer();

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();