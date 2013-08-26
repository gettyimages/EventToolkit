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

        protected override IEnumerable<IEventSubscription> GetSubscriptions<TMessage>(TMessage message)
        {
            lock (sync)
                return base.GetSubscriptions(message);
        }

        IEventSubscription IEventMonitor.Monitor<TMessage>(Action<TMessage> handler)
        {
            return Subscribe(handler);
        }

        IEventSubscription IEventMonitor.Monitor<TMessage>(IEventSubscriber subscriber)
        {
            return Subscribe<TMessage>(subscriber);
        }
    }
}

