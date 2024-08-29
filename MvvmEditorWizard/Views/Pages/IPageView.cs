namespace MvvmEditorWizard.Views.Pages
{
  /// <summary>
  /// Base interface for all page views
  /// </summary>
  public interface IPageView
  {
    /// <summary>
    /// Gets or sets WPF Data Context
    /// </summary>
    object DataContext { get; set; }
  }
}
