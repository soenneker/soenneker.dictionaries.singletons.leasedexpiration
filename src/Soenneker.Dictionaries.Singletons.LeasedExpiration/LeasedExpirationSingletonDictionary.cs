using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Dictionaries.SingletonKeys.LeasedExpiration;
using Soenneker.Dictionaries.Singletons.LeasedExpiration.Abstract;

namespace Soenneker.Dictionaries.Singletons.LeasedExpiration;

/// <inheritdoc cref="ILeasedExpirationSingletonDictionary{TValue}"/>
public sealed class LeasedExpirationSingletonDictionary<TValue> : LeasedExpirationSingletonKeyDictionary<string, TValue>,
    ILeasedExpirationSingletonDictionary<TValue>
{
    public LeasedExpirationSingletonDictionary(TimeSpan idleExpiration, TimeSpan? sweepInterval = null) : base(idleExpiration, sweepInterval)
    {
    }

    public LeasedExpirationSingletonDictionary(TimeSpan idleExpiration, Func<string, ValueTask<TValue>> func,
        TimeSpan? sweepInterval = null) : base(idleExpiration, func, sweepInterval)
    {
    }

    public LeasedExpirationSingletonDictionary(TimeSpan idleExpiration, Func<string, CancellationToken, ValueTask<TValue>> func,
        TimeSpan? sweepInterval = null) : base(idleExpiration, func, sweepInterval)
    {
    }

    public LeasedExpirationSingletonDictionary(TimeSpan idleExpiration, Func<ValueTask<TValue>> func,
        TimeSpan? sweepInterval = null) : base(idleExpiration, func, sweepInterval)
    {
    }

    public LeasedExpirationSingletonDictionary(TimeSpan idleExpiration, Func<string, TValue> func,
        TimeSpan? sweepInterval = null) : base(idleExpiration, func, sweepInterval)
    {
    }

    public LeasedExpirationSingletonDictionary(TimeSpan idleExpiration, Func<string, CancellationToken, TValue> func,
        TimeSpan? sweepInterval = null) : base(idleExpiration, func, sweepInterval)
    {
    }

    public LeasedExpirationSingletonDictionary(TimeSpan idleExpiration, Func<TValue> func, TimeSpan? sweepInterval = null) : base(idleExpiration, func,
        sweepInterval)
    {
    }

    /// <summary>
    /// Fluent typed wrapper around <see cref="LeasedExpirationSingletonKeyDictionary{TKey,TValue}.Initialize{TState}"/>.
    /// </summary>
    /// <typeparam name="TState">Type of state passed to the callback.</typeparam>
    /// <param name="state">State value used by the variant.</param>
    /// <param name="factory">Factory used to create a value when one is needed.</param>
    /// <returns>The resulting leased Expiration Singleton Dictionary.</returns>
    public new LeasedExpirationSingletonDictionary<TValue> Initialize<TState>(TState state,
        Func<TState, string, CancellationToken, ValueTask<TValue>> factory) where TState : notnull
    {
        base.Initialize(state, factory);
        return this;
    }
}
