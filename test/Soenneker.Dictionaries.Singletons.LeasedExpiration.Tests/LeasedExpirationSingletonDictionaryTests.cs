using System;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Soenneker.Dictionaries.SingletonKeys.LeasedExpiration;
using Soenneker.Tests.Unit;

namespace Soenneker.Dictionaries.Singletons.LeasedExpiration.Tests;

public sealed class LeasedExpirationSingletonDictionaryTests : UnitTest
{
    [Test]
    public async Task GetLease_reuses_value_before_idle_expiration()
    {
        var calls = 0;

        var dict = new LeasedExpirationSingletonDictionary<object>(TimeSpan.FromMilliseconds(200), _ =>
        {
            Interlocked.Increment(ref calls);
            return new object();
        });

        object firstValue;

        await using (SingletonLease<string, object> first = await dict.GetLease("a"))
        {
            firstValue = first.Value;
        }

        await Task.Delay(50);

        await using (SingletonLease<string, object> second = await dict.GetLease("a"))
        {
            second.Value.Should().BeSameAs(firstValue);
        }

        calls.Should().Be(1);

        await dict.DisposeAsync();
    }

    [Test]
    public async Task Expiration_waits_for_active_lease()
    {
        var disposed = 0;

        var dict = new LeasedExpirationSingletonDictionary<DisposableValue>(TimeSpan.FromMilliseconds(60),
            _ => new DisposableValue(() => Interlocked.Increment(ref disposed)));

        SingletonLease<string, DisposableValue> lease = await dict.GetLease("a");

        await Task.Delay(180);

        disposed.Should().Be(0);

        await lease.DisposeAsync();

        disposed.Should().Be(1);

        await dict.DisposeAsync();
    }

    private sealed class DisposableValue : IDisposable
    {
        private readonly Action _onDispose;

        public DisposableValue(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            _onDispose();
        }
    }
}
