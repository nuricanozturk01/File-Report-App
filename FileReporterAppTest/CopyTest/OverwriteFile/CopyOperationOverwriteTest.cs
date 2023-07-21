using FileReporterDecorator.FileOperation;
using FileReporterLib;

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
        internal async void Copy_Normal_Files()
        {
            await Task.Run(_normalFileCopyOperation.Run);
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

        [Fact(DisplayName = "[4] - Are TotalBytes Are Equal After Copy Overwrite")]
        internal void Are_TotalBytes_Equals_After_Copy()
        {
            Assert.Equal(_copyOperation.GetNewFileList().Count(), _normalFileCopyOperation.GetNewFileList().Count())
        }

        [Fact(DisplayName = "[5] - Are TotalBytes Are Equal After Copy Overwrite")]
        internal void Are_Total_File_Count_Equals_After_Copy()
        {
            Assert.Equal(_copyOperation.GetNewFileList().Count(), _normalFileCopyOperation.GetNewFileList().Count())
        }
    }
}
