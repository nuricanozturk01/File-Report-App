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
            _ScannerOperation = moveTestDataCreator._scanOperation;
            _expectedLastAccessSmallerFile = moveTestDataCreator._expectedLastAccessSmallerFile;
            _moveOperation = moveTestDataCreator._moveOperation;

            _expectedLastAccessSmallerFile = 
                _ScannerOperation.GetNewFileList().Select(f => new FileInfo(f)).FirstOrDefault(f => f.FullName == TEST_DIRECTORY_PATH + "\\count.txt");

            Task.WaitAll(Task.Run(_moveOperation.Run));

            WaitSecond(10, () => { });
        }

        private async Task MoveFiles()
        {
            var moveTask = Task.Run(_moveOperation.Run);

            await Task.WhenAll(moveTask);
        }

        /*
         * 
         * Check Overwrite is succussful. 
         * 
         */

        [Fact(DisplayName = "[1] - Check Last Access Date for Overwrite")]
        internal void Check_LastAccessDate_AfterOverwrite()
        {
            var actualBiggerLastAccessDate = new FileInfo(TEST_DIRECTORY_PATH + "\\count.txt");
            if (_expectedLastAccessSmallerFile != null)
            {
                Assert.True(actualBiggerLastAccessDate.LastAccessTime > _expectedLastAccessSmallerFile.LastAccessTime);
                Assert.True(_expectedLastAccessSmallerFile.LastAccessTime < actualBiggerLastAccessDate.LastAccessTime);
            }
            
        }



     

    }
}