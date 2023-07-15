using ClosedXML.Excel;

namespace FileReporterApp.ServiceApp.FileWriter
{
    internal class ExcelFileWriter : IFileWrite
    {
        private XLWorkbook _workBook;

        public async void Write(List<FileInfo> scannedMergedList, string targetPath) => await WriteExcelFileCallback(targetPath, scannedMergedList);

        private async Task WriteExcelFileCallback(string targetPath, List<FileInfo> scannedMergedList)
        {
            try
            {
                _workBook = new XLWorkbook();
                var workSheet = _workBook.AddWorksheet("file_rport");

                PrepareTitles(workSheet);

                if (scannedMergedList != null)
                {
                    for (int i = 0, j = 2; i < scannedMergedList.Count; ++i, j++)
                    {
                        var filePathCell = workSheet.Cell("A" + j);
                        filePathCell.Value = scannedMergedList[i].FullName;
                        filePathCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                        filePathCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        var creationDateCell = workSheet.Cell("B" + j);
                        creationDateCell.Value = scannedMergedList[i].CreationTime.ToString("dd.MM.yyyy hh:mm:ss");
                        creationDateCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        creationDateCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        var lastModifiedDateCell = workSheet.Cell("C" + j);
                        lastModifiedDateCell.Value = scannedMergedList[i].LastWriteTime.ToString("dd.MM.yyyy hh:mm:ss");
                        lastModifiedDateCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        lastModifiedDateCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        var lastAccessTimeCell = workSheet.Cell("D" + j);
                        lastAccessTimeCell.Value = scannedMergedList[i].LastAccessTime.ToString("dd.MM.yyyy hh:mm:ss");
                        lastAccessTimeCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        lastAccessTimeCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        var lengthCell = workSheet.Cell("E" + j);
                        lengthCell.Value = scannedMergedList[i].Length;
                        lengthCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        lengthCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                    workSheet.Columns().AdjustToContents();
                    _workBook.SaveAs(targetPath);


                }
            }
            catch (FileNotFoundException ex) { }
            finally
            {


            }
        }

        private void PrepareTitles(IXLWorksheet workSheet)
        {
            var titleCells = new string[2, 5] { { "A1", "B1", "C1", "D1", "E1" },
                { "File Path","File Created Date", "File Modified Date", "File Accessed Date", "File Size (bytes)" } };

            for (int i = 0; i < 5; i++)
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
