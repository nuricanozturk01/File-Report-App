using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class CopyTestDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public CopyTestDataCreator()
        {
            var dateTime = GetXDayBeforeFromToday(2); // Get 2 days ago DateTime Object

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate()); // Create Scan Process

            _scanOperation.Run();

            _copyOperation = CopyBuilder.Create_Copy_Operation(_scanOperation, TEST_DIRECTORY_COPY_PATH); // Create Copy Operation
        }


        public void Dispose()
        {
            //Directory.Delete(TEST_DIRECTORY_COPY_PATH, true);
        }
    }
}
