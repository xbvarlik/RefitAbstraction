using System.Net;
using Ntt.Exceptions.ExceptionTypes;
using Ntt.RefitAbstraction.Client.Handlers;

namespace RefitAbstractions.DemoClient.Handlers;

public class CustomClientExceptionHandler(IHttpContextAccessor httpContextAccessor) : ClientExceptionHandler(httpContextAccessor)
{
    protected override async Task<HttpResponseMessage> HandleErrorAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
            return response;

        throw response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => new UnauthorizedException(),
            HttpStatusCode.Forbidden => new ForbiddenException(),
            HttpStatusCode.NotFound => new NotFoundException(),
            HttpStatusCode.InternalServerError => new OperationalException(),
            _ => new BusinessException()
        };
    }
}