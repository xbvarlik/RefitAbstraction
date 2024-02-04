using Microsoft.Extensions.DependencyInjection;

namespace Ntt.RefitAbstraction.Client.Builders;

public interface IRefitClientCollectionBuilder
{
    public ICollection<Type> HttpMessageHandlers { get; }
    public IServiceCollection Services { get; }
    public ClientSettings Settings { get; set; }
    public IDictionary<Type, string> ClientEndpoints { get; }
    
    IRefitClientCollectionBuilder AddClient(Type client, string endpoint);
    
    IRefitClientCollectionBuilder AddMessageHandler<THandlerType>(THandlerType handler)
        where THandlerType : Type;
    
    void BuildClient();
}