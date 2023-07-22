using FileReporterDecorator.FileOperation;
using FileReporterDecorator.FileOperation.operations;
using FileReporterLib.Exceptions;

namespace FileReporterAppTest.CopyTest.OverwriteFile
{
    public class MoveOperationOverwriteTest : IClassFixture<MoveOverwriteFileDataCreator>
    {
        private readonly FileOperation _ScannerOperation;
        private readonly FileOperation _moveOperation;

        private FileOperation _normalFileCopyOperation;

        public MoveOperationOverwriteTest(MoveOverwriteFileDataCreator moveTestDataCreator)
        {
            Directory.CreateDirectory(MOVE_TEST_DIRECTORY_OVERWRITE_PATH);

            _ScannerOperation = moveTestDataCreator._scanOperation;

            _moveOperation = moveTestDataCreator._moveOperation;

            _normalFileCopyOperation = new CopyFileOperation(_ScannerOperation, GetTotalFileCountOnTestDirectory(), DEFAULT_THREAD_COUNT, TEST_DIRECTORY_PATH, MOVE_TEST_DIRECTORY_OVERWRITE_PATH, EMPTY_SHOW_ON_SCREEN_CALLBACK,
                 EMPTY_SHOW_MIN_PROGRESSBAR_CALLBACK, EMPTY_SHOW_MAXIMIZE_PROGRESSBAR_CALLBACK, EMPTY_SHOW_TIME_CALLBACK, EMPTY_ERROR_LABEL_CALLBACK);
           // _normalFileCopyOperation = new OverwriteOptionDecorator(_normalFileCopyOperation, _ScannerOperation);
        }

        [Fact(DisplayName = "[1] - Copy Normal Files")]
        internal void Copy_Normal_Files()
        {
            _normalFileCopyOperation.Run();
        }

        [Fact(DisplayName = "[2] - Check Assertion for Overwrite")]
        internal void Move_Normal_Files_Catch_Exception()
        {
            Assert.ThrowsAnyAsync<FileConflictException>(_moveOperation.Run);
        }

        [Fact(DisplayName = "[3] - Move With Overwrite")]
        internal void Move_Normal_Files_Overwrite()
        {
            WaitSecond(5, () => _moveOperation.Run());
        }

        [Fact(DisplayName = "[4] - Are TotalBytes Equal After Move Overwrite")]
        internal void Are_TotalBytes_Equals_After_Move()
        {
            var afterCopyTotalByteOnMoveDirectory = GetTotalByteOnDirectory(MOVE_TEST_DIRECTORY_OVERWRITE_PATH);
            var afterCopyTotalByte = GetTotalByteOnDirectory(TEST_DIRECTORY_PATH);

            Assert.True(afterCopyTotalByteOnMoveDirectory != 0 && afterCopyTotalByte == 0);
        }

        [Fact(DisplayName = "[5] - Are Total File Count Equal After Move Overwrite")]
        internal void Are_Total_File_Count_Equals_After_Move()
        {
            Assert.Equal(_moveOperation.GetNewFileList().Count(), _normalFileCopyOperation.GetNewFileList().Count());
        }
    }
}