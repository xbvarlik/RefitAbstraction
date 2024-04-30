using Ntt.RefitAbstraction.Server.Middleware;
using RefitAbstractions.DemoBackend.Settings;

namespace RefitAbstractions.DemoBackend.Middleware;

public class CustomRequestHeaderParserMiddleware(RequestDelegate next) : RequestHeaderParserMiddleware<RefitSettings>(next)
{
    protected override Task<RefitSettings> PrepareRequestHeaders(HttpContext context, RefitSettings requestHeaders)
    {
        requestHeaders.UserId = GetGuidFromHeader(context, "UserId");
        
        return base.PrepareRequestHeaders(context, requestHeaders);
    }
}