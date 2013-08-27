using System;
using System.Transactions;

namespace EventToolkit
{
    public interface IEventSubscription : IDisposable
    {
        Type EventType { get; }
        void Send(IEvent eventMessage);
    }

    class EventSubscription : IEventSubscription
    {
        readonly Type eventType;
        readonly ScopedEventBus bus;
        readonly IEventSubscriber subscriber;

        public EventSubscription(ScopedEventBus bus, Type eventType, IEventSubscriber subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException("subscriber");

            this.bus = bus;
            this.eventType = eventType;
            this.subscriber = subscriber;
        }

        public Type EventType {
            get{ return eventType; }
        }

        public void Send(IEvent eventMessage)
        {
            if (Transaction.Current == null)
                subscriber.Handle(eventMessage);
            else
            {
                Transaction.Current
                    .EnlistVolatile(new TransactionNotification(() =>
                        subscriber.Handle(eventMessage)), EnlistmentOptions.None);
            }
        }

        public void Dispose()
        {
            bus.Unsubscribe(this);
            subscriber.Dispose();
        }
    }
}
