using System;

namespace EventToolkit 
{
  public interface IEventBus {
    IEventSubscription Subscribe<TMessage>(Action<TMessage> handler)
      where TMessage : IEventMessage;

    IEventSubscription Subscribe<TMessage>(IEventSubscriber subscriber)
      where TMessage : IEventMessage;

    void Publish<TMessage>(TMessage message)
      where TMessage : IEventMessage;
  }
}
