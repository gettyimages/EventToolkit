using System;
using System.Collections.Generic;

namespace EventToolkit
{
    class GlobalEventBus : ScopedEventBus, IEventMonitor
    {
        static readonly object sync = new object();

        protected override void AddSubscription(IEventSubscription subscription)
        {
            lock (sync)
                base.AddSubscription(subscription);
        }

        protected override void RemoveSubscription(IEventSubscription subscription)
        {
            lock (sync)
                base.RemoveSubscription(subscription);
        }

        protected override IEnumerable<IEventSubscription> GetSubscriptions<TEvent>(TEvent eventMessage)
        {
            lock (sync)
                return base.GetSubscriptions(eventMessage);
        }

        IEventSubscription IEventMonitor.Monitor<TEvent>(Action<TEvent> handler)
        {
            return Subscribe(handler);
        }

        IEventSubscription IEventMonitor.Monitor<TEvent>(IEventSubscriber subscriber)
        {
            return Subscribe<TEvent>(subscriber);
        }
    }
}

