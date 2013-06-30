using System;

namespace DomainToolkit
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

    public static IEventSubscription Subscribe<T>(Action<T> handler)
      where T : IEventMessage
    {
      return Bus.Subscribe<T>(handler);
    }

    public static IEventSubscription Subscribe<T>(IEventSubscriber subscriber)
      where T : IEventMessage
    {
      return Bus.Subscribe<T>(subscriber);
    }

    public static void Publish<T>(T eventMessage)
      where T : IEventMessage
    {
      Bus.Publish<T>(eventMessage);
    }

    public static void Clear() {
      if (bus != null) {
        bus.Clear();
        bus = null;
      }
    }
  }
}
