using FileReporterDecorator.FileOperation;
using FileReporterDecorator.FileOperation.operations;

namespace FileReporterAppTest.CopyTest.OverwriteFile
{
    public class OverwriteFileDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public OverwriteFileDataCreator()
        {

            var totalFileCount = GetTotalFileCountOnTestDirectory();
            var dateTime = GetTestDateTimeNew();

            _scanOperation = OperationCreatingFactory.ScanBuilder
               .CreateScanProcess(dateTime, GetCreatedDate());

            _scanOperation.Run();

            //_copyOperation = CopyBuilder.Create_Copy_Overwrite_Operation()
            _copyOperation = new CopyFileOperation(_scanOperation, totalFileCount, DEFAULT_THREAD_COUNT, TEST_DIRECTORY_PATH,
                TEST_DIRECTORY_OVERWRITE_PATH, EMPTY_SHOW_ON_SCREEN_CALLBACK, EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK,
                EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK, EMPTY_SHOW_TIME_CALLBACK, EMPTY_SHOW_CONFLICT_MESSAGE_CALLBACK);

            _copyOperation = new OverwriteOptionDecorator(_copyOperation);
        }
        public void Dispose()
        {
            //...
        }
    }
}