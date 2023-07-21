using FileReporterDecorator.FileOperation;
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

            _moveOperation = moveTestDataCreator._copyOperation;

            _normalFileCopyOperation = CopyBuilder.Create_Copy_Operation(_ScannerOperation, MOVE_TEST_DIRECTORY_OVERWRITE_PATH);
        }

        [Fact(DisplayName = "[1] - Move Normal Files")]
        internal void Move_Normal_Files()
        {
            _normalFileCopyOperation.Run();
        }

        [Fact(DisplayName = "[2] - Check Assertion for Overwrite")]
        internal void Move_Normal_Files_Catch_Exception()
        {
            Assert.ThrowsAnyAsync<FileConflictException>(() => _normalFileCopyOperation.Run());
        }

        [Fact(DisplayName = "[3] - Copy With Overwrite")]
        internal void Move_Normal_Files_Copy_Overwrite()
        {
            WaitSecond(5, () => _moveOperation.Run());
        }

        [Fact(DisplayName = "[4] - Are TotalBytes Equal After Move Overwrite")]
        internal void Are_TotalBytes_Equals_After_Copy()
        {
            var afterCopyTotalByteOnMoveDirectory = GetTotalByteOnDirectory(MOVE_TEST_DIRECTORY_OVERWRITE_PATH);
            var afterCopyTotalByte = GetTotalByteOnDirectory(TEST_DIRECTORY_PATH);

            Assert.True(afterCopyTotalByteOnMoveDirectory != 0 && afterCopyTotalByte == 0);
        }

        [Fact(DisplayName = "[5] - Are Total File Count  Equal After Move Overwrite")]
        internal void Are_Total_File_Count_Equals_After_Copy()
        {
            Assert.Equal(_moveOperation.GetNewFileList().Count(), _normalFileCopyOperation.GetNewFileList().Count());
        }
    }
}