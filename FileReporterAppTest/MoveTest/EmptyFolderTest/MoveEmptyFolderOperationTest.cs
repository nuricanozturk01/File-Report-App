using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest
{

    public class MoveEmptyFolderOperationTest : IClassFixture<MoveEmptyFolderTestDataCreator>
    {
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _moveOperation;

        private string MOVE_OPERATION_BACKUP_PATH;
        public MoveEmptyFolderOperationTest(MoveEmptyFolderTestDataCreator moveTestDataCreator)
        {
            Directory.CreateDirectory(MOVE_TEST_DIRECTORY_PATH_EMPTY);

            MOVE_OPERATION_BACKUP_PATH = moveTestDataCreator.MOVE_OPERATION_BACKUP;

            _scannerOperation = moveTestDataCreator._scanOperation;
            _moveOperation = moveTestDataCreator._moveOperation;

            _moveOperation.Run();

            WaitSecond(3, () => { });
        }

        /*
         * 
         * Check Empty folder count 
         * 
         */

        [Fact(DisplayName = "[1] - Are Equal Empty Folder Count")]
        public void Equal_EmptyFolderCount_AfterMove()
        {
            var expectedEmptyFolderCount = _scannerOperation.GetEmptyDirectoryList().Count();

            var actualEmptyFolderCount = FindEmptyDirectories(MOVE_TEST_DIRECTORY_PATH_EMPTY).Count();
            
            WaitSecond(3, () => { });
            
            Assert.Equal(expectedEmptyFolderCount, actualEmptyFolderCount);
        }

    }
}
