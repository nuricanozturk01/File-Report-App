using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.ScanTest
{
    public class AccessTestOperationTest : IClassFixture<AccessTestDataCreator>
    {
        private readonly FileOperation _scanOperation;
        private DateTime dateTime;
        public AccessTestOperationTest(AccessTestDataCreator scanTestDataCreator)
        {
            _scanOperation = scanTestDataCreator._fileOperation;
            dateTime = scanTestDataCreator._dateTime;
            _scanOperation.Run();
        }

        [Fact(DisplayName = "[1] - Check All Dates Last Access Time [After]")]
        public void Valid_AllDates_LastAccessTime_After()
        {
            var newFileList = _scanOperation.GetNewFileList().ToList();

            if (newFileList.Count() != 0) // list cannot be null
            {
                var expectedAllDatesAreValid = newFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.LastAccessTime >= dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list is empty
            else Assert.Empty(newFileList);
        }

        [Fact(DisplayName = "[2] - Check All Dates Last Access Time [Before]")]
        public void Valid_AllDates_LastAccessTime_Before()
        {
            var oldFileList = _scanOperation.GetOldFileList().ToList();

            if (oldFileList.Count() != 0) // list cannot be empty
            {
                var expectedAllDatesAreValid = oldFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.LastAccessTime < dateTime);
                Assert.True(expectedAllDatesAreValid);
            }

            // If list is empty
            else Assert.Empty(oldFileList);
        }
    }
}