using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Ntt.RefitAbstraction.Client.Builders;

public class RefitClientCollectionBuilder : IRefitClientCollectionBuilder
{
    public IDictionary<Type, string> ClientEndpoints { get; } = new Dictionary<Type, string>();
    public ClientSettings Settings { get; set; } = null!;
    public ICollection<Type> HttpMessageHandlers { get; } = new List<Type>();
    public IServiceCollection Services { get; set; } = null!;
    
    public IRefitClientCollectionBuilder AddClient(Type client, string endpoint) 
    {
        ClientEndpoints.Add(client, endpoint);
        return this;
    }

    public IRefitClientCollectionBuilder AddMessageHandler<THandlerType>(THandlerType handler) 
        where THandlerType : Type
    {
        HttpMessageHandlers.Add(handler);
        Services.AddTransient(handler);
        return this;
    }

    public void BuildClient()
    {
        foreach (var clientEndpoint in ClientEndpoints)
        {
            var clientBuilder = Services.AddRefitClient(clientEndpoint.Key)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri($"{Settings.BaseUrl}{clientEndpoint.Value}"));

            foreach (var handler in HttpMessageHandlers)
            {
                clientBuilder.AddHttpMessageHandler(sp => (DelegatingHandler)sp.GetRequiredService(handler));
            }
            
        }
    }
}