using EventToolkit;
using Kekiri;

namespace Specs
{
    public class EventSpec : ScenarioTest
    {
        [NUnit.Framework.TestFixtureTearDown]
        public void Teardown()
        {
            EventBus.Clear();
        }
    }
}
