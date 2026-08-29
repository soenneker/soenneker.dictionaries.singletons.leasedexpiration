[![](https://img.shields.io/nuget/v/soenneker.dictionaries.singletons.leasedexpiration.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dictionaries.singletons.leasedexpiration/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dictionaries.singletons.leasedexpiration/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dictionaries.singletons.leasedexpiration/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dictionaries.singletons.leasedexpiration.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dictionaries.singletons.leasedexpiration/)

# Soenneker.Dictionaries.Singletons.LeasedExpiration

Specializes leased-expiration singleton storage for string keys.

## Install

```bash
dotnet add package Soenneker.Dictionaries.Singletons.LeasedExpiration
```

## What you get

- `ILeasedExpirationSingletonDictionary<TValue>` — Specializes leased-expiration singleton storage for string keys.

## Practical notes

- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
