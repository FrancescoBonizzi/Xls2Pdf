using System.Windows;
using Xls2Pdf.Infrastructure;
using Xls2Pdf.Services;
using Xls2Pdf.ViewModels;

namespace Xls2Pdf
{
    public partial class App : Application
    {
        private IExcelToPdfConverter _excelMerger;

        void App_Startup(object sender, StartupEventArgs e)
        {
            var dialogsManager = new WPFDialogsManager();
            _excelMerger = new ExcelInteropExcelToPdfConverter();
            var mainWindowViewModel = new MainWindowViewModel(
                dialogsManager,
                _excelMerger);
            var window = new MainWindow(mainWindowViewModel);
            window.Show();
        }
    }
}
