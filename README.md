[![](https://img.shields.io/nuget/v/soenneker.dictionaries.singletons.leasedexpiration.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dictionaries.singletons.leasedexpiration/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dictionaries.singletons.leasedexpiration/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dictionaries.singletons.leasedexpiration/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dictionaries.singletons.leasedexpiration.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dictionaries.singletons.leasedexpiration/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Dictionaries.Singletons.LeasedExpiration
### Thread-safe leased singletons with idle expiration and safe disposal.

## Installation

```
dotnet add package Soenneker.Dictionaries.Singletons.LeasedExpiration
```

## Usage

```csharp
var clients = new LeasedExpirationSingletonDictionary<MyClient>(
    TimeSpan.FromMinutes(15),
    static key => new MyClient(key));

await using SingletonLease<string, MyClient> lease = await clients.GetLease("tenant-a", cancellationToken);

await lease.Value.Send(cancellationToken);
```

Values are disposed after the idle expiration only when no leases are active. Expiration is scanned by one dictionary-wide sweeper, so disposal can occur up to one sweep interval after the idle window. Callers should not store or use `lease.Value` after disposing the lease.
