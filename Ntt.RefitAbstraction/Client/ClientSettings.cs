namespace Ntt.RefitAbstraction.Client;

public class ClientSettings
{
    public string BaseUrl { get; set; } = null!;
    
    public IDictionary<string, string>? IncludedEndpoints { get; set; }
}