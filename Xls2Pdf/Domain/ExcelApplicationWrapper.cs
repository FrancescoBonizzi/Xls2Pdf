using Microsoft.Office.Interop.Excel;
using System;
using System.Runtime.InteropServices;

namespace Xls2Pdf.Domain
{
    public class ExcelApplicationWrapper : IDisposable
    {
        public Application ExcelApplication { get; }

        public ExcelApplicationWrapper()
        {
            ExcelApplication = new Application();
        }

        public void Dispose()
        {
            // Each file I open is locked by the background EXCEL.exe until it is quitted
            ExcelApplication.Quit();
            Marshal.ReleaseComObject(ExcelApplication);
        }
    }
}
