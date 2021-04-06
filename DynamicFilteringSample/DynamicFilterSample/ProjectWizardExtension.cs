using System;
using System.Windows.Forms;
using Scada.AddIn.Contracts;

namespace DynamicFilterSample
{
  [AddInExtension(name: "Dynamic Property Sample",
    description: "This wizard extention shows the possibility to change the filter parameters of a function by its dynamic properties before executing the function.")]
  public class ProjectWizardExtension : IProjectWizardExtension
  {
    public void Run(IProject context, IBehavior behavior)
    {
      try
      {
        //This example is a bit delicate: it needs an zenon project with an AML screen and a screen switch function to that AML screen.
        var functionName = "switchToAML";
        var screenSwitchAmlFunction = context.FunctionCollection[functionName];
        if (screenSwitchAmlFunction == null)
        {
          MessageBox.Show($"No screen switch function to AML screen called '{functionName}' found! Wizard will abort.");
          return;
        }

        // Now change the dynamic properties of the screen switch function
        // For demonstration: The filter is changed to only display variables with a certain string in the name.
        var filterString = "s7tcp*";
        var variableFilterIdentifier = "PictFilter[0].VarFilter";
        screenSwitchAmlFunction.SetDynamicProperty(variableFilterIdentifier, filterString);
        screenSwitchAmlFunction.Execute();
      }
      catch (Exception e)
      {
        MessageBox.Show($"Exception occured! " + e.Message);
      }

    }
  }
}