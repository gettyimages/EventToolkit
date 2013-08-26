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

        public static IEventSubscription Subscribe<TMessage>(Action<TMessage> handler,
            SubscriptionScope scope = SubscriptionScope.Instance)
          where TMessage : IEventMessage
        {
            return scope == SubscriptionScope.Instance ?
                Bus.Subscribe(handler) : EventCore.Monitor(handler);
        }

        public static IEventSubscription Subscribe<TMessage>(IEventSubscriber subscriber,
            SubscriptionScope scope = SubscriptionScope.Instance)
          where TMessage : IEventMessage
        {
            return scope == SubscriptionScope.Instance ?
                Bus.Subscribe<TMessage>(subscriber) : EventCore.Monitor<TMessage>(subscriber);
        }

        public static void Publish<TMessage>(TMessage eventMessage)
          where TMessage : IEventMessage
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
