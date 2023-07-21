using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class CopyTestDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public CopyTestDataCreator()
        {

            var totalFileCount = GetTotalFileCountOnTestDirectory();
            var dateTime = GetXDayBeforeFromToday(2);

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate());

            _scanOperation.Run();

            _copyOperation = CopyBuilder.Create_Copy_Operation(_scanOperation, TEST_DIRECTORY_COPY_PATH);
        }


        public void Dispose() => Directory.Delete(TEST_DIRECTORY_COPY_PATH, true);
    }
}
