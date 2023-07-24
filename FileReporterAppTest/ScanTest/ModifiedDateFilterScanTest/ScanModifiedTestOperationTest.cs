using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.ScanTest
{
    public class ScanModifiedTestOperationTest : IClassFixture<ScanModifiedTestDataCreator>
    {
        private readonly FileOperation _scanOperation;
        private DateTime dateTime;
        public ScanModifiedTestOperationTest(ScanModifiedTestDataCreator scanTestDataCreator)
        {
            _scanOperation = scanTestDataCreator._fileOperation;
            dateTime = scanTestDataCreator._dateTime;
            _scanOperation.Run();
        }

        [Fact(DisplayName = "[1] - Check All Dates Last Write Time [After]")]
        public void Valid_AllDates_LastWriteTime_After()
        {
            var newFileList = _scanOperation.GetNewFileList().ToList();

            if (newFileList.Count() != 0) // list cannot be empty
            {
                var expectedAllDatesAreValid = newFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.LastWriteTime >= dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list is empty
            else Assert.Empty(newFileList);
        }

        [Fact(DisplayName = "[2] - Check All Dates Last Write Time [Before]")]
        public void Valid_AllDates_LastWriteTime_Before()
        {
            var oldFileList = _scanOperation.GetOldFileList().ToList();

            if (oldFileList.Count() != 0) // list cannot be null
            {
                var expectedAllDatesAreValid = oldFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.LastWriteTime < dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // if list is empty
            else Assert.Empty(oldFileList);
        }
    }
}