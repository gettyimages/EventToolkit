using EventToolkit;

namespace Specs
{
    public class EventMessage : IEventMessage
    {
    }

    public class BaseEventMessage : IEventMessage
    {
    }

    public class DerivedEventMessage : BaseEventMessage
    {
    }

    public class SimpleSubscriber : Handles<EventMessage>
    {
        public bool handled = false;
        public bool disposed = false;

        protected override void Handle(EventMessage message)
        {
            handled = true;
        }

        public override void Dispose()
        {
            disposed = true;
        }
    }
}
