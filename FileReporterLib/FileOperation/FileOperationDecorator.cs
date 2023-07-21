namespace FileReporterDecorator.FileOperation
{
    public abstract class FileOperationDecorator : FileOperation
    {
        protected FileOperation _fileOperation;

        public FileOperationDecorator(FileOperation fileOperation)
        {
            _fileOperation = fileOperation;
        }

        public override async Task Run()
        {
            await _fileOperation.Run();
        }

    }
}
