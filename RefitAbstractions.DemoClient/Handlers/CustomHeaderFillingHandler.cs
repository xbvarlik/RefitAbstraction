using Ntt.RefitAbstraction.Client.Handlers;

namespace RefitAbstractions.DemoClient.Handlers;

public class CustomHeaderFillingHandler(IHttpContextAccessor httpContextAccessor) : HeaderFillingHandler(httpContextAccessor)
{
    protected override Task<HttpRequestMessage> PrepareRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        AddHeader("CustomHeader", "CustomHeaderValue");
        return base.PrepareRequestAsync(request, cancellationToken);
    }
}