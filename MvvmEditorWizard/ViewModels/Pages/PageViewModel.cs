using MvvmEditorWizard.Views.Pages;

namespace MvvmEditorWizard.ViewModels.Pages
{
  /// <summary>
  /// Represents the base view model for all pages
  /// </summary>
  /// <typeparam name="TPageView">Type of the view</typeparam>
  public abstract class PageViewModel<TPageView> : DataModel, IPageViewModel where TPageView : IPageView, new()
  {
    private TPageView _pageView;

    /// <summary>
    /// Returns the page view
    /// </summary>
    public virtual object Page
    {
      get
      {
        if (_pageView == null)
        {
          _pageView = new TPageView { DataContext = this };
          PrepareData();
        }

        return _pageView;
      }
    }

    /// <summary>
    /// Returns the title of the page
    /// </summary>
    public abstract string Title { get; }



    protected virtual void PrepareData()
    {
      
    }
  }
}
