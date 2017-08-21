using System;
using System.Windows;
using Scada.AddIn.Contracts;
using SimpleWpfEditorWizard.ViewModels;

namespace SimpleWpfEditorWizard
{
    [AddInExtension("DataTypes TPL", "Loads data type statistics using Task Parallel Library", "Samples")]
    public class TplWizardExtension : IEditorWizardExtension
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
                var vm = new TplMainViewModel(context.Workspace.ActiveProject);
                var mainWindow = new Views.MainWindow
                {
                    DataContext = vm
                };
                application.MainWindow = mainWindow;
                mainWindow.Show();
                application.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An exception has been thrown: {ex.Message}");
            }
        }

        #endregion
    }

}