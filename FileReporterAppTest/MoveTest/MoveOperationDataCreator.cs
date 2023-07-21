using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class MoveOperationDataCreator : IDisposable
    {
        public FileOperation _moveOperation;
        public FileOperation _scanOperation;
        
        public int _beforeFileCount;
        public MoveOperationDataCreator()
        {
            var dateTime = GetXDayBeforeFromToday(2);

            _beforeFileCount = GetTotalFileCountOnTestDirectory();
            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate());

            _scanOperation.Run();

            _moveOperation = MoveBuilder.Create_Move_Operation(_scanOperation, MOVE_TEST_DIRECTORY_PATH);
        }


        public void Dispose()
        {
            Directory.Delete(MOVE_TEST_DIRECTORY_PATH, true);

            // After Move, Copy files in the backup to TEST_DIRECTORY_PATH
           
        }
    }
}
