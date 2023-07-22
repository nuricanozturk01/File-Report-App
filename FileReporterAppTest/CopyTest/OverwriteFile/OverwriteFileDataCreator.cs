using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest.OverwriteFile
{
    public class OverwriteFileDataCreator : IDisposable
    {
        public FileOperation _copyOperation;
        public FileOperation _scanOperation;

        public OverwriteFileDataCreator()
        {
            var dateTime = GetTestDateTimeNew();

            _scanOperation = ScanBuilder
               .CreateScanProcess(dateTime, GetCreatedDate());

            _scanOperation.Run();

            _copyOperation = CopyBuilder.Create_Copy_Overwrite_Operation(_scanOperation, TEST_DIRECTORY_OVERWRITE_PATH);
        }
        public void Dispose()
        {
            //WaitSecond(4, () => Directory.Delete(TEST_DIRECTORY_OVERWRITE_PATH, true));
        }
    }
}