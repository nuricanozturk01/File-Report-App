using FileReporterDecorator.FileOperation;
namespace FileReporterAppTest.CopyTest
{
    
    public class CopyNtfsOperationTest : IClassFixture<CopyNtfsTestDataCreator>
    {
        private readonly int WAIT_MINUTE_COPY_SECOND = 5;
        private readonly FileOperation _scannerOperation;
        private readonly FileOperation _copyOperation;
        public CopyNtfsOperationTest(CopyNtfsTestDataCreator copyTestDataCreator)
        {
            Directory.CreateDirectory(TEST_DIRECTORY_COPY_PATH);

            _scannerOperation = copyTestDataCreator._scanOperation;
            _copyOperation = copyTestDataCreator._copyOperation;

            WaitSecond(WAIT_MINUTE_COPY_SECOND, () => _copyOperation.Run());
        }
       
    }
}
