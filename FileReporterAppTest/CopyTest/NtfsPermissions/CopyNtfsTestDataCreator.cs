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
            var dateTime = GetXDayBeforeFromToday(20);

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate());

            _scanOperation.Run();

            _copyOperation = CopyBuilder.Create_Copy_Ntfs_Permission_Operation(_scanOperation, TEST_DIRECTORY_NTFS_PATH);
        }


        public void Dispose()
        {
            //Directory.Delete(TEST_DIRECTORY_NTFS_PATH, true);
        }
    }
}
