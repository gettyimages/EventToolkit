namespace EventToolkit
{
    public abstract class Handles<TEvent> : IEventSubscriber
      where TEvent : IEvent
    {
        public virtual void Dispose()
        {
        }

        protected abstract void Handle(TEvent eventMessage);

        void IEventSubscriber.Handle(IEvent eventMessage)
        {
            Handle((TEvent)eventMessage);
        }
    }
}
