using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.ScanTest
{
    public class ScanModifiedTestDataCreator : IDisposable
    {
        public FileOperation _fileOperation;
        public DateTime _dateTime;
        public ScanModifiedTestDataCreator()
        {
            var totalFileCount = GetTotalFileCountOnTestDirectory();
            _dateTime = GetXDayBeforeFromToday(20);
            File.WriteAllText("C:\\Users\\hp\\Desktop\\deneme.txt", _dateTime.ToString());
            //MofifiedDate
            _fileOperation = ScanBuilder.CreateScanProcess(_dateTime, GetModifiedDate());

            //Last Access Date
            //_fileOperation = OperationCreatingFactory.ScanBuilder.CreateScanProcess(dateTime, totalFileCount, TEST_DIRECTORY_PATH, GetLastAccessDate());
        }
        public void Dispose()
        {

        }
    }
}
