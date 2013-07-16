using System;

namespace EventToolkit
{
  public class EventBus
  {
    [ThreadStatic]
    static ScopedEventBus bus;
    static ScopedEventBus Bus {
      get {
        return bus ?? (bus = new ScopedEventBus());
      }
    }

    public static IEventSubscription Subscribe<TMessage>(Action<TMessage> handler)
      where TMessage : IEventMessage
    {
      return Bus.Subscribe(handler);
    }

    public static IEventSubscription Subscribe<TMessage>(IEventSubscriber subscriber)
      where TMessage : IEventMessage
    {
      return Bus.Subscribe<TMessage>(subscriber);
    }

    public static void Publish<TMessage>(TMessage eventMessage)
      where TMessage : IEventMessage
    {
      Bus.Publish(eventMessage);
    }

    public static void Clear() {
      if (bus != null) {
        bus.Clear();
        bus = null;
      }
    }
  }
}
