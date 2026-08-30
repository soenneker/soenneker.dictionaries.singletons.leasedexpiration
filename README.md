[![](https://img.shields.io/nuget/v/soenneker.dictionaries.singletons.leasedexpiration.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dictionaries.singletons.leasedexpiration/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dictionaries.singletons.leasedexpiration/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dictionaries.singletons.leasedexpiration/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dictionaries.singletons.leasedexpiration.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dictionaries.singletons.leasedexpiration/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dictionaries.singletons.leasedexpiration/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.dictionaries.singletons.leasedexpiration/actions/workflows/codeql.yml)

# Soenneker.Dictionaries.Singletons.LeasedExpiration

String-key convenience type for a singleton cache with idle expiration and lease-safe value disposal.

## Installation

```bash
dotnet add package Soenneker.Dictionaries.Singletons.LeasedExpiration
```

## Usage

```csharp
using Soenneker.Dictionaries.SingletonKeys.LeasedExpiration;
using Soenneker.Dictionaries.Singletons.LeasedExpiration;

await using var clients = new LeasedExpirationSingletonDictionary<ApiClient>(
    idleExpiration: TimeSpan.FromMinutes(10),
    func: (name, cancellationToken) => ApiClient.Connect(name, cancellationToken));

await using SingletonLease<string, ApiClient> lease =
    await clients.GetLease("billing", cancellationToken);

await lease.Value.Send(request, cancellationToken);
```

Keep the lease alive for every operation that uses `Value`. A value whose idle deadline passes is not disposed while any lease remains active; disposal happens after the final lease is released.

Each successful `GetLease` refreshes the key’s idle deadline. Concurrent acquisition for one missing key shares one factory execution, while different keys can initialize concurrently.

## Configuration and cleanup

An optional `sweepInterval` controls how frequently the cache scans for idle entries:

```csharp
var clients = new LeasedExpirationSingletonDictionary<ApiClient>(
    idleExpiration: TimeSpan.FromMinutes(10),
    sweepInterval: TimeSpan.FromSeconds(30));

clients.SetInitialization((name, cancellationToken) =>
    ApiClient.Connect(name, cancellationToken));
```

`Remove` returns `false` while a value has active leases. `Clear` detaches current entries so new acquisitions create replacements, then disposes each old value after its final lease ends. Dictionary disposal stops new acquisitions and the sweeper while preserving the same lease-safe deferred cleanup.

The dictionary owns cached values and prefers `IAsyncDisposable` over `IDisposable`. Use `Soenneker.Dictionaries.SingletonKeys.LeasedExpiration` directly when keys are not strings.
