using System;

namespace EventToolkit
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent eventMessage)
          where TEvent : IEvent;

        IEventSubscription Subscribe<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent;

        IEventSubscription Subscribe<TEvent>(IEventSubscriber subscriber)
            where TEvent : IEvent;
    }
}
