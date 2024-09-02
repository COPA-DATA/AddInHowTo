using System;
using System.Reflection;
using NLog;
using Scada.AddIn.Contracts;

namespace ClickUpDown
{
  /// <summary>
  /// Description of Project Service Extension.
  /// </summary>
  [AddInExtension("ClickUpDown Service", "This service extension reacts on click up or click down events on screen elements.")]
  public class ProjectServiceExtension : IProjectServiceExtension
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private IProject project;

    #region IProjectServiceExtension implementation

    public void Start(IProject context, IBehavior behavior)
    {
      

      project = context;

      if(context == null)
      {
        Logger.Error($"no project available. Service '{Assembly.GetExecutingAssembly().GetName()}' cannot be started.");
        return;
      }

      context.ScreenCollection.ElementLeftButtonDoubleClick += ScreenCollection_ElementLeftButtonDoubleClick;
      context.ScreenCollection.ElementLeftButtonDown += ScreenCollection_ElementLeftButtonDown;
      context.ScreenCollection.ElementLeftButtonUp += ScreenCollection_ElementLeftButtonUp;
      context.ScreenCollection.ElementMouseOver += ScreenCollection_ElementMouseOver;
      context.ScreenCollection.ElementRightButtonDoubleClick += ScreenCollection_ElementRightButtonDoubleClick;
      context.ScreenCollection.ElementRightButtonDown += ScreenCollection_ElementRightButtonDown;
      context.ScreenCollection.ElementRightButtonUp += ScreenCollection_ElementRightButtonUp;

      Logger.Info(DateTime.Now + "\tService start!");
    }

    private void ScreenCollection_ElementRightButtonUp(object sender, Scada.AddIn.Contracts.Screen.ElementRightButtonUpEventArgs e)
    {
      Logger.Info(DateTime.Now + $"\tRIGHT button UP on element: '{e.Element.Name}'");
    }

    private void ScreenCollection_ElementRightButtonDown(object sender, Scada.AddIn.Contracts.Screen.ElementRightButtonDownEventArgs e)
    {
      Logger.Info(DateTime.Now + $"\tRIGHT button DOWN on element: '{e.Element.Name}'");
    }

    private void ScreenCollection_ElementRightButtonDoubleClick(object sender, Scada.AddIn.Contracts.Screen.ElementRightButtonDoubleClickEventArgs e)
    {
      Logger.Info(DateTime.Now + $"\tRIGHT button DOUBLE click on element: '{e.Element.Name}'");
    }

    private void ScreenCollection_ElementLeftButtonDoubleClick(object sender, Scada.AddIn.Contracts.Screen.ElementLeftButtonDoubleClickEventArgs e)
    {
      Logger.Info(DateTime.Now + $"\tLEFT button DOUBLE click on element: '{e.Element.Name}'");
    }

    private void ScreenCollection_ElementMouseOver(object sender, Scada.AddIn.Contracts.Screen.ElementMouseOverEventArgs e)
    {
      Logger.Info(DateTime.Now + $"\tMOUSE OVER on element: '{e.Element.Name}'");
    }

    private void ScreenCollection_ElementLeftButtonUp(object sender, Scada.AddIn.Contracts.Screen.ElementLeftButtonUpEventArgs e)
    {
      Logger.Info(DateTime.Now + $"\tLEFT button UP click on element: '{e.Element.Name}'");
    }

    private void ScreenCollection_ElementLeftButtonDown(object sender, Scada.AddIn.Contracts.Screen.ElementLeftButtonDownEventArgs e)
    {
      Logger.Info(DateTime.Now + $"\tLEFT button DOWN click on element: '{e.Element.Name}'");
    }

    public void Stop()
    {
      // enter your code which should be executed when stopping the SCADA Runtime Service      context.ScreenCollection.ElementLeftButtonDoubleClick += ScreenCollection_ElementLeftButtonDoubleClick;
      project.ScreenCollection.ElementLeftButtonDown -= ScreenCollection_ElementLeftButtonDown;
      project.ScreenCollection.ElementLeftButtonUp -= ScreenCollection_ElementLeftButtonUp;
      project.ScreenCollection.ElementMouseOver -= ScreenCollection_ElementMouseOver;
      project.ScreenCollection.ElementRightButtonDoubleClick -= ScreenCollection_ElementRightButtonDoubleClick;
      project.ScreenCollection.ElementRightButtonDown -= ScreenCollection_ElementRightButtonDown;
      project.ScreenCollection.ElementRightButtonUp -= ScreenCollection_ElementRightButtonUp;

      Logger.Info("Service stop at: " + DateTime.Now);
    }

    #endregion
  }
}