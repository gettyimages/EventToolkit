using System;
using EventToolkit;
using Machine.Specifications;

namespace Specs
{
  [Subject("Scopes")]
  public class When_using_a_nested_scope {
    protected static ScopedEventBus localBus;
    protected static GlobalEventBus globalBus = new GlobalEventBus();
    protected static SimpleSubscriber nestedSubscriber = new SimpleSubscriber();
    protected static SimpleSubscriber outerSubscriber = new SimpleSubscriber();

    Establish context = () => {
      globalBus.Subscribe<Message>(outerSubscriber);
      localBus = globalBus.CreateScope();
    };

    Because of = () => {
      using (localBus.Subscribe<Message>(nestedSubscriber))
        localBus.Publish(new Message());
    };

    It should_send_an_event_to_handlers_subscribing_to_the_nested_scope = () => nestedSubscriber.handled.ShouldBeTrue();
    It should_dispose_handlers_subscribing_to_the_nested_scope = () => nestedSubscriber.disposed.ShouldBeTrue();

    It should_send_an_event_to_handlers_subscribing_to_the_outer_scope = () => outerSubscriber.handled.ShouldBeTrue();
    It should_not_dispose_handlers_subscribing_to_the_outer_scope = () => outerSubscriber.disposed.ShouldBeFalse();
  }
}

