using EventToolkit;

namespace Specs
{
  public class Message : IEventMessage
  {
  }

  public class BaseEventMessage : IEventMessage
  {
  }

  public class DerivedEventMessage : BaseEventMessage
  {
  }

  public class SimpleSubscriber : Handles<Message> {
    public bool handled = false;
    public bool disposed = false;

    protected override void Handle(Message message)
    {
      handled = true;
    }

    public override void Dispose() {
      disposed = true;
    }
  }
}
