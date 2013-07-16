namespace EventToolkit
{
  public class Event<TMessage> : IEventMessage {
      readonly TMessage message;

    public Event(TMessage message) {
      this.message = message;
    }

    public TMessage Message {
      get { return message; }
    }
  }
}
