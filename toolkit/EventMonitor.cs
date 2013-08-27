using System;

namespace EventToolkit
{
    public static class EventMonitor
    {
        public static IEventMonitor Current {
            get { return EventCore.CoreBus; }
        }

        public static IEventSubscription Monitor<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent
        {
            return Current.Monitor(handler);
        }

        public static IEventSubscription Monitor<TEvent>(IEventSubscriber subscriber)
            where TEvent : IEvent
        {
            return Current.Monitor<TEvent>(subscriber);
        }
    }
}