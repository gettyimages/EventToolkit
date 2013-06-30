using System;

namespace DomainToolkit
{
  public interface IEventSubscription : IDisposable
  {
    Type MessageType { get; }
    void Send(IEventMessage message);
  }

  class EventSubscription : IEventSubscription {
    readonly ScopedEventBus bus;
    readonly IEventSubscriber subscriber;
    readonly Type messageType;

    public EventSubscription(ScopedEventBus bus, Type messageType, IEventSubscriber subscriber) {
      if (subscriber == null)
        throw new ArgumentNullException("subscriber");

      this.bus = bus;
      this.messageType = messageType;
      this.subscriber = subscriber;
    }

    public Type MessageType {
      get {
        return messageType;
      }
    }

    public void Send(IEventMessage message)
    {
      subscriber.Handle(message);
    }

    public void Dispose()
    {
      bus.Unsubscribe(this);
      subscriber.Dispose();
    }
  }
}
