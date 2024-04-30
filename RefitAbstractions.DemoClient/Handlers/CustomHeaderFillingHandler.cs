using Ntt.RefitAbstraction.Client.Handlers;

namespace RefitAbstractions.DemoClient.Handlers;

public class CustomHeaderFillingHandler(IHttpContextAccessor httpContextAccessor) : HeaderFillingHandler(httpContextAccessor)
{
    protected override Task<HttpRequestMessage> PrepareRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        AddHeader("CustomHeader", "CustomHeaderValue");
        AddHeader("UserId", "1FD32992-10B7-4AEC-8CC8-38F4CFF0179E");
        return base.PrepareRequestAsync(request, cancellationToken);
    }
}