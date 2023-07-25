using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.ScanTest
{
    public class DateComparerDataCreator : IDisposable
    {
        public FileOperation _fileOperation;
        public DateTime _dateTime;
        
        public DateComparerDataCreator()
        {
            _dateTime = GetXDayBeforeFromToday(50); // Get 50 day before
            
            _fileOperation = ScanBuilder.CreateScanProcess(_dateTime, GetModifiedDate()); // Create Scan process
        }

        public void Dispose()
        {

        }
    }
}
