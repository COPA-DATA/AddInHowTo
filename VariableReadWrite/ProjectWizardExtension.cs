using System;
using System.Linq;
using System.Windows.Forms;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace VariableReadWrite
{
  /// <summary>
  /// This wizard demonstrates that only variables with 'Write Set Value' (or Dynamic Property 'InOut') can be written by API
  /// </summary>
  [AddInExtension("Variable ReadOnly vs ReadWrite", "This wizard shows an example between read only access variables and read write access variables.")]
  public class ProjectWizardExtension : IProjectWizardExtension
  {
    private string OnlineContainerName => "myOnlineContainer" + Guid.NewGuid();
    private readonly string[] VariableNames = {"ReadAndWrite", "ReadOnly"};
    private const int ValuePosition = 0;

    public void Run(IProject context, IBehavior behavior)
    {
      # region Parameter validation
      foreach (var variable in VariableNames)
      {
        if(context.VariableCollection[variable] == null)
        {
          MessageBox.Show($"This wizard is for demonstration purposes only!\n\n " +
            $"It needs a zenon project with a variable named '{VariableNames[0]}' and '{VariableNames[1]}'. " +
            "one of the variables should have the 'Write set value' option active and one deactivated. " +
            "An example zenon project (backup) is in the ressources of this source code. ");
        }
      }
      #endregion

      #region Make sure, that both variables are advised.
      context.OnlineVariableContainerCollection.Delete(OnlineContainerName);
      var onlineContainer = context.OnlineVariableContainerCollection.Create(OnlineContainerName);
      onlineContainer.AddVariable(VariableNames);
      onlineContainer.Activate();
      #endregion

      ShowVariableValuesInMessageBox(context);

      MessageBox.Show("The wizard tries to set the value of both variables to 42!");

      var variables = context.VariableCollection.Where(v => VariableNames.Contains(v.Name));
      foreach (var variable in variables)
      {
        if(IsReadOnly(variable))
        {
          MessageBox.Show("Variable: '" + variable.Name + "' is not suited for value modification! \n" +
            "Add-In tries to write the value anyway!");
        }
        variable.SetValue(ValuePosition,42);
      }

      ShowVariableValuesInMessageBox(context);

    }

    private bool IsReadOnly(IVariable variable)
    {
      return !IsReadWrite(variable);
    }

    private bool IsReadWrite(IVariable variable)
    {
      return (bool)variable.GetDynamicProperty("InOut");
    }

    private void ShowVariableValuesInMessageBox(IProject context)
    {
      var variables = context.VariableCollection.Where(v => VariableNames.Contains(v.Name));
      var message = "The value of the variables is:\n";
      foreach (var variable in variables)
      {
        message += variable.Name + ": " + variable.GetValue(ValuePosition) + "\n";
      }
      MessageBox.Show(message);
    }
  }

}