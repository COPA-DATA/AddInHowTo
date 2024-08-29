namespace MvvmEditorWizard.ViewModels.Pages
{
  /// <summary>
  /// Interface for all page view models
  /// </summary>
  public interface IPageViewModel
  {
    /// <summary>
    /// Returns the page view
    /// </summary>
    object Page { get; }

    /// <summary>
    /// Returns the title of the page
    /// </summary>
    string Title { get; }
  }
}
