using System;
using System.Transactions;

namespace EventToolkit
{
    public interface IEventSubscription : IDisposable
    {
        Type MessageType { get; }
        void Send(IEventMessage message);
    }

    class EventSubscription : IEventSubscription
    {
        readonly Type messageType;
        readonly ScopedEventBus bus;
        readonly IEventSubscriber subscriber;

        public EventSubscription(ScopedEventBus bus, Type messageType, IEventSubscriber subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException("subscriber");

            this.bus = bus;
            this.messageType = messageType;
            this.subscriber = subscriber;
        }

        public Type MessageType {
            get{ return messageType; }
        }

        public void Send(IEventMessage message)
        {
            if (Transaction.Current == null)
                subscriber.Handle(message);
            else
            {
                Transaction.Current
                    .EnlistVolatile(new TransactionNotification(() =>
                        subscriber.Handle(message)), EnlistmentOptions.None);
            }
        }

        public void Dispose()
        {
            bus.Unsubscribe(this);
            subscriber.Dispose();
        }
    }
}
