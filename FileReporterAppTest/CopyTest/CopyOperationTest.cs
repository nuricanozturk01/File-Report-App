using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.CopyTest
{
    
    public class CopyOperationTest : IClassFixture<CopyTestDataCreator>
    {
        private readonly int WAIT_MINUTE_COPY_SECOND = 5;
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _copyOperation;
        public CopyOperationTest(CopyTestDataCreator copyTestDataCreator)
        {
            Directory.CreateDirectory(TEST_DIRECTORY_COPY_PATH);

            _scannerOperation = copyTestDataCreator._scanOperation;
            _copyOperation = copyTestDataCreator._copyOperation;
        }

        [Fact(DisplayName = "[1] - Copy Files")]
        public async void Copy_Files_To_Target() => await Task.Run(_copyOperation.Run);


        [Fact(DisplayName = "[2] - Is File Counts Are Equal")]
        internal void Run_FileCounts_Are_Equal()
        {
            var beforeFileCount = GetTotalFileCountOnTestDirectory();

            var afterCopyFileCount = _scannerOperation.GetNewFileList().Count();

            Assert.Equal(beforeFileCount, afterCopyFileCount);
        }
        

        [Fact(DisplayName = "[3] - Are Total Bytes Are Equal")]
        internal void Run_Check_Total_Byte()
        {
            var totalByteBefore = GetTotalByteOnTestDirectory();

            var totalByteAfter = _scannerOperation.GetNewFileList()
                .Select(fileName => new FileInfo(fileName))
                .Select(fi => fi.Length)
                .Sum();

            Assert.Equal(totalByteBefore, totalByteAfter);
        }
    }
}
