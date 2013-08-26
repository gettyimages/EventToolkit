
namespace EventToolkit
{
    static class EventCore
    {
        internal static readonly GlobalEventBus CoreBus = new GlobalEventBus();

        public static ScopedEventBus CreateScope()
        {
            return new ScopedEventBus(CoreBus);
        }
    }
}