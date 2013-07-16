using System;
using EventToolkit;
using Machine.Specifications;

namespace Specs
{
  public class Context : ICleanupAfterEveryContextInAssembly
  {
    public void AfterContextCleanup()
    {
      EventBus.Clear();
    }
  }
}
