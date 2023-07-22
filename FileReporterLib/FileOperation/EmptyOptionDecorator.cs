namespace FileReporterDecorator.FileOperation
{
    public class EmptyOptionDecorator : FileOperationDecorator
    {
        private readonly FileOperation _fileOperation;
        private readonly FileOperation _scanProcess;
        public EmptyOptionDecorator(FileOperation fileOperation, FileOperation scanProcess)
        {
            _scanProcess = scanProcess;
            _scanProcess.SetEmptyFolder(true);
            _fileOperation = fileOperation;
            _fileOperation.SetEmptyFolder(true);
        }

        public override async Task Run()
        {
            await _fileOperation.Run();

        }
    }
}
