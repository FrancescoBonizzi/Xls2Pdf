using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using Xls2Pdf.Infrastructure;

namespace Xls2Pdf.Services
{
    public class WPFDialogsManager : IDialogsManager
    {
        public void ShowError(string message)
            => MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        public void ShowInformation(string message)
            => MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

        public IEnumerable<string> SelectExcelFilesToConvertToPdf()
        {
            var openFilesDialog = new OpenFileDialog()
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                Multiselect = true
            };

            if (openFilesDialog.ShowDialog() == true)
            {
                return openFilesDialog.FileNames;
            }

            return null;
        }
    }
}
