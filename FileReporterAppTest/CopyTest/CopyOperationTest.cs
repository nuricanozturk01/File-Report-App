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

            copy();
            
        }

        private async Task copy()
        {
            await Task.Run(_copyOperation.Run);
        }

        [Fact(DisplayName = "[2] - Are Total Bytes Are Equal")]
        internal void Run_Check_Total_Byte()
        {
            var totalByteBefore = GetTotalByteOnTestDirectory();

            var totalByteAfter = _scannerOperation.GetNewFileList()
                .Select(fileName => new FileInfo(fileName))
                .Select(fi => fi.Length)
                .Sum();

            Assert.Equal(totalByteBefore, totalByteAfter);
        }

        [Fact(DisplayName = "[1] - Is File Counts Are Equal")]
        internal void Run_FileCounts_Are_Equal()
        {
            var beforeFileCount = GetTotalFileCountOnTestDirectory();

            var afterCopyFileCount = GetTotalFileCountOnDirectory(TEST_DIRECTORY_COPY_PATH);

            Assert.Equal(beforeFileCount, afterCopyFileCount);
        }
    }
}
