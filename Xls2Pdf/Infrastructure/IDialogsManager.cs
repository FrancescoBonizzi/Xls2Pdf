using System.Collections.Generic;

namespace Xls2Pdf.Infrastructure
{
    public interface IDialogsManager
    {
        IEnumerable<string> SelectExcelFilesToConvertToPdf();
        void ShowInformation(string message);
        void ShowError(string message);
    }
}
