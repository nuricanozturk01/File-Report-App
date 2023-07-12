using IronXL;
namespace FileReporterApp.ServiceApp.FileWriter
{
    internal class ExcelFileWriter : IFileWrite
    {
        private WorkBook _workBook;
        public ExcelFileWriter()
        {

        }

        public void Write(List<FileInfo> scannedMergedList, string targetPath)
        {
            _workBook = WorkBook.Load(targetPath);
            WorkSheet workSheet = _workBook.DefaultWorkSheet;
            LinkedList<FileInfo> list = new LinkedList<FileInfo>();

            prepareTitles(workSheet);

            for (int i = 0, j = 2; i < scannedMergedList.Count; ++i, j++)
            {
                workSheet["A" + j].Value = scannedMergedList[i].FullName;
                workSheet["A" + j].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Justify;
                workSheet["A" + j].Style.VerticalAlignment = IronXL.Styles.VerticalAlignment.Center;

                workSheet["B" + j].Value = scannedMergedList[i].CreationTime.ToString("dd.MM.yyyy hh:mm:ss");
                workSheet["B" + j].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;
                workSheet["B" + j].Style.VerticalAlignment = IronXL.Styles.VerticalAlignment.Center;


                workSheet["C" + j].Value = scannedMergedList[i].LastWriteTime.ToString("dd.MM.yyyy hh:mm:ss");
                workSheet["C" + j].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;
                workSheet["C" + j].Style.VerticalAlignment = IronXL.Styles.VerticalAlignment.Center;

                workSheet["D" + j].Value = scannedMergedList[i].LastAccessTime.ToString("dd.MM.yyyy hh:mm:ss");
                workSheet["D" + j].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;
                workSheet["D" + j].Style.VerticalAlignment = IronXL.Styles.VerticalAlignment.Center;

                workSheet["E" + j].Value = scannedMergedList[i].Length;
                workSheet["E" + j].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;
                workSheet["E" + j].Style.VerticalAlignment = IronXL.Styles.VerticalAlignment.Center;
            }

            Enumerable.Range(0, workSheet.ColumnCount).ToList().ForEach(i => workSheet.AutoSizeColumn(i));

            _workBook.Save();

        }

        private void prepareTitles(WorkSheet workSheet)
        {
            var titleCells = new string[2, 5] { { "A1", "B1", "C1", "D1", "E1" },
                { "File Path","File Created Date", "File Modified Date", "File Accessed Date", "File Size (bytes)" } };

            for (int i = 0; i < 5; i++)
            {
                workSheet[titleCells[0, i]].Value = titleCells[1, i];
                workSheet[titleCells[0, i]].Style.Font.Bold = true;
                workSheet[titleCells[0, i]].Style.WrapText = true;
                workSheet[titleCells[0, i]].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;
                workSheet[titleCells[0, i]].Style.VerticalAlignment = IronXL.Styles.VerticalAlignment.Center;
            }
        }
    }
}
