using Soenneker.Bunny.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Bunny.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class BunnyOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IBunnyOpenApiHttpClient _httpclient;

    public BunnyOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IBunnyOpenApiHttpClient>(true);
    }

    [Test]
    [Skip("Manual")]
    public void Default()
    {

    }
}
