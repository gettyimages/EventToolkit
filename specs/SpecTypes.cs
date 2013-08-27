using EventToolkit;

namespace Specs
{
    public class Event : IEvent
    {
    }

    public class BaseEvent : IEvent
    {
    }

    public class DerivedEvent : BaseEvent
    {
    }

    public class SimpleSubscriber : Handles<Event>
    {
        public bool handled = false;
        public bool disposed = false;

        protected override void Handle(Event eventMessage)
        {
            handled = true;
        }

        public override void Dispose()
        {
            disposed = true;
        }
    }
}
