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

        public void Run(IEditorApplication context, IBehavior behavior)
        {
            if (context.Workspace.ActiveProject == null)
            {
                MessageBox.Show("No project is available. Please active a project.");
                return;
            }

            try
            {
                var application = new Application();

                var pageViewModels = new List<IPageViewModel>
                {
                    new WelcomeViewModel<WelcomePage>(),
                    new SelectionListViewModel<SelectionListPage>(context.Workspace.ActiveProject)
                };

                var mainView = new MainView { DataContext = new MainViewModel(pageViewModels) };
                application.MainWindow = mainView;
                mainView.Show();
                application.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An exception has been thrown: {ex.Message}");
                throw;
            }
        }

        #endregion
    }

}