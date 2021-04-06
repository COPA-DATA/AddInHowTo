using System;
using System.Collections.Generic;
using System.Windows;
using MvvmEditorWizard.ViewModels;
using MvvmEditorWizard.ViewModels.Pages;
using MvvmEditorWizard.Views;
using MvvmEditorWizard.Views.Pages;
using Scada.AddIn.Contracts;

namespace MvvmEditorWizard
{
  /// <summary>
  /// Description of Editor Wizard Extension.
  /// </summary>
  [AddInExtension("WPF MVVM Sample", "A WPF Wizard that uses the MVVM pattern", "Samples")]
  public class EditorWizardExtension : IEditorWizardExtension
  {
    #region IEditorWizardExtension implementation

    /// <summary>
    /// Method which is executed on starting the SCADA Editor Wizard
    /// </summary>
    /// <param name="context">SCADA editor application object</param>
    /// <param name="behavior">For future use</param>
    public void Run(IEditorApplication context, IBehavior behavior)
    {
      if (context.Workspace.ActiveProject == null)
      {
        MessageBox.Show(string.Format("There is no active project available." + Environment.NewLine +
                                      "Please load a project into the workspace!")
                                      , "Wizard with WPF GUI");
        return;
      }

      try
      {
        var pageViewModels = new List<IPageViewModel>
        {
          new WelcomeViewModel<WelcomePage>(),
          new SelectionListViewModel<SelectionListPage>(context.Workspace.ActiveProject)
        };

        var mainViewModel = new MainViewModel(pageViewModels);
        var mainView = new MainView 
        { 
          DataContext = mainViewModel 
        };

        var application = new Application();
        application.MainWindow = mainView;
        mainView.Show();
        application.Run();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"An exception has been thrown: {ex.Message}", "Wizard with WPF GUI");
        throw;
      }
    }

    #endregion
  }

}