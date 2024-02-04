using System.Diagnostics.CodeAnalysis;
using Ntt.RefitAbstraction.Client.Handlers;

namespace RefitAbstractions.DemoClient.Handlers;

public class CustomHeaderFillingHandler : HeaderFillingHandler
{
    
    public CustomHeaderFillingHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        AddCustomHeaders();
    }
    
    private void AddCustomHeaders()
    {
        AddHeader("CustomHeader", "CustomHeaderValue");
        // AddHeaderFromContext("Authorization");
        // AddHeaderFromContext("X-LanguageCode", "LanguageCode");
    }
}