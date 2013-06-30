using System;

namespace DomainToolkit
{
  public class Event<T> : IEventMessage {
    readonly T message;

    public Event(T message) {
      this.message = message;
    }

    public T Message {
      get { return message; }
    }
  }
}
