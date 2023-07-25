using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.ScanTest
{
    public class ScanOperationTest : IClassFixture<ScanTestDataCreator>
    {
        private readonly FileOperation _scanOperation;

        public ScanOperationTest(ScanTestDataCreator scanTestDataCreator)
        {
            _scanOperation = scanTestDataCreator._fileOperation;
            _scanOperation.Run(); // scan operation start
            WaitSecond(1, () => { });
        }

        /*
         * 
         * 
         * Check the file counts are equal before (on test directory) and after(memory) 
         * 
         */
        [Fact(DisplayName = "Equal File Counts Are Equal Before And After")]
        public void Equal_File_Counts_Are_Equal_Before_And_After()
        {
            WaitSecond(2, () => { });
            Assert.Equal(GetTotalFileCountOnTestDirectory(), _scanOperation.GetTotalFileCount());
        }
    }
}
