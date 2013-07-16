using System.Collections.Generic;

namespace EventToolkit
{
  public interface IEventScopeContainer {
    ScopedEventBus CreateScope();
  }

  public class GlobalEventBus : ScopedEventBus, IEventScopeContainer
  {
    static object sync = new object();

    protected override void AddSubscription(IEventSubscription subscription)
    {
      lock (sync)
        base.AddSubscription(subscription);
    }

    protected override void RemoveSubscription(IEventSubscription subscription) {
      lock (sync)
        base.RemoveSubscription(subscription);
    }

    protected override IEnumerable<IEventSubscription> GetSubscriptions<TMessage>(TMessage message)
    {
      lock (sync)
        return base.GetSubscriptions(message);
    }

    public ScopedEventBus CreateScope() {
      return new ScopedEventBus(this);
    }
  }
}

