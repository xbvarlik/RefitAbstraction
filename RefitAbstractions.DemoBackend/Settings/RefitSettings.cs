using Ntt.RefitAbstraction.Server;

namespace RefitAbstractions.DemoBackend.Settings;

public class RefitSettings : RequestHeaders
{
    public Guid UserId { get; set; }
}