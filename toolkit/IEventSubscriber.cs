using System;

namespace EventToolkit
{
  public interface IEventSubscriber : IDisposable
  {
    void Handle(IEventMessage message);
  }
}
