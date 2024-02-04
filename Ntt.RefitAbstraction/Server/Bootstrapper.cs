using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ntt.RefitAbstraction.Server.Middleware;

namespace Ntt.RefitAbstraction.Server;

public static class Bootstrapper
{
    public static void AddRefitServer(this IServiceCollection services)
    {
        services.AddSingleton<RequestHeaders>();
    }
    
    public static void UseRefitServer(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestHeaderParserMiddleware<RequestHeaders>>();
    }
}