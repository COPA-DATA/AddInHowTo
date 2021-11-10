using Scada.AddIn.Contracts;
using System;

namespace TactileRecognition
{
  [AddInExtension("Tactile Recognition", "This service registers to all sorts of tactile (touch) events.")]
  public class ProjectServiceExtension : IProjectServiceExtension
  {
    #region IProjectServiceExtension implementation

    public void Start(IProject context, IBehavior behavior)
    {
      // enter your code which should be executed when starting the service for the SCADA Service Engine

    }

    public void Stop()
    {
      // enter your code which should be executed when stopping the service for the SCADA Service Engine
    }

    #endregion
  }
}