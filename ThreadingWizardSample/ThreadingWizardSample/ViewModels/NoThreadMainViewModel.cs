using System.Linq;
using System.Threading;
using Scada.AddIn.Contracts;

namespace SimpleWpfEditorWizard.ViewModels
{
    public class NoThreadMainViewModel : MainViewModel
    {
        public NoThreadMainViewModel(IProject project) : base(project)
        {
        }

        public override void Load()
        {
            this.BusyMessage = "Loading data using no thread...";

            var query = _project.VariableCollection.GroupBy(variable => variable.IecType)
                .Select(x => new VariableSummary { IecType = x.Key, Count = x.Count() });

            foreach (var item in query.ToArray())
            {
                _itemList.Add(item);
            }

            this.BusyMessage = "Done";
        }
    }
}
