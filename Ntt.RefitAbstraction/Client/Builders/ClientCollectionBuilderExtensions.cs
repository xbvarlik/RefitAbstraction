using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ntt.RefitAbstraction.Client.Handlers;

namespace Ntt.RefitAbstraction.Client.Builders;

public static class ClientCollectionBuilderExtensions
{
    public static IRefitClientCollectionBuilder AddClientConfiguration<TClientSettings>(
        this IServiceCollection services, 
        IConfiguration configuration)
        where TClientSettings : ClientSettings, new()
    {
        var settings = configuration.GetSection(typeof(TClientSettings).Name).Get<TClientSettings>() ?? 
                       throw new InvalidOperationException("Client settings are not configured.");
        
        var builder = new RefitClientCollectionBuilder
        {
            Services = services,
            Settings = settings
        };

        return builder;
    }
    
    public static IRefitClientCollectionBuilder AddClientConfiguration(
        this IServiceCollection services, 
        IConfiguration configuration,
        string settingsName)
    {
        var settings = configuration.GetSection(settingsName).Get<ClientSettings>() ?? 
                       throw new InvalidOperationException("Client settings are not configured.");
        
        var builder = new RefitClientCollectionBuilder
        {
            Services = services,
            Settings = settings
        };

        return builder;
    }
    
    public static IRefitClientCollectionBuilder AddClientCollection(
        this IRefitClientCollectionBuilder builder, 
        params Type[] clients)
    {
        var endpoints = builder.Settings.IncludedEndpoints;

        if (endpoints is null)
        {
            throw new InvalidOperationException("Endpoints are not configured.");
        }
        
        foreach (var client in clients)
        {
            if (!endpoints.TryGetValue(client.Name, out var endpoint))
            {
                throw new InvalidOperationException($"Endpoint for client {client.Name} is not configured.");
            }
            
            builder.AddClient(client, endpoint);
        }
        
        return builder;
    }
    
    public static IRefitClientCollectionBuilder RegisterRefitClient<TClient>(
        this IRefitClientCollectionBuilder builder)
    {
        var endpoints = builder.Settings.IncludedEndpoints;
        var client = typeof(TClient);
        
        if (endpoints is null)
        {
            throw new InvalidOperationException("Endpoints are not configured.");
        }
        
        if (!endpoints.TryGetValue(client.Name, out var endpoint))
        {
            throw new InvalidOperationException($"Endpoint for client {client.Name} is not configured.");
        }
            
        builder.AddClient(client, endpoint);
        
        return builder;
    }
    
    public static IRefitClientCollectionBuilder RegisterHttpMessageHandlers(
        this IRefitClientCollectionBuilder builder, params Type [] handlers)
    {
        foreach (var handler in handlers)
        {
            builder.AddMessageHandler(handler);
        }
        
        return builder;
    }
    
    public static IRefitClientCollectionBuilder RegisterHttpMessageHandler<THandler>(
        this IRefitClientCollectionBuilder builder)
    where THandler : DelegatingHandler
    {
        builder.AddMessageHandler(typeof(THandler));
        
        return builder;
    }
}