using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.CopyTest
{

    public class MoveOperation : IClassFixture<MoveOperationDataCreator>
    {
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _moveOperation;
        private readonly long _expectedTotalByteOnTestDirectory;
        private  long _actualTotalByteAfterCopy;
        private string MOVE_OPERATION_BACKUP_PATH;

        public MoveOperation(MoveOperationDataCreator moveDataCreator)
        {
            Directory.CreateDirectory(MOVE_TEST_DIRECTORY_PATH);

            MOVE_OPERATION_BACKUP_PATH = moveDataCreator.MOVE_OPERATION_BACKUP;
            _scannerOperation = moveDataCreator._scanOperation;
            _moveOperation = moveDataCreator._moveOperation;
            _expectedTotalByteOnTestDirectory = moveDataCreator._expectedTotalByte;

            _moveOperation.Run();

            WaitSecond(7, () => { });
        }

        /*
         * 
         * 
         * Check file counts are equal 
         *
         */

        [Fact(DisplayName = "[1] - Equal after move operation file count")]
        internal void Equal_AfterMoveOperation_FileCount()
        {
            var actualFileCountOnTestDirectory = GetTotalFileCountOnDirectory(MOVE_OPERATION_BACKUP_PATH);

            var expectedTotalFileCountOnTestDirectory = 0;

            Assert.Equal(expectedTotalFileCountOnTestDirectory, actualFileCountOnTestDirectory);

            Assert.True(_expectedTotalByteOnTestDirectory != actualFileCountOnTestDirectory);
        }


        /*
         * 
         * 
         * Check Total Bytes Are Equal 
         * 
         */
        [Fact(DisplayName = "[2] - Move Are Total Bytes Are Equal")]
        internal async void Equal_TotalBytes_MoveOperation()
        {
            var t = Task.Run(() => GetTotalByteOnDirectory(MOVE_TEST_DIRECTORY_PATH));
            
            await Task.WhenAll(t);
            
            WaitSecond(3, () => { });


            Assert.Equal(_expectedTotalByteOnTestDirectory, t.Result);
        }
    }
}
