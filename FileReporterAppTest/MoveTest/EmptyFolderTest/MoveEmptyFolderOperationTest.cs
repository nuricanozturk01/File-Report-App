using FileReporterDecorator.FileOperation;
using System.Text.RegularExpressions;

namespace FileReporterAppTest.CopyTest
{
    
    public class MoveEmptyFolderOperationTest : IClassFixture<MoveEmptyFolderTestDataCreator>
    {
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _moveOperation;
        public MoveEmptyFolderOperationTest(MoveEmptyFolderTestDataCreator moveTestDataCreator)
        {
            Directory.CreateDirectory(MOVE_TEST_DIRECTORY_PATH_EMPTY);

            _scannerOperation = moveTestDataCreator._scanOperation;
            _moveOperation = moveTestDataCreator._copyOperation;
        }
        
        [Fact(DisplayName = "[1] - Copy Files with Empty Directories")]
        public async void MoveEmptyFolder() => await Task.Run(_moveOperation.Run);


        
        
        [Fact(DisplayName = "[2] - Are Equal Empty Folder Count")]
        public void Are_Equal_Empty_Folder_Count()
        {
            var scannedEmptyFileCount = _scannerOperation.GetEmptyDirectoryList().Count();

            var movedEmptyFileCount = FindEmptyDirectories(MOVE_TEST_DIRECTORY_PATH_EMPTY).Count();

            Assert.Equal(scannedEmptyFileCount, movedEmptyFileCount);
        }

        [Fact(DisplayName = "[3] Are Main Directory is Empty")]
        public void Is_Test_Directory_Empty()
        {
            var scanOperation = ScanBuilder.CreateScanProcess(GetXDayBeforeFromToday(2), GetCreatedDate());
            scanOperation.Run();
            Assert.True(scanOperation.GetNewFileList().Count == 0);
        }

    }
}
