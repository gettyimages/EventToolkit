using System;

namespace EventToolkit
{
    public class EventBus
    {
        [ThreadStatic]
        static ScopedEventBus bus;
        static ScopedEventBus Bus {
            get {
                return bus ?? (bus = EventCore.CreateScope());
            }
        }

        public static IEventBus Current {
            get { return Bus; }
        }

        public static IEventSubscription Subscribe<TEvent>(Action<TEvent> handler)
          where TEvent : IEvent
        {
            return Bus.Subscribe(handler);
        }

        public static IEventSubscription Subscribe<TEvent>(IEventSubscriber subscriber)
          where TEvent : IEvent
        {
            return Bus.Subscribe<TEvent>(subscriber);
        }

        public static IEventSubscription Subscribe(Type eventType, IEventSubscriber subscriber)
        {
            return Bus.Subscribe(eventType, subscriber);
        }

        public static void Publish<TEvent>(TEvent eventMessage)
          where TEvent : IEvent
        {
            Bus.Publish(eventMessage);
        }

        public static void Clear()
        {
            if (bus == null) return;
            bus.Clear();
            bus = null;
        }
    }
}
