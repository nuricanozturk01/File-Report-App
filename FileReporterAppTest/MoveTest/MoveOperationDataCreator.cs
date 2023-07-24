using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class MoveOperationDataCreator : IDisposable
    {
        public FileOperation _moveOperation;
        public FileOperation _scanOperation;

        public readonly string MOVE_OPERATION_BACKUP = PATH_PREFIX + "test_dir_backup_2";
        public long _expectedTotalByte;
        public MoveOperationDataCreator()
        {
            Directory.CreateDirectory(MOVE_OPERATION_BACKUP);

            var dateTime = GetXDayBeforeFromToday(50);

            _expectedTotalByte = GetTotalByteOnTestDirectory(); // file count before move operation

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, MOVE_OPERATION_BACKUP,GetCreatedDate()); // Create Scan process

            _scanOperation.Run();

            // Create Move Operation
            _moveOperation = MoveBuilder.Create_Move_Operation(_scanOperation, MOVE_OPERATION_BACKUP, MOVE_TEST_DIRECTORY_PATH);
        }


        public void Dispose()
        {

        }
    }
}
