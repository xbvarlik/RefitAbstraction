using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Ntt.RefitAbstraction.Client.Handlers;

[SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed", Justification = "Appending headers to the dictionary does not require return value to be used.")]
public class HeaderFillingHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    private Dictionary<string, string>? Headers { get; set; } = [];
    
    [SuppressMessage("ReSharper", "InvertIf")]
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await base.SendAsync(request, cancellationToken);
    }
    
    protected void AddHeader(string key, string value)
    {
        Headers?.Append(new KeyValuePair<string, string>(key, value));
    }

    protected void AddHeaderFromContext(string contextKey)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;
        
        if (httpContext.Request.Headers.TryGetValue(contextKey, out var header))
            Headers?.Append(new KeyValuePair<string, string>(contextKey, header.ToString()));
    }

    protected void AddHeaderFromContext(string contextKey, string requestKey)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;

        if (httpContext.Request.Headers.TryGetValue(contextKey, out var value))
            Headers?.Append(new KeyValuePair<string, string>(contextKey, value.ToString()));
    }

    private void FillHeadersFromContext(HttpRequestMessage request)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;
        
        foreach (var header in Headers!)
        {
            request.Headers.Add(header.Key, httpContext.Request.Headers.TryGetValue(header.Key, out var requestHeader) ? requestHeader.ToString() : header.Value);
        }
    }
}