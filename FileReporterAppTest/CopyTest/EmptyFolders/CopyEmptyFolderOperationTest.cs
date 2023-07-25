using FileReporterDecorator.FileOperation;
using System.Text.RegularExpressions;

namespace FileReporterAppTest.CopyTest
{
    public class CopyEmptyFolderOperationTest : IClassFixture<CopyEmptyFolderTestDataCreator>
    {
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _copyOperation;

        public CopyEmptyFolderOperationTest(CopyEmptyFolderTestDataCreator copyTestDataCreator)
        {
            Directory.CreateDirectory(TEST_DIRECTORY_PATH_EMPTY);

            _scannerOperation = copyTestDataCreator._scanOperation;
            _copyOperation = copyTestDataCreator._copyOperation;
         
            _copyOperation.Run();

            WaitSecond(3, () => { });
        }

        /*
         * 
         * Check Equal Empty directory count before and after copy operation.
         * 
         */
        [Fact(DisplayName = "[1] - Are Equal Empty Folder Count After Copy")]
        public void Equal_EmptyFolderCount_AfterCopy()
        {
            var expectedEmptyFileCountAfterCopy = FindEmptyDirectories(TEST_DIRECTORY_PATH).Count();

            var actualEmptyFileCountAfterCopy = FindEmptyDirectories(TEST_DIRECTORY_PATH_EMPTY).Count();

            Assert.Equal(expectedEmptyFileCountAfterCopy, actualEmptyFileCountAfterCopy);
        }

        /*
         * 
         * This method get empty directories before and after then merge two list.
         * After merge, if distinct emptyFileCount are equal the expectedEmptyFileList.Count(), test is successful
         * 
         */
        [Fact(DisplayName = "[2] Are Equal Empty Folder Names")]
        public void Are_Equal_Empty_Folder_Names()
        {
            var expectedEmptyFileList = _scannerOperation.GetEmptyDirectoryList()
                .Select(i => Regex.Match(i, @"[^\\]+$").Value).ToList();

            var actualEmptyFileList = FindEmptyDirectories(TEST_DIRECTORY_PATH_EMPTY)
                                   .Select(i => Regex.Match(i, @"[^\\]+$").Value)
                                   .Concat(expectedEmptyFileList)
                                   .Distinct()
                                   .ToList();

            Assert.Equal(expectedEmptyFileList.Count(), actualEmptyFileList.Count());
        }
    }
}
