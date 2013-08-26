using System;

namespace EventToolkit
{
    public interface IEventMonitor {
        IEventSubscription Monitor<TMessage>(Action<TMessage> handler)
            where TMessage : IEventMessage;

        IEventSubscription Monitor<TMessage>(IEventSubscriber subscriber)
            where TMessage : IEventMessage;
    }

    public static class EventMonitor
    {
        public static IEventMonitor Current {
            get { return EventCore.CoreBus; }
        }

        public static IEventSubscription Monitor<TMessage>(Action<TMessage> handler)
            where TMessage : IEventMessage
        {
            return Current.Monitor(handler);
        }

        public static IEventSubscription Monitor<TMessage>(IEventSubscriber subscriber)
            where TMessage : IEventMessage
        {
            return Current.Monitor<TMessage>(subscriber);
        }
    }
}