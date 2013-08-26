using EventToolkit;
using FluentAssertions;
using Kekiri;

namespace Specs
{
    [Scenario("Monitoring")]
    public class When_monitoring_an_event_with_a_delegate : EventSpec
    {
        IEventSubscription subscription;

        [When]
        public void when()
        {
            subscription = EventMonitor.Monitor<EventMessage>(_ => { });
        }

        [Then]
        public void then_it_creates_a_subscription()
        {
            subscription.Should().NotBeNull();
        }
    }

    [Scenario("Monitoring")]
    public class When_monitoring_an_event_with_a_subscriber : EventSpec
    {
        IEventSubscription subscription;

        [When]
        public void when()
        {
            subscription = EventMonitor.Monitor<EventMessage>(new SimpleSubscriber());
        }

        [Then]
        public void then_it_creates_a_subscription()
        {
            subscription.Should().NotBeNull();
        }
    }
}
