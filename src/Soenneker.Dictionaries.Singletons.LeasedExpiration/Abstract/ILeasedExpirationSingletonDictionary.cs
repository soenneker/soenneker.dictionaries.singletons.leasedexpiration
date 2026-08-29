using Soenneker.Dictionaries.SingletonKeys.LeasedExpiration.Abstract;

namespace Soenneker.Dictionaries.Singletons.LeasedExpiration.Abstract;

/// <summary>
/// Specializes leased-expiration singleton storage for string keys.
/// </summary>
public interface ILeasedExpirationSingletonDictionary<TValue> : ILeasedExpirationSingletonKeyDictionary<string, TValue>;
