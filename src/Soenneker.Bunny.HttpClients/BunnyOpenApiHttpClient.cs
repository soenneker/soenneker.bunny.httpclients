using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Bunny.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Bunny.HttpClients;

///<inheritdoc cref="IBunnyOpenApiHttpClient"/>
public sealed class BunnyOpenApiHttpClient : IBunnyOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;

    private const string _prodBaseUrl = "https://api.bunny.net";

    public BunnyOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(BunnyOpenApiHttpClient),
            (config: _config, baseUrl: _config["Bunny:ClientBaseUrl"] ?? _prodBaseUrl), static state =>
            {
                var apiKey = state.config.GetValueStrict<string>("Bunny:ApiKey");
                string authHeaderName = state.config["Bunny:AuthHeaderName"] ?? "Authorization";
                string authHeaderValueTemplate = state.config["Bunny:AuthHeaderValueTemplate"] ?? "Bearer {token}";
                string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

                return new HttpClientOptions
                {
                    BaseAddress = new Uri(state.baseUrl),
                    DefaultRequestHeaders = new Dictionary<string, string>
                    {
                        { authHeaderName, authHeaderValue },
                    }
                };
            }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(nameof(BunnyOpenApiHttpClient));
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(nameof(BunnyOpenApiHttpClient));
    }
}