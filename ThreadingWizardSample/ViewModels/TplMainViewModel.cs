using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Scada.AddIn.Contracts;

namespace SimpleWpfEditorWizard.ViewModels
{
    public class TplMainViewModel : MainViewModel
    {
        public TplMainViewModel(IProject project) : base(project)
        {

        }

        public override void Load()
        {
            this.BusyMessage = "Loading data using TPL...";

            Task.Factory.StartNew(() =>
            {
                // Type 1 - programatically syntax
                var query = _project.VariableCollection.GroupBy(variable => variable.IecType)
                    .Select(x => new VariableSummary { IecType = x.Key, Count = x.Count() });


                // Type 2 - SQL like syntax
                //var query = from variable in _project.VariableCollection
                //            group variable by variable.IecType into g
                //            select new VariableSummary { Count = g.Count(), IecType = g.Key };

                // Execute
                return query.ToArray();

            }).ContinueWith(task =>
            {
                foreach (var item in task.Result)
                {
                    _itemList.Add(item);
                }

                this.BusyMessage = "Done";
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
