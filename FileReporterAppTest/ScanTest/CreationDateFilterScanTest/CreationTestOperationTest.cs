using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.ScanTest
{
    public class CreationModifiedTestOperationTest : IClassFixture<CreationTestDataCreator>
    {
        private readonly FileOperation _scanOperation;
        private DateTime dateTime;
        public CreationModifiedTestOperationTest(CreationTestDataCreator scanTestDataCreator)
        {
            _scanOperation = scanTestDataCreator._fileOperation;
            dateTime = scanTestDataCreator._dateTime;
            _scanOperation.Run();
        }

        [Fact(DisplayName = "[1] - Created Dates Are Checked [After]")]
        public void Are_Modified_Dates_Checked_After()
        {
            List<string> newFileList = _scanOperation.GetNewFileList().ToList();

            if (newFileList is not null && newFileList.Count() != 0)
            {
                Assert.True(newFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.CreationTime >= dateTime));
                //Assert.Equal(4, newFileList.Count);
            }

            else Assert.Empty(newFileList);
        }

        [Fact(DisplayName = "[2] - Created Dates Are Checked [Before]")]
        public void Are_Modified_Dates_Checked_Before()
        {
            List<string> oldFileList = _scanOperation.GetOldFileList().ToList();

            if (oldFileList is not null && oldFileList.Count() != 0)
            {
                Assert.True(oldFileList.Select(fi => new FileInfo(fi)).ToList().All(fi => fi.CreationTime < dateTime));
                //Assert.Equal(1, oldFileList.Count());
            }

            else Assert.Empty(oldFileList);
        }
    }
}