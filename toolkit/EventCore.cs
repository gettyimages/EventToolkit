
using System;

namespace EventToolkit
{
    static class EventCore
    {
        internal static readonly GlobalEventBus CoreBus = new GlobalEventBus();

        public static ScopedEventBus CreateScope()
        {
            return new ScopedEventBus(CoreBus);
        }

        public static IEventSubscription Monitor<TMessage>(Action<TMessage> handler)
          where TMessage : IEventMessage
        {
            return CoreBus.Subscribe(handler);
        }

        public static IEventSubscription Monitor<TMessage>(IEventSubscriber subscriber)
            where TMessage : IEventMessage
        {
            return CoreBus.Subscribe<TMessage>(subscriber);
        }
    }
}