using Microsoft.Extensions.DependencyInjection;
using Ntt.RefitAbstraction.Client.Handlers;
using Refit;

namespace Ntt.RefitAbstraction.Client.Extensions;

public static class RefitClientExtensions
{
    public static void AddCustomRefitClient<TInterface>(this IServiceCollection services, string baseUrl)
        where TInterface : class
    {
        services.AddRefitClient<TInterface>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl))
            .AddHttpMessageHandler<HeaderFillingHandler>()
            .AddHttpMessageHandler<ClientExceptionHandler>();
    }
    
    public static void AddHeadlessClient<TInterface>(this IServiceCollection services, string baseUrl)
        where TInterface : class
    {
        services.AddRefitClient<TInterface>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl))
            .AddHttpMessageHandler<ClientExceptionHandler>();
    }
}
