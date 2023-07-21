using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.CopyTest
{
    
    public class CopyEmptyFolderOperationTest : IClassFixture<CopyEmptyFolderTestDataCreator>
    {
        private readonly int WAIT_MINUTE_COPY_SECOND = 8;
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _copyOperation;
        public CopyEmptyFolderOperationTest(CopyEmptyFolderTestDataCreator copyTestDataCreator)
        {
            Directory.CreateDirectory(TEST_DIRECTORY_PATH_EMPTY);

            _scannerOperation = copyTestDataCreator._scanOperation;
            _copyOperation = copyTestDataCreator._copyOperation;
        }

    
        [Fact(DisplayName = "[1] - Copy Files with Empty Directories")]
        public async void CopyEmptyFolder() => await Task.Run(_copyOperation.Run);

        [Fact(DisplayName = "[2] - Are Equal Empty Folder Count")]
        public void Are_Equal_Empty_Folder_Count()
        {
            var scannedEmptyFileCount = _scannerOperation.GetEmptyDirectoryList().Count();

            var copiedEmptyFileCount = FindEmptyDirectories(TEST_DIRECTORY_PATH_EMPTY).Count();

            Assert.Equal(scannedEmptyFileCount, copiedEmptyFileCount);
        }

        [Fact(DisplayName = "[3] Are Equal Empty Folder Names")]
        public void Are_Equal_Empty_Folder_Names()
        {
            var distinctListCount = _scannerOperation.GetEmptyDirectoryList()
                .Concat(FindEmptyDirectories(TEST_DIRECTORY_PATH_EMPTY))
                .Distinct()
                .Count();

            Assert.Equal(distinctListCount - 1, _scannerOperation.GetEmptyDirectoryList().ToList().Count());    
        }
    }
}
