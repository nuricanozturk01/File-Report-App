namespace FileReporterDecorator.FileOperation
{
    public class OverwriteOptionDecorator : FileOperationDecorator
    {
        private readonly FileOperation _fileOperation;
        private readonly FileOperation scanProcess;

       /*
       * 
       * decorate the overwrite feature to main operation 
       * 
       */
        public OverwriteOptionDecorator(FileOperation fileOperation, FileOperation scanProcess)
        {
            _fileOperation = fileOperation;
            this.scanProcess = scanProcess;
        }


        /*
         * 
         * 
         * Trigger method for Set Overwrite feature. 
         * 
         * 
         */
        public override async Task Run()
        {
            _fileOperation.SetOverwrite(true);
            SetOverwrite(true);
            scanProcess.SetOverwrite(true);
            await _fileOperation.Run();

        }
    }
}
