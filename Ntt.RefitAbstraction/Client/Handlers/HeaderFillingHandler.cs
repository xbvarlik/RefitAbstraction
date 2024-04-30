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
        request = await PrepareRequestAsync(request, cancellationToken);
        request = FillHeadersFromContext(request);
        return await base.SendAsync(request, cancellationToken);
    }

    protected virtual Task<HttpRequestMessage> PrepareRequestAsync(HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(request);
    }

    protected void AddHeader(string key, string value)
    {
        Headers ??= new Dictionary<string, string>();
        Headers[key] = value;
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

    private HttpRequestMessage FillHeadersFromContext(HttpRequestMessage request)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return request;
        
        foreach (var header in Headers!)
        {
            if (!request.Headers.Contains(header.Key))
            {
                request.Headers.Add(header.Key, header.Value);
            }
            else
            {
                // Decide whether to overwrite or handle duplicates differently
                request.Headers.Remove(header.Key);
                request.Headers.Add(header.Key, header.Value);
            }
        }

        return request;
    }
}