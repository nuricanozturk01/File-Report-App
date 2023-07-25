namespace FileReporterDecorator.FileOperation
{
    public class EmptyOptionDecorator : FileOperationDecorator
    {
        private readonly FileOperation _fileOperation;
        private readonly FileOperation _scanProcess;
        
        /*
         * 
         * decorate the empty folder feature to main operation 
         * 
         */
        public EmptyOptionDecorator(FileOperation fileOperation, FileOperation scanProcess)
        {
            _scanProcess = scanProcess;
            _scanProcess.SetEmptyFolder(true);
            _fileOperation = fileOperation;
        }

        /*
         * 
         *  Trigger Method for Empty Folder Operation.
         */
        public override async Task Run()
        {
            await _fileOperation.Run();

        }
    }
}
