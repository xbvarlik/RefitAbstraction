using Ntt.RefitAbstraction.Client.Clients;
using RefitAbstractions.DemoClient.Models;

namespace RefitAbstractions.DemoClient.Clients;

public interface IProductClient : IBasePrimaryClient<int, ProductCreateModel, ProductViewModel, ProductQueryFilterModel,
    ProductUpdateModel>;

public interface IProductClientCustomizing : IBaseCustomizingClient<int, ProductCreateModel, ProductViewModel>;