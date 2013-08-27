using System;
using System.Transactions;

namespace EventToolkit
{
    class EventSubscriptionDelegate<TEvent> : IEventSubscription
      where TEvent : IEvent
    {
        readonly Action<TEvent> handler;
        readonly ScopedEventBus bus;

        public EventSubscriptionDelegate(ScopedEventBus bus, Action<TEvent> handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            this.bus = bus;
            this.handler = handler;
        }

        public Type EventType
        {
            get { return typeof(TEvent); }
        }

        public void Send(IEvent eventMessage)
        {
            if (Transaction.Current == null)
                handler((TEvent)eventMessage);
            else
            {
                Transaction.Current
                    .EnlistVolatile(new TransactionNotification(() =>
                        handler((TEvent)eventMessage)), EnlistmentOptions.None);
            }
        }

        public void Dispose()
        {
            bus.Unsubscribe(this);
        }
    }
}
