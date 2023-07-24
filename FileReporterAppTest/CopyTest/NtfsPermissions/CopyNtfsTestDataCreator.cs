using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class CopyNtfsTestDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public CopyNtfsTestDataCreator()
        {
            var dateTime = GetXDayBeforeFromToday(20); // Get DateTime before 20 day

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate()); // Create Scan Process

            _scanOperation.Run();

            // Create Copt Operation with ntfs permission decorator
            _copyOperation = CopyBuilder.Create_Copy_Ntfs_Permission_Operation(_scanOperation, TEST_DIRECTORY_NTFS_PATH);
        }


        public void Dispose()
        {
            //Directory.Delete(TEST_DIRECTORY_NTFS_PATH, true);
        }
    }
}
