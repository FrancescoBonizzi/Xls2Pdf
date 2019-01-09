using System;
using System.IO;

namespace Xls2Pdf.Domain
{
    public class ExcelFileInfos
    {
        public string FileNameWithoutExtension { get; set; }
        public string FilePath { get; set; }

        public ExcelFileInfos(string filePath)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

            FilePath = Path.GetFullPath(filePath);
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
        }
    }
}
