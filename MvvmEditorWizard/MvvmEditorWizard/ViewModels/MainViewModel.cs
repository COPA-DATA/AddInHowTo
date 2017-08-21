using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmEditorWizard.ViewModels.Pages;

namespace MvvmEditorWizard.ViewModels
{
  /// <summary>
  /// Represents the view model for main view
  /// </summary>
  public class MainViewModel : DataModel
  {
    private IPageViewModel _currentPageModel;
    private readonly IList<IPageViewModel> _pageViewModels;
    private int _pageIndex;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pageViewModels">List of page view models that are used in wizard</param>
    public MainViewModel(IList<IPageViewModel> pageViewModels)
    {
      _pageViewModels = pageViewModels;
      _pageIndex = -1;
      if (PageViewModels.Any())
      {
        CurrentPageViewModel = PageViewModels.First();
        _pageIndex = 0;
      }
      Backward = new ActionCommand(DoBackward, CanBackward);
      Forward = new ActionCommand(DoForward, CanForward);
    }

    /// <summary>
    /// Gets or sets the current visible page view model
    /// </summary>
    public IPageViewModel CurrentPageViewModel
    {
      get { return _currentPageModel; }
      set
      {
        _currentPageModel = value;
        OnPropertyChanged();
        _pageIndex = PageViewModels.IndexOf(value);
      }
    }

    /// <summary>
    /// Returns a list of available page view models
    /// </summary>
    public IList<IPageViewModel> PageViewModels
    {
      get { return _pageViewModels; }
    }

    /// <summary>
    /// Returns the backward command
    /// </summary>
    public ICommand Backward { get; private set; }

    /// <summary>
    /// Returns the forward command
    /// </summary>
    public ICommand Forward { get; private set; }

    private bool CanBackward(object param)
    {
      return !Equals(CurrentPageViewModel, PageViewModels.First());
    }

    private void DoBackward(object param)
    {
      CurrentPageViewModel = PageViewModels.ElementAt(--_pageIndex);
    }

    private bool CanForward(object param)
    {
      return !Equals(CurrentPageViewModel, PageViewModels.Last());
    }

    private void DoForward(object param)
    {
      CurrentPageViewModel = PageViewModels.ElementAt(++_pageIndex);
    }
  }
}