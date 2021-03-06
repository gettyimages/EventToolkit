using System;
using EventToolkit;
using Kekiri;
using FluentAssertions;

namespace Specs
{
    [Scenario("Subscribing")]
    public class When_subscribing_to_an_event_with_a_delegate : EventSpec
    {
        IEventSubscription subscription;

        [When]
        public void when()
        {
            subscription = EventBus.Subscribe<Event>(_ => { });
        }

        [Then]
        public void then_it_creates_a_subscription()
        {
            subscription.Should().NotBeNull();
        }
    }

    [Scenario("Subscribing")]
    public class When_subscribing_to_an_event_with_a_subscriber : EventSpec
    {
        IEventSubscription subscription;

        [When]
        public void when()
        {
            subscription = EventBus.Subscribe<Event>(new SimpleSubscriber());
        }

        [Then]
        public void then_it_creates_a_subscription()
        {
            subscription.Should().NotBeNull();
        }
    }

    [Scenario("Subscribing")]
    public class When_subscribing_to_the_current_event_bus : EventSpec
    {
        IEventSubscription subscription;

        [When]
        public void when()
        {
            subscription = EventBus.Current.Subscribe<Event>(_ => { });
        }

        [Then]
        public void then_it_creates_a_subscription()
        {
            subscription.Should().NotBeNull();
        }
    }

    [Scenario("Subscribing")]
    public class When_subscribing_to_an_event_with_a_null_delegate_reference : EventSpec
    {
        Exception exception;

        [When, Throws]
        public void when()
        {
            EventBus.Subscribe((Action<Event>)null);
        }

        [Then]
        public void then_it_rejects_the_subscription()
        {
            Catch<ArgumentNullException>();
        }
    }

    [Scenario("Subscribing")]
    public class When_subscribing_to_an_event_with_a_null_subscriber_reference : EventSpec
    {
        Exception exception;

        [When, Throws]
        public void when()
        {
            EventBus.Subscribe<Event>((IEventSubscriber)null);
        }

        [Then]
        public void then_it_rejects_the_subscription()
        {
            Catch<ArgumentNullException>();
        }
    }

    [Scenario("Subscribing")]
    public class When_a_delegate_subscription_is_disposed : EventSpec
    {
        bool notified;
        IEventSubscription subscription;

        [Given]
        public void given()
        {
            subscription = EventBus.Subscribe<Event>(_ => notified = true);
        }

        [When]
        public void when()
        {
            subscription.Dispose();
        }

        [Then]
        public void then_it_does_not_receive_events()
        {
            EventBus.Publish(new Event());
            notified.Should().BeFalse();
        }
    }

    [Scenario("Subscribing")]
    public class When_a_subscription_is_disposed : EventSpec
    {
        bool notified;
        SimpleSubscriber subscriber = new SimpleSubscriber();
        IEventSubscription subscription;

        [Given]
        public void given()
        {
            subscription = EventBus.Subscribe<Event>(subscriber);
        }

        [When]
        public void when()
        {
            subscription.Dispose();
        }

        [Then]
        public void then_it_disposes_the_subscribers()
        {
            subscriber.disposed.Should().BeTrue();
        }
    }
}
