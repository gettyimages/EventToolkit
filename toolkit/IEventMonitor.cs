using System;

namespace EventToolkit
{
    public interface IEventMonitor {
        IEventSubscription Monitor<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent;

        IEventSubscription Monitor<TEvent>(IEventSubscriber subscriber)
            where TEvent : IEvent;
    }
}