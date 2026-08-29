using Soenneker.Dictionaries.SingletonKeys.LeasedExpiration.Abstract;

namespace Soenneker.Dictionaries.Singletons.LeasedExpiration.Abstract;

public interface ILeasedExpirationSingletonDictionary<TValue> : ILeasedExpirationSingletonKeyDictionary<string, TValue>;
