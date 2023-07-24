using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{
    public class MoveEmptyFolderTestDataCreator : IDisposable
    {
        public FileOperation _moveOperation;
        public FileOperation _scanOperation;


        public readonly string MOVE_OPERATION_BACKUP = PATH_PREFIX + "test_dir_backup_3";
        public MoveEmptyFolderTestDataCreator()
        {


            var dateTime = GetXDayBeforeFromToday(10);


            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, MOVE_OPERATION_BACKUP, GetCreatedDate()); // Create Scan process

            _scanOperation.Run();

            // Create Move Operation
            _moveOperation = MoveBuilder.Create_Move_EmptyFolder_Operation(_scanOperation, MOVE_OPERATION_BACKUP, MOVE_TEST_DIRECTORY_PATH_EMPTY);


        }
       
        public void Dispose()
        {

        }
    }
}
