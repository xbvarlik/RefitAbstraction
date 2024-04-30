using Refit;

namespace Ntt.RefitAbstraction.Client.Clients;

public interface IBaseClient;

public interface IGetRequest<in TId, in TQueryFilterModel, TViewModel> : IBaseClient
    where TViewModel : class
    where TQueryFilterModel : class
{
    [Get("/")]
    Task<List<TViewModel>> GetAllAsync([Query] TQueryFilterModel? query = null, CancellationToken cancellationToken = default);
    
    [Get("/{id}")]
    Task<TViewModel> GetAsync([AliasAs("id")] TId id, CancellationToken cancellationToken = default);
}

public interface IPostRequest<in TCreateModel, TViewModel> : IBaseClient
    where TCreateModel : class
    where TViewModel : class
{
    [Post("/")]
    Task<TViewModel> CreateAsync([Body] TCreateModel model, CancellationToken cancellationToken = default);
}

public interface IDeleteRequest<in TId> : IBaseClient
{
    [Delete("/{id}")]
    Task DeleteAsync([AliasAs("id")] TId id, CancellationToken cancellationToken = default);
}

public interface IPutRequest<in TId, in TUpdateModel, TViewModel> : IBaseClient
    where TUpdateModel : class
    where TViewModel : class
{
    [Put("/{id}")]
    Task<TViewModel> UpdateAsync([AliasAs("id")] TId id, [Body] TUpdateModel model, CancellationToken cancellationToken = default);
}

public interface IBaseCustomizingClient<in TId, in TCreateModel, in TQueryFilterModel, TViewModel> :
    IGetRequest<TId, TQueryFilterModel, TViewModel>, IPostRequest<TCreateModel, TViewModel>, IDeleteRequest<TId>
    where TCreateModel : class
    where TViewModel : class
    where TQueryFilterModel : class;
    
public interface IBasePrimaryClient<in TId, in TCreateModel, TViewModel, in TQueryFilterModel, in TUpdateModel> : 
    IGetRequest<TId, TQueryFilterModel, TViewModel>, 
    IPostRequest<TCreateModel, TViewModel>, 
    IDeleteRequest<TId>, 
    IPutRequest<TId, TUpdateModel, TViewModel>
    where TCreateModel : class
    where TViewModel : class
    where TUpdateModel : class
    where TQueryFilterModel : class;