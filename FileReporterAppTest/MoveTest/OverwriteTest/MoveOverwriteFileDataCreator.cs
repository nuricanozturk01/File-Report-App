using FileReporterDecorator.FileOperation;


namespace FileReporterAppTest.CopyTest.OverwriteFile
{
    public class MoveOverwriteFileDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public MoveOverwriteFileDataCreator()
        {
            var dateTime = GetTestDateTimeNew();

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate());

            _scanOperation.Run();
        
            _copyOperation = MoveBuilder.Create_Move_Overwrite_Operation(_scanOperation, MOVE_TEST_DIRECTORY_OVERWRITE_PATH);
        }
        public void Dispose()
        {
            Directory.Delete(MOVE_TEST_DIRECTORY_OVERWRITE_PATH, true);
        }
    }
}