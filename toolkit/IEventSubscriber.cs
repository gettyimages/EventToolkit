using System;

namespace EventToolkit
{
    public interface IEventSubscriber : IDisposable
    {
        void Handle(IEvent message);
    }
}
