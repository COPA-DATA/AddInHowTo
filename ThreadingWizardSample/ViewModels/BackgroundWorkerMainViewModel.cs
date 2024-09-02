using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Scada.AddIn.Contracts;

namespace SimpleWpfEditorWizard.ViewModels
{
    public class BackgroundWorkerMainViewModel : MainViewModel
    {
        private IEnumerable<VariableSummary> _loadedItems;

        public BackgroundWorkerMainViewModel(IProject project) : base(project)
        {
        }
        public override void Load()
        {
            this.BusyMessage = "Loading data using background worker...";

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.BusyMessage = "Loading of data has been canceled.";
                return;
            }

            if (_loadedItems != null)
            {
                foreach (var item in _loadedItems)
                {
                    _itemList.Add(item);
                }
            }

            this.BusyMessage = "Done";
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            var query = _project.VariableCollection.GroupBy(variable => variable.IecType)
                .Select(x => new VariableSummary { IecType = x.Key, Count = x.Count() });

            _loadedItems = query.ToArray();
        }
    }
}
