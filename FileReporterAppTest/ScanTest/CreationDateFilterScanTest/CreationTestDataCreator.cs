using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.ScanTest
{
    public class CreationTestDataCreator : IDisposable
    {
        public FileOperation _fileOperation;
        public DateTime _dateTime;


        public CreationTestDataCreator()
        {
            //You can test the After option if you remove the comment line below
            _dateTime = GetXDayAfterFromToday(50);
            
            _fileOperation = ScanBuilder.CreateScanProcess(_dateTime, GetCreatedDate());
        }
        public void Dispose()
        {

        }
    }
}
