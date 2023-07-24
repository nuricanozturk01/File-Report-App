using FileReporterDecorator.FileOperation;

namespace FileReporterAppTest.CopyTest.OverwriteFile
{
    public class CopyOperationOverwriteTest : IClassFixture<OverwriteFileDataCreator>
    {
        private readonly FileOperation _ScannerOperation;
        private readonly FileOperation _overwriteCopyOperation;

        private FileOperation _copyOperation;

        public CopyOperationOverwriteTest(OverwriteFileDataCreator copyTestDataCreator)
        {
            Directory.CreateDirectory(TEST_DIRECTORY_OVERWRITE_PATH);

            _ScannerOperation = copyTestDataCreator._scanOperation;

            _overwriteCopyOperation = copyTestDataCreator._overwriteCopyOperation;

            // Defined other copy operation (Without any decorator) for test overwrite
            _copyOperation = CopyBuilder.Create_Copy_Operation(_ScannerOperation, TEST_DIRECTORY_OVERWRITE_PATH);
            _copyOperation.Run(); // copy operation run (first copy)
        }

        

        [Fact(DisplayName = "[2] - Check Last Access Date for Overwrite")]
        internal async Task Check_LastAccessDate_AfterOverwrite()
        {
            var expectedSmallerLastAccessDate = new FileInfo(TEST_DIRECTORY_OVERWRITE_PATH + "\\count.txt").LastAccessTime;

            await Task.Run(_overwriteCopyOperation.Run);
            
            var actualBiggerLastAccessDate = new FileInfo(TEST_DIRECTORY_OVERWRITE_PATH + "\\count.txt").LastAccessTime;

            Assert.True(actualBiggerLastAccessDate > expectedSmallerLastAccessDate);
        }

    

        [Fact(DisplayName = "[5] - Are Total Filr Count  Equal After Copy Overwrite")]
        internal void Equal_TotalFileCount_AfterOverwriteCopy()
        {
            var expectedFileCount = _overwriteCopyOperation.GetNewFileList().Count();
            var actualFileCount = _copyOperation.GetNewFileList().Count();

            Assert.Equal(expectedFileCount, actualFileCount);
        }
    }
}