using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.ScanTest
{
    public class ScanModifiedTestDataCreator : IDisposable
    {
        public FileOperation _fileOperation;
        public DateTime _dateTime;
        
        public ScanModifiedTestDataCreator()
        {
            _dateTime = GetXDayBeforeFromToday(20); // Get 20 day before
            
            _fileOperation = ScanBuilder.CreateScanProcess(_dateTime, GetModifiedDate()); // Create Scan process
        }

        public void Dispose()
        {

        }
    }
}
