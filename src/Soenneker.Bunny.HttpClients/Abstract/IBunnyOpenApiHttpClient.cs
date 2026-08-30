using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Bunny.HttpClients.Abstract;

/// <summary>
/// Provides the cached, authenticated <see cref="HttpClient"/> used by the bunny.net OpenAPI client.
/// </summary>
public interface IBunnyOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the named HTTP client, creating and configuring it on first use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel client creation.</param>
    /// <returns>The cached HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
