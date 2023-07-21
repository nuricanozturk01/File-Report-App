using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.CopyTest
{
    
    public class MoveOperation : IClassFixture<MoveOperationDataCreator>
    {
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _moveOperation;
        private readonly int _beforeFileCount;
        public MoveOperation(MoveOperationDataCreator moveDataCreator)
        {
            Directory.CreateDirectory(MOVE_TEST_DIRECTORY_PATH);

            _scannerOperation = moveDataCreator._scanOperation;
            _moveOperation = moveDataCreator._moveOperation;
            _beforeFileCount = moveDataCreator._beforeFileCount;
        }


        [Fact(DisplayName = "[1] - Move Files")]
        public async void MoveFilesToTarget() => await Task.Run(_moveOperation.Run);


        [Fact(DisplayName = "[2] - Move Is File Counts Are Equal")]
        internal void RunIsFileCountsAreEqual()
        {
            var afterCopyFileCount = GetTotalFileCountOnDirectory(TEST_DIRECTORY_PATH);

            Assert.Equal(0, afterCopyFileCount);
            Assert.True(_beforeFileCount!= afterCopyFileCount);
        }


        [Fact(DisplayName = "[3] - Move Are Total Bytes Are Equal")]
        internal void RunIsTotalByte()
        {
            var totalByteBefore = GetTotalByteOnTestDirectory();

            var totalByteAfter = GetTotalByteOnDirectory(MOVE_TEST_DIRECTORY_PATH);

            Assert.NotEqual(totalByteBefore, totalByteAfter);
        }
    }
}
