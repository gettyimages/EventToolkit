using EventToolkit;
using Kekiri;
using FluentAssertions;

namespace Specs
{
    [Scenario("Publishing")]
    public class When_publishing_an_event : EventSpec
    {
        bool notified;

        [Given]
        public void given()
        {
            EventBus.Subscribe<EventMessage>(_ => notified = true);
        }

        [When]
        public void when()
        {
            EventBus.Publish(new EventMessage());
        }

        [Then]
        public void then_it_notifies_the_subscribers()
        {
            notified.Should().BeTrue();
        }
    }

    [Scenario("Publishing")]
    public class When_publishing_an_event_with_multiple_subscribers : EventSpec
    {
        string notified = string.Empty;

        [Given]
        public void given()
        {
            EventBus.Subscribe<EventMessage>(_ => notified += "a");
            EventBus.Subscribe<EventMessage>(_ => notified += "b");
            EventBus.Subscribe<EventMessage>(_ => notified += "c");
        }

        [When]
        public void when()
        {
            EventBus.Publish(new EventMessage());
        }

        [Then]
        public void then_it_notifies_the_subscribers()
        {
            notified.Should().Be("abc");
        }
    }

    [Scenario("Publishing")]
    public class When_publishing_an_event_of_a_derived_type : EventSpec
    {
        bool base_notification;
        bool derived_notification;

        [Given]
        public void given()
        {
            EventBus.Subscribe<BaseEventMessage>(_ => base_notification = true);
            EventBus.Subscribe<DerivedEventMessage>(_ => derived_notification = true);
        }

        [When]
        public void when()
        {
            EventBus.Publish(new DerivedEventMessage());
        }


        [Then]
        public void then_it_notifies_the_subscribers_of_the_base_type()
        {
            base_notification.Should().BeTrue();
        }

        [Then]
        public void then_it_notifies_the_subscribers_of_the_derived_type()
        {
            derived_notification.Should().BeTrue();
        }
    }

    [Scenario("Publishing")]
    public class When_publishing_an_event_of_a_base_type : EventSpec
    {
        bool base_notification;
       bool derived_notification;

        [Given]
        public void given()
        {
            EventBus.Subscribe<BaseEventMessage>(_ => base_notification = true);
            EventBus.Subscribe<DerivedEventMessage>(_ => derived_notification = true);
        }

        [When]
        public void when()
        {
            EventBus.Publish(new BaseEventMessage());
        }

        [Then]
        public void then_it_notifies_the_subscribers_of_the_base_type()
        {
            base_notification.Should().BeTrue();
        }

        [Then]
        public void then_it_does_not_notify_the_subscribers_of_the_derived_type()
        {
            derived_notification.Should().BeFalse();
        }
    }
}
