using EventToolkit;
using Kekiri;
using FluentAssertions;

namespace Specs
{
    [Scenario("Scopes")]
    public class When_publishing_an_event_to_local_and_global_scopes : EventSpec
    {
        protected SimpleSubscriber localSubscriber = new SimpleSubscriber();
        protected SimpleSubscriber globalSubscriber = new SimpleSubscriber();

        [Given]
        public void given_a_global_subscription()
        {
            EventMonitor.Monitor<EventMessage>(globalSubscriber);
        }

        [Given]
        public void given_a_local_subscription()
        {
            EventBus.Subscribe<EventMessage>(localSubscriber);
        }

        [When]
        public void when()
        {
            EventBus.Publish(new EventMessage());
        }

        [Then]
        public void it_sends_an_event_to_handlers_at_the_local_scope()
        {
            localSubscriber.handled.Should().BeTrue();
        }

        [Then]
        public void it_sends_an_event_to_handlers_at_the_application_scope()
        {
            globalSubscriber.handled.Should().BeTrue();
        }
    }
}

