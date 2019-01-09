using System.Collections.Generic;
using System.Threading.Tasks;
using Xls2Pdf.Domain;

namespace Xls2Pdf.Infrastructure
{
    public interface IExcelToPdfConverter
    {
        Task ConvertToPdf(IEnumerable<ExcelFileInfos> excelFileInfos);
    }
}
