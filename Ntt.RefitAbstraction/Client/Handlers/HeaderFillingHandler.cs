using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Ntt.RefitAbstraction.Client.Handlers;

public class HeaderFillingHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    private ICollection<Dictionary<string, string>> Headers { get; set; } = [];
    
    [SuppressMessage("ReSharper", "InvertIf")]
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (Headers is {Count: > 0})
            FillHeadersFromContext(request);
        
        return await base.SendAsync(request, cancellationToken);
    }
    
    protected void AddHeader(string key, string value)
    {
        Headers.Add(new Dictionary<string, string>
        {
            { key, value }
        });
    }

    protected void AddHeaderFromContext(string contextKey)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;
        
        if (httpContext.Request.Headers.TryGetValue(contextKey, out var header))
            Headers.Add(new Dictionary<string, string>
            {
                { contextKey, header.ToString() }
            });
    }

    protected void AddHeaderFromContext(string contextKey, string requestKey)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;
        
        if (httpContext.Request.Headers.TryGetValue(contextKey, out var value))
            Headers.Add(new Dictionary<string, string>
            {
                { requestKey, value.ToString() }
            });
    }

    private void FillHeadersFromContext(HttpRequestMessage request)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;
        
        foreach (var header in Headers)
        {
            var key = header.Keys.First();
            var value = header.Values.First();

            request.Headers.Add(key, httpContext.Request.Headers.TryGetValue(key, out var requestHeader) ? requestHeader.ToString() : value);
        }
    }
}