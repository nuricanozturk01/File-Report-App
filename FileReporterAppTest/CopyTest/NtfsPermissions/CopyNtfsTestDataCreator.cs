using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class CopyNtfsTestDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public CopyNtfsTestDataCreator()
        {

            var totalFileCount = GetTotalFileCountOnTestDirectory();
            var dateTime = GetXDayBeforeFromToday(2);

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate());

            _scanOperation.Run();

            _copyOperation = CopyBuilder.Create_Copy_Operation(_scanOperation, TEST_DIRECTORY_PATH_EMPTY);
        }


        public void Dispose()
        {

        }
    }
}
