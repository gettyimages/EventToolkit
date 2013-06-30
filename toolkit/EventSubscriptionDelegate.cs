using System;

namespace DomainToolkit
{
  class EventSubscriptionDelegate<T> : IEventSubscription
    where T : IEventMessage
  {
    readonly Action<T> handler;
    readonly ScopedEventBus bus;

    public EventSubscriptionDelegate(ScopedEventBus bus, Action<T> handler)
    {
      if (handler == null)
        throw new ArgumentNullException("handler");

      this.bus = bus;
      this.handler = handler;
    }

    public Type MessageType {
      get { return typeof(T); }
    }

    public void Send(IEventMessage message)
    {
      handler((T)message);
    }

    public void Dispose() {
      bus.Unsubscribe(this);
    }
  }
}
