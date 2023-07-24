using FileReporterDecorator.FileOperation;


namespace FileReporterAppTest.CopyTest.OverwriteFile
{
    public class MoveOverwriteFileDataCreator : IDisposable
    {
        public FileOperation _moveOperation;
        public FileOperation _scanOperation;
        public FileInfo _expectedLastAccessSmallerFile;

        public readonly string MOVE_OPERATION_BACKUP = PATH_PREFIX + "test_dir_backup_1";

        public MoveOverwriteFileDataCreator()
        {


            var dateTime = GetXDayBeforeFromToday(50);

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, MOVE_OPERATION_BACKUP, GetCreatedDate());

            _scanOperation.Run();

            _expectedLastAccessSmallerFile = new FileInfo(TEST_DIRECTORY_PATH + "\\count.txt");

            _moveOperation = MoveBuilder.Create_Move_Overwrite_Operation(_scanOperation, MOVE_OPERATION_BACKUP, TEST_DIRECTORY_PATH);
        }
        public void Dispose()
        {
           // Directory.Delete(MOVE_TEST_DIRECTORY_OVERWRITE_PATH, true);
        }
    }
}