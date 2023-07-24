using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest.OverwriteFile
{
    public class OverwriteFileDataCreator : IDisposable
    {
        public FileOperation _overwriteCopyOperation;
        public FileOperation _scanOperation;

        public OverwriteFileDataCreator()
        {
            var dateTime = GetTestDateTimeNew(); // Get date (now) (DateTime)

            _scanOperation = ScanBuilder.CreateScanProcess(dateTime, GetCreatedDate()); // Create scan process

            _scanOperation.Run();

            // Create Copy operation decorated with overwrite operation
            _overwriteCopyOperation = CopyBuilder.Create_Copy_Overwrite_Operation(_scanOperation, TEST_DIRECTORY_OVERWRITE_PATH); 
        }
        public void Dispose()
        {
            
        }
    }
}