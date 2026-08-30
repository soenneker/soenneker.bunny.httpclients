[![](https://img.shields.io/nuget/v/soenneker.bunny.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.bunny.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.bunny.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.bunny.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.bunny.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.bunny.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.bunny.httpclients/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.bunny.httpclients/actions/workflows/codeql.yml)

# Soenneker.Bunny.HttpClients

Provides a cached, authenticated `HttpClient` for bunny.net APIs.

## Installation

```bash
dotnet add package Soenneker.Bunny.HttpClients
```

## Configuration

```json
{
  "Bunny": {
    "ApiKey": "your-access-key"
  }
}
```

`Bunny:ApiKey` is required. The defaults are `https://api.bunny.net` and `AccessKey: {token}`. `Bunny:ClientBaseUrl`, `Bunny:AuthHeaderName`, and `Bunny:AuthHeaderValueTemplate` can override them.

bunny.net products do not all use the same host or credential. Configure the base URL and header for the product you call; storage zones, Stream, JWT-authenticated endpoints, and other specialized APIs may require a different key or authentication format.

## Registration

```csharp
using Soenneker.Bunny.HttpClients.Registrars;

services.AddBunnyOpenApiHttpClientAsSingleton();
```

`AddBunnyOpenApiHttpClientAsScoped()` is also available. Both registrations use the singleton HTTP-client cache.

## Usage

```csharp
using Soenneker.Bunny.HttpClients.Abstract;

HttpClient client = await clientProvider.Get(cancellationToken);
using HttpResponseMessage response = await client.GetAsync("pullzone", cancellationToken);
response.EnsureSuccessStatusCode();
```

`Get()` creates the named client on first use and returns it afterward. Configuration changes do not rebuild an existing client. Do not dispose the returned `HttpClient` per request. Disposing the provider removes and disposes its named client from the cache.
