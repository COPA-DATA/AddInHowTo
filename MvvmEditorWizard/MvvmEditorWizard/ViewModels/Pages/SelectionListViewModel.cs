using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmEditorWizard.Views.Pages;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace MvvmEditorWizard.ViewModels.Pages
{
    /// <summary>
    /// Represents the screen selection view model
    /// </summary>
    /// <typeparam name="TView">The type of selection list view</typeparam>
    public class SelectionListViewModel<TView> : PageViewModel<TView>
      where TView : ISelectionListPage, new()
    {
        private readonly IProject _project;
        private IEnumerable<IDataType> _itemList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="project">The active project.</param>
        public SelectionListViewModel(IProject project)
        {
            _project = project;
        }

        protected override void PrepareData()
        {
            IEnumerable<IDataType> itemList = null;
            Task.Factory.StartNew(() =>
            {
                var query = _project.DataTypeCollection;

                // Execute
                itemList = query.ToArray();

            }).ContinueWith(task =>
            {
                ItemList = itemList;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Returns a list of screens
        /// </summary>
        public IEnumerable<IDataType> ItemList
        {
            get { return _itemList; }
            set
            {
                if (!Equals(_itemList, value))
                {
                    _itemList = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Returns title of the page
        /// </summary>
        public override string Title => Strings.Wizard_SelectionListPage_Title;
    }
}
