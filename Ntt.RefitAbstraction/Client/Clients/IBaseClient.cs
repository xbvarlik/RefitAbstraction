using Refit;

namespace Ntt.RefitAbstraction.Client.Clients;

public interface IBaseClient;

public interface IBaseCustomizingClient<in TId, in TCreateModel, TViewModel> : IBaseClient 
    where TViewModel : class
{
    [Get("/")]
    Task<List<TViewModel>> GetAllAsync(CancellationToken cancellationToken = default);
    
    [Get("/{id}")]
    Task<TViewModel> GetAsync([AliasAs("id")] TId id, CancellationToken cancellationToken = default);
    
    [Post("/")]
    Task<TViewModel> CreateAsync([Body] TCreateModel model, CancellationToken cancellationToken = default);
    
    [Delete("/{id}")]
    Task DeleteAsync([AliasAs("id")] TId id, CancellationToken cancellationToken = default);
}

public interface IBasePrimaryClient<in TId, in TCreateModel, TViewModel, in TQueryFilterModel, in TUpdateModel> : IBaseClient
    where TCreateModel : class
    where TViewModel : class
    where TQueryFilterModel : class
    where TUpdateModel : class
{
    [Get("/")]
    Task<List<TViewModel>> GetAllAsync([Query] TQueryFilterModel? query, CancellationToken cancellationToken = default);
    
    [Get("/{id}")]
    Task<TViewModel> GetAsync([AliasAs("id")] TId id, CancellationToken cancellationToken = default);
    
    [Post("/")]
    Task<TViewModel> CreateAsync([Body] TCreateModel model, CancellationToken cancellationToken = default);
    
    [Put("/{id}")]
    Task<TViewModel> UpdateAsync([AliasAs("id")] TId id, [Body] TUpdateModel model, CancellationToken cancellationToken = default);
    
    [Delete("/{id}")]
    Task DeleteAsync([AliasAs("id")] TId id, CancellationToken cancellationToken = default);
}