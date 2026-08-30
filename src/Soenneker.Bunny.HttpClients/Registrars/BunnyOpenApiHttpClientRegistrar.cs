using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Bunny.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Bunny.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class BunnyOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="IBunnyOpenApiHttpClient"/> as a singleton service backed by the singleton HTTP-client cache.
    /// </summary>
    public static IServiceCollection AddBunnyOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IBunnyOpenApiHttpClient, BunnyOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IBunnyOpenApiHttpClient"/> as a scoped service backed by the singleton HTTP-client cache.
    /// </summary>
    public static IServiceCollection AddBunnyOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IBunnyOpenApiHttpClient, BunnyOpenApiHttpClient>();

        return services;
    }
}
