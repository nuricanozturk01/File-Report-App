using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{

    public class CopyOperationTest : IClassFixture<CopyTestDataCreator>
    {
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _copyOperation;
        public CopyOperationTest(CopyTestDataCreator copyTestDataCreator)
        {
            Directory.CreateDirectory(TEST_DIRECTORY_COPY_PATH);

            _scannerOperation = copyTestDataCreator._scanOperation;
            _copyOperation = copyTestDataCreator._copyOperation;

            _copyOperation.Run();

            WaitSecond(3, () => { });
        }

        

        [Fact(DisplayName = "[2] - Are Total Bytes Are Equal After Copy")]
        internal void Equal_TotalByte_AfterCopy()
        {
            var expectedTotalByteBeforeCopy = GetTotalByteOnTestDirectory();

            var actualTotalByteAfterCopy = _scannerOperation.GetNewFileList()
                .Select(fileName => new FileInfo(fileName))
                .Select(fi => fi.Length)
                .Sum();
            
            Assert.Equal(expectedTotalByteBeforeCopy, actualTotalByteAfterCopy);
        }

        
        [Fact(DisplayName = "[1] - Equal File Count After Copy")]
        internal async void Equal_FileCount_AfterCopy()
        {
            var task1 = Task.Run(() => GetTotalFileCountOnTestDirectory());

            var task2 = Task.Run(() => GetTotalFileCountOnDirectory(TEST_DIRECTORY_COPY_PATH));

            var expectedFileCountBeforeCopy = await task1;
            var actualFileCountAfterCopy = await task2;
            
            Assert.Equal(expectedFileCountBeforeCopy, actualFileCountAfterCopy);
        }
    }
}
