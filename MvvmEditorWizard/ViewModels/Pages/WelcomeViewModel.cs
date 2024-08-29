using MvvmEditorWizard.Views.Pages;

namespace MvvmEditorWizard.ViewModels.Pages
{
  /// <summary>
  /// Represents the welcome page view model
  /// </summary>
  /// <typeparam name="TView">The type of welcome list view</typeparam>
  public class WelcomeViewModel<TView> : PageViewModel<TView>
    where TView : IWelcomePage, new()
  {

    /// <summary>
    /// Returns title of the page
    /// </summary>
    public override string Title { get { return Strings.Wizard_WelcomePage_Title; } }
  }
}
