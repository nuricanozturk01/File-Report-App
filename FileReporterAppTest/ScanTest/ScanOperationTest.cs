using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.ScanTest
{
    public class ScanOperationTest : IClassFixture<ScanTestDataCreator>
    {
        private readonly FileOperation _scanOperation;

        public ScanOperationTest(ScanTestDataCreator scanTestDataCreator)
        {
            _scanOperation = scanTestDataCreator._fileOperation;
            _scanOperation.Run();
        }

        [Fact(DisplayName = "Is File Count Are Equal")]
        public void Is_File_Counts_Are_Equal()
        {
            Assert.Equal(GetTotalFileCountOnTestDirectory(), _scanOperation.GetTotalFileCount());
        }
    }
}
