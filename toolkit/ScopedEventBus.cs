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

        public IEventSubscription Subscribe<TMessage>(Action<TMessage> handler)
          where TMessage : IEventMessage
        {
            var subscription = new EventSubscriptionDelegate<TMessage>(this, handler);
            AddSubscription(subscription);
            return subscription;
        }

        public IEventSubscription Subscribe<TMessage>(IEventSubscriber subscriber)
          where TMessage : IEventMessage
        {
            var subscription = new EventSubscription(this, typeof(TMessage), subscriber);
            AddSubscription(subscription);
            return subscription;
        }

        internal void Unsubscribe(IEventSubscription subscription)
        {
            RemoveSubscription(subscription);
        }

        public void Publish<TMessage>(TMessage message)
          where TMessage : IEventMessage
        {
            foreach (var subscription in GetSubscriptions(message))
                subscription.Send(message);
            if (outerBus != null) outerBus.Publish(message);
        }

        internal void Clear()
        {
            subscriptions.ForEach(s => s.Dispose());
            subscriptions.Clear();
        }

        protected virtual IEnumerable<IEventSubscription> GetSubscriptions<TMessage>(TMessage message)
          where TMessage : IEventMessage
        {
            return subscriptions.Where(s => s.MessageType.IsInstanceOfType(message));
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

