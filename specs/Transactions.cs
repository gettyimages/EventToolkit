using EventToolkit;
using Kekiri;
using FluentAssertions;
using System.Transactions;

namespace Specs
{
    [Scenario("Transactions")]
    public class When_a_transaction_is_completed : EventSpec
    {
        bool notified;
        TransactionScope trx;

        [Given]
        public void given_a_transaction_scope()
        {
            trx = new TransactionScope();
        }

        [Given]
        public void given_an_event_subscription()
        {
            EventBus.Subscribe<EventMessage>(_ => notified = true);
        }

        [Given]
        public void given_an_event_is_published()
        {
            EventBus.Publish(new EventMessage());
        }

        [Given]
        public void given_the_transaction_is_committed()
        {
            trx.Complete();
        }

        [When]
        public void when()
        {
            trx.Dispose();
        }

        [Then]
        public void then_it_notifies_the_subscribers()
        {
            notified.Should().BeTrue();
        }
    }

    [Scenario("Transactions")]
    public class When_a_transaction_is_cancelled : EventSpec
    {
        bool notified;
        TransactionScope trx;

        [Given]
        public void given_a_transaction_scope()
        {
            trx = new TransactionScope();
        }

        [Given]
        public void given_an_event_subscription()
        {
            EventBus.Subscribe<EventMessage>(_ => notified = true);
        }

        [Given]
        public void given_an_event_is_published()
        {
            EventBus.Publish(new EventMessage());
        }

//        [Given]
//        public void given_the_transaction_is_cancelled()
//        {
//            Transaction.Current.Rollback();
//        }

        [When]
        public void when()
        {
            trx.Dispose();
        }

        [Then]
        public void then_it_notifies_the_subscribers()
        {
            notified.Should().BeFalse();
        }
    }
}
