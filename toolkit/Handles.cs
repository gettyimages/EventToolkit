namespace EventToolkit
{
    public abstract class Handles<TMessage> : IEventSubscriber
      where TMessage : IEventMessage
    {
        public virtual void Dispose()
        {
        }

        protected abstract void Handle(TMessage message);

        void IEventSubscriber.Handle(IEventMessage message)
        {
            Handle((TMessage)message);
        }
    }
}
