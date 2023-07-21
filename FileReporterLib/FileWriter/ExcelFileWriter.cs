using ClosedXML.Excel;
using static FileReporterDecorator.Util.ExceptionUtil;
namespace FileReporterLib.FileWriter
{
    internal class ExcelFileWriter : IFileWrite
    {
        private XLWorkbook _workBook;

        public async void Write(List<FileInfo> newFileList, List<FileInfo> oldFileList, string targetPath) => await WriteExcelFileCallback(targetPath, newFileList, oldFileList);


        private void WriteExcel(List<FileInfo> newFileList, List<FileInfo> oldFileList)
        {
            _workBook = new XLWorkbook();
            var workSheet = _workBook.AddWorksheet("new_file_report");
            var oldWorkSheet = _workBook.AddWorksheet("old_file_report");

            PrepareTitles(workSheet);
            PrepareTitles(oldWorkSheet);

            writeListToExcel(newFileList, workSheet, "New");
            writeListToExcel(oldFileList, oldWorkSheet, "Old");
        }
        private async Task WriteExcelFileCallback(string targetPath, List<FileInfo> newFileList, List<FileInfo> oldFileList)
        {
            ThrowFileNotFoundException(() => WriteExcel(newFileList, oldFileList), () => _workBook.SaveAs(targetPath));
        }

        private void writeListToExcel(List<FileInfo> fileList, IXLWorksheet workSheet, string fileStatus)
        {
            if (fileList != null)
            {
                for (int i = 0, j = 2; i < fileList.Count; ++i, j++)
                {
                    CreateCell(workSheet.Cell("A" + j), fileList[i].FullName, XLAlignmentHorizontalValues.Justify, XLAlignmentVerticalValues.Center);
                    CreateCell(workSheet.Cell("B" + j), fileList[i].CreationTime.ToString("dd.MM.yyyy hh:mm:ss"), XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center);
                    CreateCell(workSheet.Cell("C" + j), fileList[i].LastWriteTime.ToString("dd.MM.yyyy hh:mm:ss"), XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center);
                    CreateCell(workSheet.Cell("D" + j), fileList[i].LastAccessTime.ToString("dd.MM.yyyy hh:mm:ss"), XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center);
                    CreateCell(workSheet.Cell("E" + j), fileList[i].Length.ToString(), XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center);
                    CreateCell(workSheet.Cell("F" + j), fileStatus, XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center);
                }

                workSheet.Columns().AdjustToContents();
            }
        }

        private void CreateCell(IXLCell cell, string cellValue, XLAlignmentHorizontalValues horizontalAlignment, XLAlignmentVerticalValues verticalAlignment)
        {
            var excelCell = cell;
            excelCell.Value = cellValue;
            excelCell.Style.Alignment.Horizontal = horizontalAlignment;
            excelCell.Style.Alignment.Vertical = verticalAlignment;
        }

        private void PrepareTitles(IXLWorksheet workSheet)
        {
            var titleCells = new string[2, 6] { { "A1", "B1", "C1", "D1", "E1", "F1" },
                { "File Path","File Created Date", "File Modified Date", "File Accessed Date", "File Size (bytes)", "FileStatus" } };

            for (int i = 0; i < 6; i++)
            {
                var cell = workSheet.Cell(titleCells[0, i]);
                cell.Value = titleCells[1, i];

                cell.Style.Font.Bold = true;
                cell.Style.Alignment.WrapText = true;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }
        }
    }
}