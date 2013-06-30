using System;
using Machine.Specifications;
using DomainToolkit;

namespace Specs
{
  [Subject("Subscribing")]
  public class When_subscribing_to_an_event_with_a_delegate
  {
    static IEventSubscription subscription;

    Because of = () =>
      subscription = EventBus.Subscribe<Message>(_ => {});

    It should_return_a_subscription = () =>
      subscription.ShouldNotBeNull();
  }


  [Subject("Subscribing")]
  public class When_subscribing_to_an_event_with_a_subscriber {
    static IEventSubscription subscription;

    Because of = () =>
      subscription = EventBus.Subscribe<Message>(new SimpleSubscriber());

    It should_return_a_subscription = () =>
      subscription.ShouldNotBeNull();
  }

  [Subject("Subscribing")]
  public class When_subscribing_to_an_event_with_a_null_delegate_reference {
    static Exception exception;

    Because of = () =>
      exception = Catch.Exception(() => EventBus.Subscribe<Message>((Action<Message>)null));

    It should_reject_the_subscription = () =>
      exception.ShouldBeOfType(typeof(ArgumentNullException));
  }

  [Subject("Subscribing")]
  public class When_subscribing_to_an_event_with_a_null_subscriber_reference {
    static Exception exception;

    Because of = () =>
      exception = Catch.Exception(() => EventBus.Subscribe<Message>((IEventSubscriber)null));

    It should_reject_the_subscription = () =>
      exception.ShouldBeOfType(typeof(ArgumentNullException));
  }

  [Subject("Subscribing")]
  public class When_a_delegate_subscription_is_disposed {
    static bool notified;
    static IEventSubscription subscription;

    Establish context = () =>
      subscription = EventBus.Subscribe<Message>(_ => notified = true);

    Because of = () =>
      subscription.Dispose();

    It should_not_recieve_events = () => {
      EventBus.Publish(new Message());
      notified.ShouldBeFalse();
    };
  }

  [Subject("Subscribing")]
  public class When_a_subscriber_subscription_is_disposed {
    static bool notified;
    static SimpleSubscriber subscriber = new SimpleSubscriber();
    static IEventSubscription subscription;

    Establish context = () =>
      subscription = EventBus.Subscribe<Message>(subscriber);

    Because of = () =>
      subscription.Dispose();

    It should_dispose_the_subscriber = () => {
      subscriber.disposed.ShouldBeTrue();
    };
  }
}
