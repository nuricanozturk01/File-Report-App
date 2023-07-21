using FileReporterDecorator.FileOperation;
using FileReporterLib.Exceptions;

namespace FileReporterAppTest.CopyTest.OverwriteFile
{
    public class CopyOperationOverwriteTest : IClassFixture<OverwriteFileDataCreator>
    {
        private readonly FileOperation _ScannerOperation;
        private readonly FileOperation _copyOperation;

        private FileOperation _normalFileCopyOperation;

        public CopyOperationOverwriteTest(OverwriteFileDataCreator copyTestDataCreator)
        {
            Directory.CreateDirectory(TEST_DIRECTORY_OVERWRITE_PATH);

            _ScannerOperation = copyTestDataCreator._scanOperation;

            _copyOperation = copyTestDataCreator._copyOperation;

            _normalFileCopyOperation = CopyBuilder.Create_Copy_Operation(_ScannerOperation, TEST_DIRECTORY_OVERWRITE_PATH);
        }

        [Fact(DisplayName = "[1] - Copy Normal Files")]
        internal void Copy_Normal_Files()
        {
            _normalFileCopyOperation.Run();
        }

        [Fact(DisplayName = "[2] - Check Assertion for Overwrite")]
        internal void Copy_Normal_Files_Catch_Exception()
        {
            Assert.ThrowsAnyAsync<FileConflictException>(() => _normalFileCopyOperation.Run());
        }

        [Fact(DisplayName = "[3] - Copy With Overwrite")]
        internal async void Copy_Normal_Files_Copy_Overwrite()
        {
            await Task.Run(_copyOperation.Run);
        }

        [Fact(DisplayName = "[4] - Are TotalBytes Equal After Copy Overwrite")]
        internal void Are_TotalBytes_Equals_After_Copy()
        {
            WaitSecond(5, () =>
            {
                var afterCopyTotalByte = GetTotalByteOnDirectory(TEST_DIRECTORY_OVERWRITE_PATH);

                var beforeCopyTotalByte = GetTotalByteOnTestDirectory();

                Assert.Equal(beforeCopyTotalByte, afterCopyTotalByte);
            });

        }

        [Fact(DisplayName = "[5] - Are Total Filr Count  Equal After Copy Overwrite")]
        internal void Are_Total_File_Count_Equals_After_Copy()
        {
            Assert.Equal(_copyOperation.GetNewFileList().Count(), _normalFileCopyOperation.GetNewFileList().Count());
        }
    }
}