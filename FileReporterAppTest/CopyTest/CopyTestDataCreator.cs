using FileReporterDecorator.FileOperation;
using FileReporterDecorator.FileOperation.operations;

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

           /* _copyOperation = new CopyFileOperation(_scanOperation, totalFileCount, DEFAULT_THREAD_COUNT, TEST_DIRECTORY_PATH,
               TEST_DIRECTORY_COPY_PATH, EMPTY_SHOW_ON_SCREEN_CALLBACK, EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK,
               EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK, EMPTY_SHOW_TIME_CALLBACK, EMPTY_SHOW_CONFLICT_MESSAGE_CALLBACK);*/
        }


        public void Dispose()
        {
            Directory.Delete(TEST_DIRECTORY_COPY_PATH, true);
        }
    }
}
