using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainToolkit 
{
  public class ScopedEventBus : IEventBus
  {
    List<IEventSubscription> subscriptions = new List<IEventSubscription>();
    GlobalEventBus outerBus;

    public ScopedEventBus() {
    }

    internal ScopedEventBus(GlobalEventBus outerBus) {
      this.outerBus = outerBus;
    }

    public IEventSubscription Subscribe<T>(Action<T> handler)
      where T : IEventMessage
    {
      var subscription = new EventSubscriptionDelegate<T>(this, handler);
      AddSubscription(subscription);
      return subscription;
    }

    public IEventSubscription Subscribe<T>(IEventSubscriber subscriber)
      where T : IEventMessage
    {
      var subscription = new EventSubscription(this, typeof(T), subscriber);
      AddSubscription(subscription);
      return subscription;
    }

    internal void Unsubscribe(IEventSubscription subscription) {
      RemoveSubscription(subscription);
    }

    public void Publish<T>(T message)
      where T : IEventMessage
    {
      foreach (var subscription in GetSubscriptions(message))
        subscription.Send(message);
      if (outerBus != null) outerBus.Publish(message);
    }

    internal void Clear() {
      subscriptions.ForEach(s => s.Dispose());
      subscriptions.Clear();
    }

    protected virtual IEnumerable<IEventSubscription> GetSubscriptions<T>(T message)
      where T : IEventMessage
    {
      return subscriptions.Where(s => {
        return s.MessageType.IsAssignableFrom(message.GetType());
      });
    }

    protected virtual void AddSubscription(IEventSubscription subscription) {
      subscriptions.Add(subscription);
    }

    protected virtual void RemoveSubscription(IEventSubscription subscription) {
      subscriptions.Remove(subscription);
    }
  }
}

