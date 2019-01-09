using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xls2Pdf.Domain;
using Xls2Pdf.Infrastructure;

namespace Xls2Pdf.Services
{
    public class ExcelInteropExcelToPdfConverter : IExcelToPdfConverter
    {
        public async Task ConvertToPdf(IEnumerable<ExcelFileInfos> excelFilesInfos)
        {
            await Task.Run(() =>
            {
                using (var excelApplication = new ExcelApplicationWrapper())
                {
                    foreach (var excelFileInfo in excelFilesInfos)
                    {
                        var thisFileWorkbook = excelApplication.ExcelApplication.Workbooks.Open(excelFileInfo.FilePath);
                        string newPdfFilePath = Path.Combine(
                            Path.GetDirectoryName(excelFileInfo.FilePath),
                            $"{excelFileInfo.FileNameWithoutExtension}.pdf");

                        thisFileWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, newPdfFilePath);
                        thisFileWorkbook.Close(false, excelFileInfo.FilePath);
                        Marshal.ReleaseComObject(thisFileWorkbook);
                    }
                }
            });
        }
    }
}
