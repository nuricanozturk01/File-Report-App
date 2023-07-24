using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class CopyEmptyFolderTestDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public CopyEmptyFolderTestDataCreator()
        {
            var dateTime = GetXDayBeforeFromToday(20); // Create DateTime Object before 20 day 

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate()); // Create Scan Process

            _scanOperation.Run();

            // Create copy operation with decorated empty folder option.
            _copyOperation = CopyBuilder.Create_Copy_EmptyFolder_Operation(_scanOperation);
        }

        public void Dispose()
        {
            //Directory.Delete(TEST_DIRECTORY_PATH_EMPTY);
        }
    }
}
