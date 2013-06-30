using System;

namespace DomainToolkit
{
  public interface IEventSubscriber : IDisposable
  {
    void Handle(IEventMessage message);
  }
}
