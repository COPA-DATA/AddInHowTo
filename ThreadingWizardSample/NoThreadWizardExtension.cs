using System;
using System.Windows;
using Scada.AddIn.Contracts;
using SimpleWpfEditorWizard.ViewModels;

namespace SimpleWpfEditorWizard
{
    [AddInExtension("DataTypes No Thread", "Loads Variables using a no thread", "Samples")]
    public class NoThreadWizardExtension : IEditorWizardExtension
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
                var mainWindow = new Views.MainWindow
                {
                    DataContext = new NoThreadMainViewModel(context.Workspace.ActiveProject)
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