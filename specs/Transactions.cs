using System;
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

    [Scenario("Transactions")]
    public class When_an_exception_happens_while_notifying_subscribers : EventSpec
    {
        bool notified;
        TransactionScope trx;

        [Given]
        public void given_a_transaction_scope()
        {
            trx = new TransactionScope();
        }

        [Given]
        public void given_an_unsafe_event_subscription()
        {
            EventBus.Subscribe<EventMessage>(_ => { throw new StackOverflowException(); });
        }

        [Given]
        public void given_a_safe_event_subscription()
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
        public void then_no_exception_is_raised_outside_the_transaction()
        {
        }

        [Then]
        public void then_the_next_subscriber_is_notified()
        {
            notified.Should().BeTrue();
        }
    }
}
