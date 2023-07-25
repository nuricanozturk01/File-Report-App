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

        
        /*
         * 
         * 
         * Check LastAccessDate After Copy 
         * 
         */

        [Fact(DisplayName = "[1] - Check Last Access Date for Overwrite expected bigger")]
        internal void Check_LastAccessDate_AfterOverwrite()
        {
            var expectedSmallerLastAccessDate = new FileInfo(TEST_DIRECTORY_OVERWRITE_PATH + "\\count.txt").LastAccessTime;

            _overwriteCopyOperation.Run();

            WaitSecond(7, () => { });

            var actualBiggerLastAccessDate = new FileInfo(TEST_DIRECTORY_OVERWRITE_PATH + "\\count.txt").LastAccessTime;

            Assert.True(actualBiggerLastAccessDate > expectedSmallerLastAccessDate);
            Assert.True(expectedSmallerLastAccessDate < actualBiggerLastAccessDate);
        }


        /*
         * 
         * Check total file count After Copy 
         * 
         */

        [Fact(DisplayName = "[2] - Are Total File Count  Equal After Copy Overwrite")]
        internal void Equal_TotalFileCount_AfterOverwriteCopy()
        {
            var expectedFileCount = _overwriteCopyOperation.GetNewFileList().Count();
            var actualFileCount = _copyOperation.GetNewFileList().Count();

            Assert.Equal(expectedFileCount, actualFileCount);
        }
    }
}