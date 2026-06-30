using Soenneker.Dictionaries.SingletonKeys.LeasedExpiration.Abstract;

namespace Soenneker.Dictionaries.Singletons.LeasedExpiration.Abstract;

/// <summary>
/// A string-keyed singleton cache that returns leases and disposes values only after they are idle and no leases are active.
/// </summary>
/// <typeparam name="TValue">The leased value type.</typeparam>
/// <remarks>
/// This is a convenience specialization over <see cref="ILeasedExpirationSingletonKeyDictionary{TKey,TValue}"/> using <see cref="string"/> keys.
/// </remarks>
public interface ILeasedExpirationSingletonDictionary<TValue> : ILeasedExpirationSingletonKeyDictionary<string, TValue>;
