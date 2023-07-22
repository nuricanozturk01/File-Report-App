namespace FileReporterDecorator.FileOperation
{
    public class OverwriteOptionDecorator : FileOperationDecorator
    {
        private readonly FileOperation _fileOperation;
        private readonly FileOperation scanProcess;

        public OverwriteOptionDecorator(FileOperation fileOperation, FileOperation scanProcess)
        {
            _fileOperation = fileOperation;
            this.scanProcess = scanProcess;
        }

        public override async Task Run()
        {
            _fileOperation.SetOverwrite(true);
            SetOverwrite(true);
            scanProcess.SetOverwrite(true);
            await _fileOperation.Run();
            
        }
    }
}
