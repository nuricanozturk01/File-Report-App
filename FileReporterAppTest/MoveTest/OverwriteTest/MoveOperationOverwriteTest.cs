using FileReporterDecorator.FileOperation;
using FileReporterDecorator.FileOperation.operations;

namespace FileReporterAppTest.CopyTest.OverwriteFile
{
    public class MoveOperationOverwriteTest : IClassFixture<MoveOverwriteFileDataCreator>
    {
        private readonly FileOperation _ScannerOperation;
        private readonly FileOperation _moveOperation;
        
        private readonly FileInfo? _expectedLastAccessSmallerFile;
        public MoveOperationOverwriteTest(MoveOverwriteFileDataCreator moveTestDataCreator)
        {
           // Directory.CreateDirectory(MOVE_TEST_DIRECTORY_OVERWRITE_PATH);

            _ScannerOperation = moveTestDataCreator._scanOperation;
            _expectedLastAccessSmallerFile = moveTestDataCreator._expectedLastAccessSmallerFile;
            _moveOperation = moveTestDataCreator._moveOperation;
            _expectedLastAccessSmallerFile = _ScannerOperation.GetNewFileList().Select(f => new FileInfo(f)).FirstOrDefault(f => f.FullName == TEST_DIRECTORY_PATH + "\\count.txt");
            _moveOperation.Run();

            WaitSecond(5, () => { });
        }


        [Fact(DisplayName = "[1] - Check Last Access Date for Overwrite")]
        internal void Check_LastAccessDate_AfterOverwrite()
        {
            var actualBiggerLastAccessDate = new FileInfo(TEST_DIRECTORY_PATH + "\\count.txt");
            if (_expectedLastAccessSmallerFile != null)
            {
                Assert.True(actualBiggerLastAccessDate.LastAccessTime > _expectedLastAccessSmallerFile.LastAccessTime);
            }
            
        }



     

    }
}