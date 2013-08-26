using System;

namespace EventToolkit
{
    public interface IEventMonitor {
        IEventSubscription Monitor<TMessage>(Action<TMessage> handler)
            where TMessage : IEventMessage;

        IEventSubscription Monitor<TMessage>(IEventSubscriber subscriber)
            where TMessage : IEventMessage;
    }
}