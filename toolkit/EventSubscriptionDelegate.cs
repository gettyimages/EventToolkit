using System;

namespace EventToolkit
{
  class EventSubscriptionDelegate<TMessage> : IEventSubscription
    where TMessage : IEventMessage
  {
    readonly Action<TMessage> handler;
    readonly ScopedEventBus bus;

    public EventSubscriptionDelegate(ScopedEventBus bus, Action<TMessage> handler)
    {
      if (handler == null)
        throw new ArgumentNullException("handler");

      this.bus = bus;
      this.handler = handler;
    }

    public Type MessageType {
      get { return typeof(TMessage); }
    }

    public void Send(IEventMessage message)
    {
      handler((TMessage)message);
    }

    public void Dispose() {
      bus.Unsubscribe(this);
    }
  }
}
