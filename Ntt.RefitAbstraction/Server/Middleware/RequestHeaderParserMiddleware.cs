using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Ntt.RefitAbstraction.Server.Middleware;

public class RequestHeaderParserMiddleware<TRequestHeaders>(RequestDelegate next)
where TRequestHeaders : RequestHeaders
{
    public async Task InvokeAsync(HttpContext context, TRequestHeaders requestHeaders)
    {
        requestHeaders.Headers = context.Request.Headers.ToDictionary(x => x.Key, x => x.Value.ToString());
        await next(context);
    }
    
    protected static T? ParseHeaderValue<T>(HttpContext context, string headerName)
    {
        return context.Request.Headers.TryGetValue(headerName, out var value) 
            ? Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value!) : default;
    }
    
    protected static Guid GetGuidFromHeader(HttpContext context, string headerName)
    {
        if (context.Request.Headers.TryGetValue(headerName, out var value))
        {
            return Guid.TryParse(value, out var guid) ? guid : Guid.Empty;
        }
        return Guid.Empty;
    }
    
    protected static string? GetStringFromHeader(HttpContext context, string headerName)
    {
        if (context.Request.Headers.TryGetValue(headerName, out var value))
        {
            return value;
        }
        return string.Empty;
    }
    
    protected static int? GetIntFromHeader(HttpContext context, string headerName)
    {
        if (context.Request.Headers.TryGetValue(headerName, out var value))
        {
            return int.TryParse(value, out var number) ? number : null;
        }
        return null;
    }
    
    protected static bool? GetBoolFromHeader(HttpContext context, string headerName)
    {
        if (context.Request.Headers.TryGetValue(headerName, out var value))
        {
            return bool.TryParse(value, out var boolean) ? boolean : null;
        }
        return null;
    }
    
    [SuppressMessage("ReSharper", "UseCollectionExpression")]
    protected static List<string>? GetStringListFromHeader(HttpContext context, string headerName)
    {
        return context.Request.Headers.TryGetValue(headerName, out var value) ? 
            Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(value.ToString()) : new List<string>();
    }
}