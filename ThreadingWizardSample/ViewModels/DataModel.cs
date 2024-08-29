using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleWpfEditorWizard.ViewModels
{
    /// <summary>
    /// Base class for all data models that uses IPropertyChanged
    /// </summary>
    public abstract class DataModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected DataModel()
        {
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="property">Name of property</param>
        protected void OnPropertyChanged([CallerMemberName] string property = null)
        {
            var changed = PropertyChanged;
            changed?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}