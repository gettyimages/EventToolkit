using EventToolkit;
using Kekiri;
using FluentAssertions;

namespace Specs
{
    [Scenario("Scopes")]
    public class When_raising_an_event_at_the_local_scope : EventSpec
    {
        protected ScopedEventBus localBus;
        protected GlobalEventBus globalBus;
        protected SimpleSubscriber nestedSubscriber = new SimpleSubscriber();
        protected SimpleSubscriber outerSubscriber = new SimpleSubscriber();

        [Given]
        public void given_a_handler_subscribed_at_an_outer_scope()
        {
            globalBus = new GlobalEventBus();
            globalBus.Subscribe<EventMessage>(outerSubscriber);
        }

        [Given]
        public void given_a_handler_subscribed_at_the_local_scope()
        {
            localBus = globalBus.CreateScope();
            localBus.Subscribe<EventMessage>(nestedSubscriber);
        }

        [When]
        public void when()
        {
            localBus.Publish(new EventMessage());
        }

        [Then]
        public void it_sends_an_event_to_handlers_at_the_nested_scope()
        {
            nestedSubscriber.handled.Should().BeTrue();
        }

        [Then]
        public void it_sends_an_event_to_handlers_at_the_outer_scope()
        {
            outerSubscriber.handled.Should().BeTrue();
        }
    }
}

