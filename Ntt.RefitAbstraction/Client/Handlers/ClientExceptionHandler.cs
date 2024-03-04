using Microsoft.AspNetCore.Http;

namespace Ntt.RefitAbstraction.Client.Handlers;

public class ClientExceptionHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        
        if (response.IsSuccessStatusCode)
            return response;
        
        return await HandleErrorAsync(response, cancellationToken);
    }

    protected virtual async Task<HttpResponseMessage> HandleErrorAsync(HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return response;
        
        httpContext.Response.StatusCode = (int)response.StatusCode;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsync(content, cancellationToken);

        return response;
    }
}