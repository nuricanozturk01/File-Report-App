using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.ScanTest
{
    public class AccessTestDataCreator : IDisposable
    {
        public FileOperation _fileOperation;
        public DateTime _dateTime;
        public AccessTestDataCreator()
        {
            //_dateTime = GetXDayBeforeFromToday(20);
            
            /**
             * 
             *  You can test the After option if you remove the comment line below
             *
             */

            _dateTime = GetXDayAfterFromToday(50);
            
            _fileOperation = ScanBuilder.CreateScanProcess(_dateTime, GetLastAccessDate());
        }
        public void Dispose()
        {

        }
    }
}
