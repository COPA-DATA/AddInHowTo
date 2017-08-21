using System.Windows;
using System.Windows.Controls;
using WpfElementCommunicationSample.ViewModel;

namespace WpfElementCommunicationSample.View
{
    /// <summary>
    /// Interaction logic for SampleControl.xaml
    /// </summary>
    public partial class SampleControl : UserControl
    {
        public SampleControl()
        {
            InitializeComponent();
        }

        private void SampleControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (SampleViewModel)RootLayout.DataContext;
            viewModel.Load();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (SampleViewModel)RootLayout.DataContext;
            viewModel.Load();
        }
    }
}
