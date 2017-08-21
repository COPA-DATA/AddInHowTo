using System.Collections.Generic;
using System.Collections.ObjectModel;
using Scada.AddIn.Contracts;

namespace SimpleWpfEditorWizard.ViewModels
{
    public abstract class MainViewModel : DataModel
    {
        protected readonly ObservableCollection<VariableSummary> _itemList;
        protected readonly IProject _project;
        private string _busyMessage;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="project">The active project.</param>
        protected MainViewModel(IProject project)
        {
            _project = project;

            _itemList = new ObservableCollection<VariableSummary>();
            ItemList = new ReadOnlyObservableCollection<VariableSummary>(_itemList);
        }

        /// <summary>
        /// Loads the data
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Returns a list of items
        /// </summary>
        public ReadOnlyObservableCollection<VariableSummary> ItemList { get; }

        public string BusyMessage
        {
            get
            {
                return _busyMessage;
            }
            set
            {
                if (_busyMessage != value)
                {
                    _busyMessage = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
