using System.Diagnostics.CodeAnalysis;
using Ntt.RefitAbstraction.Client.Handlers;

namespace RefitAbstractions.DemoClient.Handlers;

public class CustomHeaderFillingHandler(IHttpContextAccessor httpContextAccessor) : HeaderFillingHandler(httpContextAccessor)
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        AddCustomHeaders();
        return base.SendAsync(request, cancellationToken);
    }

    private void AddCustomHeaders()
    {
        AddHeader("CustomHeader", "CustomHeaderValue");
        // AddHeaderFromContext("Authorization");
        // AddHeaderFromContext("X-LanguageCode", "LanguageCode");
    }
}