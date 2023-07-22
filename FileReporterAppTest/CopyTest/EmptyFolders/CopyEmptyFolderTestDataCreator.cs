using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class CopyEmptyFolderTestDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public CopyEmptyFolderTestDataCreator()
        {
            var dateTime = GetXDayBeforeFromToday(2);

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate());

            _scanOperation.Run();

            _copyOperation = CopyBuilder.Create_Copy_EmptyFolder_Operation(_scanOperation);
        }

        public void Dispose()
        {
            //Directory.Delete(TEST_DIRECTORY_PATH_EMPTY);
        }
    }
}
