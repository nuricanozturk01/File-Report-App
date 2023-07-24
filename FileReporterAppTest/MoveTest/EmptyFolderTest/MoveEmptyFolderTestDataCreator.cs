using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class MoveEmptyFolderTestDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public MoveEmptyFolderTestDataCreator()
        {
            var dateTime = GetXDayBeforeFromToday(2);

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate());

            _scanOperation.Run();

            _copyOperation = MoveBuilder.Create_Move_EmptyFolder_Operation(_scanOperation);
        }

        public void Dispose()
        {

        }
    }
}
