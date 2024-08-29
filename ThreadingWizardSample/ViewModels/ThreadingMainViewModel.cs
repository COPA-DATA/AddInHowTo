using System;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using Scada.AddIn.Contracts;
using System.Windows;

namespace SimpleWpfEditorWizard.ViewModels
{
    public class ThreadingMainViewModel : MainViewModel
    {
        public ThreadingMainViewModel(IProject project) : base(project)
        {
        }

        public override void Load()
        {
            this.BusyMessage = "Loading data using Thread...";

            var thread = new Thread(RunThread);
            thread.Start();
        }

        private void RunThread()
        {

            var query = _project.VariableCollection.GroupBy(variable => variable.IecType)
                .Select(x => new VariableSummary { IecType = x.Key, Count = x.Count() });

            var itemList = query.ToArray();

            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    foreach (var item in itemList)
                    {
                        _itemList.Add(item);
                    }

                    this.BusyMessage = "Done";
                }));
        }
    }
}
