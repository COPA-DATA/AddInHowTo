using System;
using System.Windows;
using SimpleWpfEditorWizard.ViewModels;

namespace SimpleWpfEditorWizard.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            var vm = (MainViewModel)DataContext;
            vm.Load();
        }
    }
}
