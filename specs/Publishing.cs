using System;
using Machine.Specifications;
using DomainToolkit;

namespace Specs
{
  [Subject("Publishing")]
  public class When_publishing_an_event
  {
    static bool notified = false;

    Establish context = () => {
      EventBus.Subscribe<Message>(_ => notified = true);
    };

    Because of = () => EventBus.Publish(new Message());

    It should_notify_the_subscribers = () =>
      notified.ShouldBeTrue();
  }

  [Subject("Publishing")]
  public class When_publishing_an_event_with_multiple_subscribers {
    static string notified = "";

    Establish context = () => {
      EventBus.Subscribe<Message>(_ => notified += "a");
      EventBus.Subscribe<Message>(_ => notified += "b");
      EventBus.Subscribe<Message>(_ => notified += "c");
    };

    Because of = () => EventBus.Publish(new Message());

    It should_notify_the_subscribers = () =>
      notified.ShouldEqual("abc");
  }

  [Subject("Publishing")]
  public class When_publishing_an_event_of_a_derived_type {
    static bool base_notification;
    static bool derived_notification;

    Establish context = () => {
      EventBus.Subscribe<BaseEventMessage>(_ => base_notification = true);
      EventBus.Subscribe<DerivedEventMessage>(_ => derived_notification = true);
    };

    Because of = () => EventBus.Publish(new DerivedEventMessage());

    It should_notify_the_subscribers_of_the_base_type = () => base_notification.ShouldBeTrue();
    It should_notify_the_subscribers_of_the_derived_type = () => derived_notification.ShouldBeTrue();
  }

  [Subject("Publishing")]
  public class When_publishing_an_event_of_a_base_type {
    static bool base_notification;
    static bool derived_notification;

    Establish context = () => {
      EventBus.Subscribe<BaseEventMessage>(_ => base_notification = true);
      EventBus.Subscribe<DerivedEventMessage>(_ => derived_notification = true);
    };

    Because of = () => EventBus.Publish(new BaseEventMessage());

    It should_notify_the_subscribers_of_the_base_type = () => base_notification.ShouldBeTrue();
    It should_not_notify_the_subscribers_of_a_derived_type = () => derived_notification.ShouldBeFalse();
  }
}
