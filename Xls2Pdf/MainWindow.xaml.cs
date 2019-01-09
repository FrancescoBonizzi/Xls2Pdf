using System.Windows;
using Xls2Pdf.ViewModels;

namespace Xls2Pdf
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
