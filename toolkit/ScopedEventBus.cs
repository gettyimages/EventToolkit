using System;
using System.Collections.Generic;
using System.Linq;

namespace EventToolkit
{
    public class ScopedEventBus : IEventBus
    {
        readonly List<IEventSubscription> subscriptions = new List<IEventSubscription>();
        readonly GlobalEventBus outerBus;

        internal ScopedEventBus()
        {
        }

        internal ScopedEventBus(GlobalEventBus outerBus)
        {
            this.outerBus = outerBus;
        }

        public IEventSubscription Subscribe<TEvent>(Action<TEvent> handler)
          where TEvent : IEvent
        {
            var subscription = new EventSubscriptionDelegate<TEvent>(this, handler);
            AddSubscription(subscription);
            return subscription;
        }

        public IEventSubscription Subscribe<TEvent>(IEventSubscriber subscriber)
          where TEvent : IEvent
        {
            var subscription = new EventSubscription(this, typeof(TEvent), subscriber);
            AddSubscription(subscription);
            return subscription;
        }

        internal void Unsubscribe(IEventSubscription subscription)
        {
            RemoveSubscription(subscription);
        }

        public void Publish<TEvent>(TEvent eventMessage)
          where TEvent : IEvent
        {
            foreach (var subscription in GetSubscriptions(eventMessage))
                subscription.Send(eventMessage);
            if (outerBus != null) outerBus.Publish(eventMessage);
        }

        internal void Clear()
        {
            subscriptions.ForEach(s => s.Dispose());
            subscriptions.Clear();
        }

        protected virtual IEnumerable<IEventSubscription> GetSubscriptions<TEvent>(TEvent eventMessage)
          where TEvent : IEvent
        {
            return subscriptions.Where(s => s.EventType.IsInstanceOfType(eventMessage));
        }

        protected virtual void AddSubscription(IEventSubscription subscription)
        {
            subscriptions.Add(subscription);
        }

        protected virtual void RemoveSubscription(IEventSubscription subscription)
        {
            subscriptions.Remove(subscription);
        }
    }
}

