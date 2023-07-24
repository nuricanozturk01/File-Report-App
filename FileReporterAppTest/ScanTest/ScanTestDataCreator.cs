using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.ScanTest
{
    public class ScanTestDataCreator : IDisposable
    {
        public FileOperation _fileOperation;
        public DateTime _dateTime;
        public ScanTestDataCreator()
        {
            _dateTime = GetTestDateTimeNew(); // Get DateTime (now)

            _fileOperation = ScanBuilder.CreateScanProcess(_dateTime, GetCreatedDate()); // CXreate Scan Process with created Date Option
        }
        public void Dispose()
        {

        }
    }
}
