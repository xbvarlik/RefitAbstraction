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
    
    public static void AddRefitServer<TRequestHeaders>(this IServiceCollection services)
        where TRequestHeaders : RequestHeaders
    {
        services.AddSingleton<TRequestHeaders>();
    }
    
    public static void UseRefitServer(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestHeaderParserMiddleware<RequestHeaders>>();
    }
    
    public static void UseRefitServer<TMiddleware, TRequestHeaders>(this IApplicationBuilder app)
        where TRequestHeaders : RequestHeaders, new ()
        where TMiddleware : RequestHeaderParserMiddleware<TRequestHeaders>
    {
        app.UseMiddleware<TMiddleware>();
    }
}